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
    /// <summary>
    /// Abstract class - Controller of the XML parser
    /// </summary>
    abstract class ControllerParserXML
    {
        public event EventHandler<MyEventArgs> EventUpdateListView; // Send events to the view (Update ListView)
        public event EventHandler<MyEventArgs> EventUpdateProgressBar; // Send events to the view (Update ProgressBar)
        public event EventHandler<MyEventArgs> EventRangeMaxProgressBar; // Send events to the view (set the max range of the ProgressBar)

        protected XmlDocument XmlDocument; // Allow to navigate in the xml file
        protected DBManager DbManager; // Access SQL command
        protected string Filename; // Content the path of the file
        protected Articles Article;
        protected XmlNode Node; // To store the current node when parsing
        protected MyEventArgs ArgsEvent; // Store args for event
        protected Dictionary<TypeMessage, int> CounterTypeMessage; // To know how many success, warning, error, critical, ...

        /// <summary>
        /// Each child implement his own version of parsing.
        /// </summary>
        abstract public void Parse();

        /// <summary>
        /// Comfort constructor - Init attributes
        /// </summary>
        /// <param name="Filename"> Filename of the xml file gave by the view </param>
        public ControllerParserXML(string Filename)
        {
            this.Filename = Filename;
            ArgsEvent = new MyEventArgs();
            DbManager = new DBManager();
            XmlDocument = new XmlDocument();
            Article = null;

            // Init dictionary. One entry = one enum
            CounterTypeMessage = new Dictionary<TypeMessage, int>();
            foreach (TypeMessage Foo in Enum.GetValues(typeof(TypeMessage)))
                CounterTypeMessage.Add(Foo, 0);
        }

        /// <summary>
        /// Called by this class and childs when they want to update the listView (console log)
        /// </summary>
        /// <param name="Type"> Type of message (success or warning or etc ...) that you want to send </param>
        /// <param name="Subject"> Subject of the message (addArticle, addFamille) </param>
        /// <param name="Message"> The message to send </param>
        protected void UpdateListView(TypeMessage Type, SubjectMessage Subject, string Message)
        {
            ArgsEvent.Message = Message;
            ArgsEvent.Subject = Subject;
            ArgsEvent.Type = Type;

            CounterTypeMessage[Type] += 1; // Increment the counter

            if (EventUpdateListView != null)
                EventUpdateListView(this, ArgsEvent); // Send the event
        }

        /// <summary>
        /// Called by this class and childs when they want to update the progress bar of the view
        /// </summary>
        protected void UpdateProgressBar()
        {
            EventUpdateProgressBar(this, ArgsEvent);
        }

        /// <summary>
        /// Called by childs one time to set the max range of the progress bar. The max range is defined by the number of articles in the XML file.
        /// </summary>
        /// <param name="NodeCount"> Number of articles in the XML file </param>
        protected void UpdateMaxRangeProgressBar(int NodeCount)
        {
            ArgsEvent.MaxRange = NodeCount;
            EventRangeMaxProgressBar(this, ArgsEvent);
        }

        /// <summary>
        /// Load the xml file and verify the structure (just briefly)
        /// </summary>
        protected void LoadDocument()
        {
            XmlDocument.Load(Filename); // Load the file into the XMLDocument
            UpdateListView(TypeMessage.Succès, SubjectMessage.Structure_XML, "Le fichier XML est chargé");
        }

        /// <summary>
        /// Verify deeply of the structure of the XML file is correct (check the order, if not empty, ...)
        /// </summary>
        protected void VerifyFile() // Verify the structure of the xml file
        {
            XmlSchemaSet SchemaSet = new XmlSchemaSet();
            SchemaSet.Add(null, "../../validateXMLFile.xsd"); // Add the xsd to the schema
            XmlDocument.Schemas.Add(SchemaSet); // Add the schema to the xml document
            ValidationEventHandler Veh = new ValidationEventHandler(EventVerifyStructure); // Send event when something goes wrong.
            XmlDocument.Validate(Veh); // Run the validation
            UpdateListView(TypeMessage.Succès, SubjectMessage.Structure_XML, "La structure du fichier XML est validée");
        }

        /// <summary>
        /// Event sent by verifyFile method 
        /// </summary>
        /// <param name="Sender"> Mandatory because receive event </param>
        /// <param name="Args"> Mandatory because receive event </param>
        protected void EventVerifyStructure(object Sender, ValidationEventArgs Args) // Signal send by verifyFile() when the xml structure does not correct.
        {
            throw new System.Exception(Args.Message);
        }

        /// <summary>
        /// Called by childs when they want to add an article to the database
        /// </summary>
        protected void AddArticle()
        {   
            Article = new Articles();

            Article.Description = Node.SelectSingleNode("description").InnerText;
            Article.Reference = Node.SelectSingleNode("refArticle").InnerText;

            TreatFamille();
            if (!TreatSousFamille()) // Check if the subFamily correspond to the good family. If mistake, generate error signal and the article won't be updatd.
            {
                TreatMarque();

                Article.PrixHT = Convert.ToDouble(Node.SelectSingleNode("prixHT").InnerText);
                Article.Quantite = 1;

                DbManager.InsertArticle(Article);
                UpdateListView(TypeMessage.Succès, SubjectMessage.Ajouter_article, "L'article " + Article.Reference + " a été ajouté");
            }
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        private void TreatFamille()
        {
            Familles Famille = DbManager.GetFamille(Node.SelectSingleNode("famille").InnerText); // Check if the famille already exist
            if (Famille == null) // Famille does not exist
            {
                Famille = CheckSpellingFamilles(Node.SelectSingleNode("famille").InnerText);
                if (Famille == null)
                {
                    NewFamille();
                }
                else
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La famille de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("famille").InnerText + "\". Elle a été remplacé par \"" + Famille.Nom + "\"");

                    Article.IdFamille = Famille.Id;
                    Node.SelectSingleNode("famille").InnerText = Famille.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
                Article.IdFamille = Famille.Id;
        }

        /// <summary>
        /// Insert new family to the database
        /// </summary>
        protected void NewFamille()
        {
            Familles Famille = new Familles();
            Famille.Nom = Node.SelectSingleNode("famille").InnerText;
            Article.IdFamille = DbManager.InsertFamille(Famille); // Insert return the last id of the famille added.
            UpdateListView(TypeMessage.Succès, SubjectMessage.Ajouter_famille, "La famille " + Famille.Nom + " a été créée");
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        /// <returns> True if everything is OK, false if the family does not match to the subfamily </returns>
        private bool TreatSousFamille()
        {
            bool FoundMistake = false;

            SousFamilles SousFamille = DbManager.GetSousFamille(Node.SelectSingleNode("sousFamille").InnerText); // Check if the sousFamille already exist
            if (SousFamille == null) // If the sousFamille does not exist
            {
                SousFamille = CheckSpellingSousFamilles(Node.SelectSingleNode("sousFamille").InnerText);
                if (SousFamille == null)
                {
                    NewSousFamille();
                }
                else
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La sous famille de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("sousFamille").InnerText + "\". Elle a été remplacé par \"" + SousFamille.Nom + "\"");

                    Article.IdSousFamille = SousFamille.Id;

                    // Generate error when the sousFamille don't belong to the good famille
                    if (!DbManager.ExistSousFamilleInFamille(Article.IdSousFamille, Article.IdFamille))
                    {
                        UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'ajouter l'article " + Article.Reference + " car sa famille ne correspond pas à la bonne sous famille");

                        FoundMistake = true;
                    }
                    Node.SelectSingleNode("sousFamille").InnerText = SousFamille.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
            {
                Article.IdSousFamille = SousFamille.Id;
                // Generate error when the sousFamille don't belong to the good famille
                if (!DbManager.ExistSousFamilleInFamille(Article.IdSousFamille, Article.IdFamille))
                {
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'ajouter l'article " + Article.Reference + " car sa famille ne correspond pas à la bonne sous famille");

                    FoundMistake = true;
                }
            }
            return FoundMistake;
        }

        /// <summary>
        /// Insert new subFamily to the database
        /// </summary>
        protected void NewSousFamille()
        {
            SousFamilles SousFamille = new SousFamilles();
            SousFamille.IdFamille = Article.IdFamille;
            SousFamille.Nom = Node.SelectSingleNode("sousFamille").InnerText;
            Article.IdSousFamille = DbManager.InsertSousFamille(SousFamille); // Insert return the last id of the sousFamille added.
            UpdateListView(TypeMessage.Succès, SubjectMessage.Ajouter_sous_famille, "La sous famille " + SousFamille.Nom + " a été créée");
        }

        /// <summary>
        /// If not exist, check if spelling mistake, if not, create new one.
        /// </summary>
        private void TreatMarque()
        {
            Marques Marque = DbManager.GetMarque(Node.SelectSingleNode("marque").InnerText);
            if (Marque == null)
            {
                Marque = CheckSpellingMarques(Node.SelectSingleNode("marque").InnerText);
                if (Marque == null)
                {
                    NewMarque();
                }
                else
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La marque de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("marque").InnerText + "\". Elle a été remplacée par \"" + Marque.Nom + "\"");

                    Article.IdMarque = Marque.Id;
                    Node.SelectSingleNode("marque").InnerText = Marque.Nom; // Change the text of the XML to correct the spelling mistake
                }
            }
            else
                Article.IdMarque = Marque.Id;
        }

        /// <summary>
        /// Insert new brand to the database
        /// </summary>
        protected void NewMarque()
        {
            Marques Marque = new Marques();
            Marque.Nom = Node.SelectSingleNode("marque").InnerText;
            Article.IdMarque = DbManager.InsertMarque(Marque); // Insert return the last id of the brand added.
            UpdateListView(TypeMessage.Succès, SubjectMessage.Ajouter_marque, "La marque " + Marque.Nom + " a été créée");
        }

        /// <summary>
        /// Check if the article already exist or not
        /// </summary>
        /// <returns></returns>
        protected bool CheckDoubleArticle()
        {
            Article = DbManager.GetArticle(Node.SelectSingleNode("refArticle").InnerText);
            if (Article == null)
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
        protected int DistanceLevenshtein(string s, string t)
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
        /// <param name="Name"> The name of the family </param>
        /// <returns> The family fixed or null if no family have been found </returns>
        protected Familles CheckSpellingFamilles(string Name)
        {
            int BestDistance = 255, TempDistance;
            Familles Famille = null;

            Dictionary<int, Familles> ListFamille = DbManager.GetAllFamilles(); // Retrieve all family of the database

            foreach (KeyValuePair<int, Familles> Entry in ListFamille)
            {
                TempDistance = DistanceLevenshtein(Name, Entry.Value.Nom); // Compute the distance

                if (TempDistance < BestDistance)
                {
                    BestDistance = TempDistance;
                    Famille = Entry.Value;
                }
            }

            if (BestDistance <= 2) // Here we decide the degree of tolerance
                return Famille;
            else
                return null;
        }

        /// <summary>
        /// Check if the sub family contains a mistake spelling
        /// </summary>
        /// <param name="Name"> The name of the sub family </param>
        /// <returns> The sub family fixed or null if no sub family have been found </returns>
        protected SousFamilles CheckSpellingSousFamilles(string Name)
        {
            int BestDistance = 255, TempDistance;
            SousFamilles SousFamille = null;

            Dictionary<int, SousFamilles> ListSousFamille = DbManager.GetAllSousFamilles(); // Retrieve all SousFamille of the database

            foreach (KeyValuePair<int, SousFamilles> Entry in ListSousFamille)
            {
                TempDistance = DistanceLevenshtein(Name, Entry.Value.Nom); // Compute the distance

                if (TempDistance < BestDistance)
                {
                    BestDistance = TempDistance;
                    SousFamille = Entry.Value;
                }
            }

            if (BestDistance <= 2) // Here we decide the degree of tolerance
                return SousFamille;
            else
                return null;
        }

        /// <summary>
        /// Check if the brand contains a mistake spelling
        /// </summary>
        /// <param name="Name"> The name of the brand to check </param>
        /// <returns> The brand fixed or null if no brand have been found </returns>
        protected Marques CheckSpellingMarques(string Name)
        {
            int BestDistance = 255, TempDistance;
            Marques Marque = null;

            Dictionary<int, Marques> ListMarque = DbManager.GetAllMarques(); // Retrieve all brands of the database

            foreach (KeyValuePair<int, Marques> Entry in ListMarque)
            {
                TempDistance = DistanceLevenshtein(Name, Entry.Value.Nom); // Compute the distance

                if (TempDistance < BestDistance)
                {
                    BestDistance = TempDistance;
                    Marque = Entry.Value;
                }
            }

            if (BestDistance <= 2) // Here we decide the degree of tolerance
                return Marque;
            else
                return null;
        }
    }
}