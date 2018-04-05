using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class ControllerParserXMLAdd : ControllerParserXML
    {

        /// <summary>
        /// Comfort constructor
        /// </summary>
        /// <param name="filename"> The filename contains the path of the XML file </param>
        public ControllerParserXMLAdd(string filename) : base(filename)
        { }

        /// <summary>
        /// Parse the XML file
        /// </summary>
        public override void parse()
        {
            try
            {
                loadDocument();
                verifyFile();
                dbManager.deleteTables(); // Clear the database

                XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes
                updateMaxRangeProgressBar(nodelist.Count); // Send the max range of the progress bar to the view

                foreach (XmlNode node in nodelist) // for each <article> node
                {
                    this.node = node;

                    if (!checkDoubleArticle()) // Check if the article already exist
                        addArticle(); // Add new article
                    else
                        treatDoubleArticle(); // When the article is already exist. Check if the information in XML file are the same than database.

                    updateProgressBar(); // Calculer ici le pourcentage à envoyer à chaque itération
                }
                xmlDocument.Save(filename); // Apply modification to the document (fix spelling mistake).

                updateListView(TypeMessage.Success, SubjectMessage.Finish, 
                    "Success : " + counterTypeMessage[TypeMessage.Success] + "   Warning : " + counterTypeMessage[TypeMessage.Warning] + 
                    "   Error : " +counterTypeMessage[TypeMessage.Error] + "   Critical : " + counterTypeMessage[TypeMessage.Critical]);
            }
            catch (Exception e)
            {
                updateListView(TypeMessage.Critical, SubjectMessage.Xml_Structure, e.Message);
            }
        }

        /// <summary>
        /// If the article already exist, check if xml information match to the database information
        /// </summary>
        private void treatDoubleArticle()
        {
            bool error = false;
            string nom;

            // DESCRIPTION
            if (article.Description.CompareTo(node.SelectSingleNode("description").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(article.Description, node.SelectSingleNode("description").InnerText) <= 2) // Spelling mistake or not
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake, 
                        "The description of the article " + article.Reference + " is \"" 
                        + node.SelectSingleNode("description").InnerText + "\". It has been replaced by \"" + article.Description + "\"");

                    node.SelectSingleNode("sousFamille").InnerText = article.Description; // Change the text of the XML to correct the spelling mistake
                }
                else // String totally different
                {
                    updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information, 
                        "Cannot increment the quantity of the article " + article.Reference + " because it doesn't have the same description than the article in the database");

                    error = true;
                }

            // FAMILY
            nom = dbManager.getFamille(id: article.IdFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("famille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("famille").InnerText) <= 2) // Check if it's the same famille
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The familly of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("famille").InnerText + "\". It has been replaced by \"" + nom + "\"");

                    node.SelectSingleNode("famille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information,
                        "Cannot increment the quantity of the article " + article.Reference + " because it doesn't have the same familly than the article in the database");

                    error = true;
                }

            // SUB FAMILY
            nom = dbManager.getSousFamille(id: article.IdSousFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("sousFamille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("sousFamille").InnerText) <= 2) // Check if it's the same sousFamille
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The subfamily of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("sousFamille").InnerText + "\". It has been replaced by \"" + nom + "\"");

                    node.SelectSingleNode("sousFamille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information,
                        "Cannot increment the quantity of the article " + article.Reference + " because it doesn't have the same subfamily than the article in the database");

                    error = true;
                }

            // BRAND
            nom = dbManager.getMarque(id: article.IdMarque).Nom;
            if (nom.CompareTo(node.SelectSingleNode("marque").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("marque").InnerText) <= 2) // Check if it's the same marque
                {
                    updateListView(TypeMessage.Warning, SubjectMessage.Spelling_Mistake,
                        "The marque of the article " + article.Reference + " is \""
                        + node.SelectSingleNode("marque").InnerText + "\". It has been replaced by \"" + nom + "\"");
                    node.SelectSingleNode("marque").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Error, SubjectMessage.Wrong_Information,
                        "Cannot increment the quantity of the article " + article.Reference + " because it doesn't have the same marque than the article in the database");
                    error = true;
                }

            if (!error) // If all information are the same than database, increment quantity of the article
            {
                updateListView(TypeMessage.Success, SubjectMessage.Update_Article, "The quantity of the article " + article.Reference + " has been incremented");
                dbManager.updateQuantiteArticle(node.SelectSingleNode("refArticle").InnerText);
            }   
        }
    }
}