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

            XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes

            foreach (XmlNode node in nodelist) // for each <article> node
            {
                this.node = node;

                try
                {
                    if (!checkDoubleArticle(node)) // Check if the article already exist
                    {
                        article = new Articles();

                        article.Description = node.SelectSingleNode("description").InnerText;
                        article.Reference = node.SelectSingleNode("refArticle").InnerText;

                        treatFamille(); // Create new Famille or not if already exist
                        treatSousFamille(); // Same thing for SousFamille
                        treatMarque(); // Same thing for Marque

                        article.PrixHT = Convert.ToDouble(node.SelectSingleNode("prixHT").InnerText);
                        article.Quantite = 1;

                        dbManager.insertArticle(article);
                    }
                    else
                        treatDoubleArticle(node); // When the article is already exist. Check if the information in XML file are the same than database.
                }  
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
        }

        protected override void treatFamille()
        {
            Familles famille = dbManager.getFamille(node.SelectSingleNode("famille").InnerText); // Check if the famille already exist
            if (famille == null) // Famille does not exist
            {
                famille = checkSpellingFamilles(node.SelectSingleNode("famille").InnerText);
                if (famille == null)
                {
                    famille = new Familles();
                    famille.Nom = node.SelectSingleNode("famille").InnerText;
                    article.IdFamille = dbManager.insertFamille(famille); // Insert return the last id of the famille added.
                }
                else
                {
                    // SEND SIGNAL HERE TO INFORM THE VIEW A MISTAKE HAS BEEN DETECTED IN THE XML (SPELLING MISTAKE)
                    article.IdFamille = famille.Id;
                    node.SelectSingleNode("famille").InnerText = famille.Nom; // Change the text of the XML to correct the spelling mistake
                    xmlDocument.Save(filename); // Apply modification to the document.
                }   
            }
            else
                article.IdFamille = famille.Id;
        }

        protected override void treatSousFamille()
        {
            SousFamilles sousFamille = dbManager.getSousFamille(node.SelectSingleNode("sousFamille").InnerText); // Check if the sousFamille already exist
            if (sousFamille == null) // If the sousFamille does not exist
            {
                sousFamille = checkSpellingSousFamilles(node.SelectSingleNode("sousFamille").InnerText);
                if (sousFamille == null)
                {
                    sousFamille = new SousFamilles();
                    sousFamille.IdFamille = article.IdFamille;
                    sousFamille.Nom = node.SelectSingleNode("sousFamille").InnerText;
                    article.IdSousFamille = dbManager.insertSousFamille(sousFamille); // Insert return the last id of the sousFamille added.
                }
                else
                {
                    // SEND SIGNAL HERE TO INFORM THE VIEW A MISTAKE HAS BEEN DETECTED IN THE XML (SPELLING MISTAKE)
                    article.IdSousFamille = sousFamille.Id;
                    // Generate error when the sousFamille don't belong to the good famille
                    if (!dbManager.existSousFamilleInFamille(article.IdSousFamille, article.IdFamille))
                    {
                        // TODO
                        sendSignal(null, null);
                    }
                    node.SelectSingleNode("sousFamille").InnerText = sousFamille.Nom; // Change the text of the XML to correct the spelling mistake
                    xmlDocument.Save(filename); // Apply modification to the document.
                }
                
            }
            else
            {
                article.IdSousFamille = sousFamille.Id;
                // Generate error when the sousFamille don't belong to the good famille
                if (!dbManager.existSousFamilleInFamille(article.IdSousFamille, article.IdFamille))
                {
                    // TODO
                    sendSignal(null, null);
                }
            }
        }

        protected override void treatMarque()
        {
            Marques marque = dbManager.getMarque(node.SelectSingleNode("marque").InnerText);
            if (marque == null)
            {
                marque = checkSpellingMarques(node.SelectSingleNode("marque").InnerText);
                if (marque == null)
                {
                    marque = new Marques();
                    marque.Nom = node.SelectSingleNode("marque").InnerText;
                    article.IdMarque = dbManager.insertMarque(marque); // Insert return the last id of the marque added.
                }
                else
                {
                    // SEND SIGNAL HERE TO INFORM THE VIEW A MISTAKE HAS BEEN DETECTED IN THE XML (SPELLING MISTAKE)
                    article.IdMarque = marque.Id;
                    node.SelectSingleNode("marque").InnerText = marque.Nom; // Change the text of the XML to correct the spelling mistake
                    xmlDocument.Save(filename); // Apply modification to the document.
                }
            }
            else
                article.IdMarque = marque.Id;
        }

        private bool checkDoubleArticle(XmlNode node)
        {
            article = dbManager.getArticle(node.SelectSingleNode("refArticle").InnerText);
            if (article == null)
                return false;
            else
                return true;
        }

        private void treatDoubleArticle(XmlNode node)
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