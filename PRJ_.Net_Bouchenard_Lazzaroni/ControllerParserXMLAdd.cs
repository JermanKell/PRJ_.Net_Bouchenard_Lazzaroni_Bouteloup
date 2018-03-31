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
        public ControllerParserXMLAdd(string filename) : base(filename)
        { }

        public override void parse()
        {
            verifyFile();
            dbManager.deleteTables(); // Clear the database

            XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes

            foreach (XmlNode node in nodelist) // for each <article> node
            {
                this.node = node;

                try
                {
                    if (!checkDoubleArticle()) // Check if the article already exist
                        addArticle(); // Add new article
                    else
                        treatDoubleArticle(); // When the article is already exist. Check if the information in XML file are the same than database.
                }  
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
        }

        private void treatDoubleArticle()
        {
            bool error = false;
            string nom;

            // DESCRIPTION
            if (article.Description.CompareTo(node.SelectSingleNode("description").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(article.Description, node.SelectSingleNode("description").InnerText) <= 2) // Spelling mistake or not
                {
                    // SEND SIGNAL SPELLING MISTAKE IN DESCRIPTION
                    node.SelectSingleNode("sousFamille").InnerText = article.Description; // Change the text of the XML to correct the spelling mistake
                }
                else // String totally different
                {
                    // SEND SIGNAL WRONG DESCRIPTION
                    error = true;
                }

            // FAMILLE
            nom = dbManager.getFamille(id: article.IdFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("famille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("famille").InnerText) <= 2) // Check if it's the same famille
                {
                    // SEND SIGNAL SPELLING MISTAKE IN DESCRIPTION
                    node.SelectSingleNode("famille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    // SEND SIGNAL WRONG DESCRIPTION
                    error = true;
                }

            // SOUSFAMILLE
            nom = dbManager.getSousFamille(id: article.IdSousFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("sousFamille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("sousFamille").InnerText) <= 2) // Check if it's the same sousFamille
                {
                    // SEND SIGNAL SPELLING MISTAKE IN DESCRIPTION
                    node.SelectSingleNode("sousFamille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    // SEND SIGNAL WRONG DESCRIPTION
                    error = true;
                }

            // MARQUE
            nom = dbManager.getMarque(id: article.IdMarque).Nom;
            if (nom.CompareTo(node.SelectSingleNode("marque").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("marque").InnerText) <= 2) // Check if it's the same marque
                {
                    // SEND SIGNAL SPELLING MISTAKE IN DESCRIPTION
                    node.SelectSingleNode("marque").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    // SEND SIGNAL WRONG DESCRIPTION
                    error = true;
                }

            if (!error) // If all information are the same than database, increment quantity of the article
                dbManager.updateQuantiteArticle(node.SelectSingleNode("refArticle").InnerText);

            xmlDocument.Save(filename); // Apply modification to the document
        }
    }
}