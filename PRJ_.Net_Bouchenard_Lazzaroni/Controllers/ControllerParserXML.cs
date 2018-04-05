using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Data.SQLite;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    abstract class ControllerParserXML
    {
        public event EventHandler<MyEventArgs> eventUpdateListView; // Send events to the view (Update ListView)
        public event EventHandler<MyEventArgs> eventUpdateProgressBar; // Send events to the view (Update ProgressBar)
        public event EventHandler<MyEventArgs> eventRangeMaxProgressBar; // Send events to the view (set the max range of the ProgressBar)

        protected XmlDocument xmlDocument; // Allow to navigate in the xml file
        protected DBManager dbManager; // Access SQL command
        protected string filename; // Content the path of the file
        protected Articles article;
        protected XmlNode node; // To store the current node when parsing
        protected MyEventArgs argsEvent; // Store args for event
        protected Dictionary<TypeMessage, int> counterTypeMessage; // To know how many success, warning, error, critical, ...

        /// <summary>
        /// Each child implement his own version of parsing.
        /// </summary>
        abstract public void parse();

        /// <summary>
        /// Comfort constructor - Init attributes
        /// </summary>
        /// <param name="filename"> Filename of the xml file gave by the view </param>
        public ControllerParserXML(string filename)
        {
            this.filename = filename;
            argsEvent = new MyEventArgs();
            dbManager = new DBManager();
            xmlDocument = new XmlDocument();
            article = null;

            // Init dictionary. One entry = one enum
            counterTypeMessage = new Dictionary<TypeMessage, int>();
            foreach (TypeMessage foo in Enum.GetValues(typeof(TypeMessage)))
                counterTypeMessage.Add(foo, 0);
        }

        /// <summary>
        /// Called by this class and childs when they want to update the listView (console log)
        /// </summary>
        /// <param name="type"> Type of message (success or warning or etc ...) that you want to send </param>
        /// <param name="subject"> Subject of the message (addArticle, addFamille) </param>
        /// <param name="message"> The message to send </param>
        protected void updateListView(TypeMessage type, SubjectMessage subject, string message)
        {
            argsEvent.message = message;
            argsEvent.subject = subject;
            argsEvent.type = type;

            counterTypeMessage[type] += 1; // Increment the counter

            if (eventUpdateListView != null)
                eventUpdateListView(this, argsEvent); // Send the event
        }

        /// <summary>
        /// Called by this class and childs when they want to update the progress bar of the view
        /// </summary>
        protected void updateProgressBar()
        {
            eventUpdateProgressBar(this, argsEvent);
        }

        /// <summary>
        /// Called by childs one time to set the max range of the progress bar. The max range is defined by the number of articles in the XML file.
        /// </summary>
        /// <param name="nodeCount"> Number of articles in the XML file </param>
        protected void updateMaxRangeProgressBar(int nodeCount)
        {
            argsEvent.maxRange = nodeCount;
            eventRangeMaxProgressBar(this, argsEvent);
        }

        /// <summary>
        /// Load the xml file and verify the structure (just briefly)
        /// </summary>
        protected void loadDocument()
        {
            xmlDocument.Load(filename); // Load the file into the XMLDocument
            updateListView(TypeMessage.Success, SubjectMessage.Xml_Structure, "File loaded");
        }

        /// <summary>
        /// Verify deeply of the structure of the XML file is correct (check the order, if not empty, ...)
        /// </summary>
        protected void verifyFile() // Verify the structure of the xml file
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, "../../validateXMLFile.xsd"); // Add the xsd to the schema
            xmlDocument.Schemas.Add(schemaSet); // Add the schema to the xml document
            ValidationEventHandler veh = new ValidationEventHandler(eventVerifyStructure); // Send event when something goes wrong.
            xmlDocument.Validate(veh); // Run the validation
            updateListView(TypeMessage.Success, SubjectMessage.Xml_Structure, "XML structure is valid");
        }

        /// <summary>
        /// Event sent by verifyFile method 
        /// </summary>
        /// <param name="sender"> Mandatory because receive event </param>
        /// <param name="args"> Mandatory because receive event </param>
        protected void eventVerifyStructure(object sender, ValidationEventArgs args) // Signal send by verifyFile() when the xml structure does not correct.
        {
            throw new System.Exception(args.Message);
        }

        /// <summary>
        /// Called by childs when they want to add an article to the database
        /// </summary>
        protected void addArticle()
        {   
            article = new Articles();

            article.Description = node.SelectSingleNode("description").InnerText;
            article.Reference = node.SelectSingleNode("refArticle").InnerText;

            treatFamille();
            if (!treatSousFamille()) // Check if the subFamily correspond to the good family. If mistake, generate error signal and the article won't be updatd.
            {
                treatMarque();

                article.PrixHT = Convert.ToDouble(node.SelectSingleNode("prixHT").InnerText);
                article.Quantite = 1;

                dbManager.insertArticle(article);
                updateListView(TypeMessage.Success, SubjectMessage.Add_Article, "The article " + article.Reference + " has been added");
            }
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        private void treatFamille()
        {
            Familles famille = dbManager.getFamille(node.SelectSingleNode("famille").InnerText); // Check if the famille already exist
            if (famille == null) // Famille does not exist
            {
                famille = checkSpellingFamilles(node.SelectSingleNode("famille").InnerText);
                if (famille == null)
                {
                    newFamille();
                }
                else
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The familly of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("famille").InnerText + "\". It has been replaced by \"" + famille.Nom + "\"");

                    article.IdFamille = famille.Id;
                    node.SelectSingleNode("famille").InnerText = famille.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
                article.IdFamille = famille.Id;
        }

        /// <summary>
        /// Insert new family to the database
        /// </summary>
        protected void newFamille()
        {
            Familles famille = new Familles();
            famille.Nom = node.SelectSingleNode("famille").InnerText;
            article.IdFamille = dbManager.insertFamille(famille); // Insert return the last id of the famille added.
            updateListView(TypeMessage.Success, SubjectMessage.Add_Famille, "The familly " + famille.Nom + " has been created");
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        /// <returns> True if everything is OK, false if the family does not match to the subfamily </returns>
        private bool treatSousFamille()
        {
            bool foundMistake = false;

            SousFamilles sousFamille = dbManager.getSousFamille(node.SelectSingleNode("sousFamille").InnerText); // Check if the sousFamille already exist
            if (sousFamille == null) // If the sousFamille does not exist
            {
                sousFamille = checkSpellingSousFamilles(node.SelectSingleNode("sousFamille").InnerText);
                if (sousFamille == null)
                {
                    newSousFamille();
                }
                else
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The subfamilly of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("sousFamille").InnerText + "\". It has been replaced by \"" + sousFamille.Nom + "\"");

                    article.IdSousFamille = sousFamille.Id;

                    // Generate error when the sousFamille don't belong to the good famille
                    if (!dbManager.existSousFamilleInFamille(article.IdSousFamille, article.IdFamille))
                    {
                        updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information,
                        "Cannot add the article " + article.Reference + " because his family does not match to his sub family");

                        foundMistake = true;
                    }
                    node.SelectSingleNode("sousFamille").InnerText = sousFamille.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
            {
                article.IdSousFamille = sousFamille.Id;
                // Generate error when the sousFamille don't belong to the good famille
                if (!dbManager.existSousFamilleInFamille(article.IdSousFamille, article.IdFamille))
                {
                    updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information,
                        "Cannot add the article " + article.Reference + " because his family does not match to his sub family");

                    foundMistake = true;
                }
            }
            return foundMistake;
        }

        /// <summary>
        /// Insert new subFamily to the database
        /// </summary>
        protected void newSousFamille()
        {
            SousFamilles sousFamille = new SousFamilles();
            sousFamille.IdFamille = article.IdFamille;
            sousFamille.Nom = node.SelectSingleNode("sousFamille").InnerText;
            article.IdSousFamille = dbManager.insertSousFamille(sousFamille); // Insert return the last id of the sousFamille added.
            updateListView(TypeMessage.Success, SubjectMessage.Add_Famille, "The subfamilly " + sousFamille.Nom + " has been created");
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        private void treatMarque()
        {
            Marques marque = dbManager.getMarque(node.SelectSingleNode("marque").InnerText);
            if (marque == null)
            {
                marque = checkSpellingMarques(node.SelectSingleNode("marque").InnerText);
                if (marque == null)
                {
                    newMarque();
                }
                else
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The brand of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("marque").InnerText + "\". It has been replaced by \"" + marque.Nom + "\"");

                    article.IdMarque = marque.Id;
                    node.SelectSingleNode("marque").InnerText = marque.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
                article.IdMarque = marque.Id;
        }

        /// <summary>
        /// Insert new brand to the database
        /// </summary>
        protected void newMarque()
        {
            Marques marque = new Marques();
            marque.Nom = node.SelectSingleNode("marque").InnerText;
            article.IdMarque = dbManager.insertMarque(marque); // Insert return the last id of the brand added.
            updateListView(TypeMessage.Success, SubjectMessage.Add_Famille, "The marque " + marque.Nom + " has been created");
        }

        /// <summary>
        /// Check if the article already exist or not
        /// </summary>
        /// <returns></returns>
        protected bool checkDoubleArticle()
        {
            article = dbManager.getArticle(node.SelectSingleNode("refArticle").InnerText);
            if (article == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compute the distance between tow string
        /// </summary>
        /// <param name="s"> The first string </param>
        /// <param name="t">The second string </param>
        /// <returns> The distance between these two strings </returns>
        protected int distanceLevenshtein(string s, string t)
        {
            s = s.ToLower();
            t = t.ToLower();

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++) {}
            for (int j = 0; j <= m; d[0, j] = j++) {}

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        /// <summary>
        /// Check if the family contains a mistake spelling
        /// </summary>
        /// <param name="name"> The name of the family </param>
        /// <returns> The family fixed or null if no family have been found </returns>
        protected Familles checkSpellingFamilles(string name)
        {
            int bestDistance = 255, tempDistance;
            Familles famille = null;

            List<Familles> listFamille = dbManager.getAllFamilles(); // Retrieve all family of the database

            foreach (Familles oneOfList in listFamille)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    famille = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return famille;
            else
                return null;
        }

        /// <summary>
        /// Check if the sub family contains a mistake spelling
        /// </summary>
        /// <param name="name"> The name of the sub family </param>
        /// <returns> The sub family fixed or null if no sub family have been found </returns>
        protected SousFamilles checkSpellingSousFamilles(string name)
        {
            int bestDistance = 255, tempDistance;
            SousFamilles sousFamille = null;

            List<SousFamilles> listSousFamille = dbManager.getAllSousFamilles(); // Retrieve all SousFamille of the database

            foreach (SousFamilles oneOfList in listSousFamille)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    sousFamille = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return sousFamille;
            else
                return null;
        }

        /// <summary>
        /// Check if the brand contains a mistake spelling
        /// </summary>
        /// <param name="name"> The name of the brand to check </param>
        /// <returns> The brand fixed or null if no brand have been found </returns>
        protected Marques checkSpellingMarques(string name)
        {
            int bestDistance = 255, tempDistance;
            Marques marque = null;

            List<Marques> listMarque = dbManager.getAllMarques(); // Retrieve all brands of the database

            foreach (Marques oneOfList in listMarque)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    marque = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return marque;
            else
                return null;
        }
    }
}