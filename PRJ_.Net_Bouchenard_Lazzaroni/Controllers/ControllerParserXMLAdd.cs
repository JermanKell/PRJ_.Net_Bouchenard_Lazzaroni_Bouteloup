using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Controller of the XML parser. Called by view when the user want make a new XML integration.
    /// </summary>
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

                updateListView(TypeMessage.Succès, SubjectMessage.Terminé, 
                    "Succès : " + counterTypeMessage[TypeMessage.Succès] + "   Avertissement : " + counterTypeMessage[TypeMessage.Avertissement] + 
                    "   Erreur : " +counterTypeMessage[TypeMessage.Erreur] + "   Critique : " + counterTypeMessage[TypeMessage.Critique]);
            }
            catch (Exception e)
            {
                updateListView(TypeMessage.Critique, SubjectMessage.Structure_XML, e.Message);
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
                    updateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe, 
                        "La description de l'article " + article.Reference + " est \"" 
                        + node.SelectSingleNode("description").InnerText + "\". Elle a été remplacé par \"" + article.Description + "\"");

                    node.SelectSingleNode("sousFamille").InnerText = article.Description; // Change the text of the XML to correct the spelling mistake
                }
                else // String totally different
                {
                    updateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information, 
                        "Impossible d'incrémenter la quantité de l'article " + article.Reference + " car il n'a pas la même description que l'article dans la base de données");

                    error = true;
                }

            // FAMILY
            nom = dbManager.getFamille(id: article.IdFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("famille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("famille").InnerText) <= 2) // Check if it's the same famille
                {
                    updateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La famille de l'article " + article.Reference + " est \""
                        + node.SelectSingleNode("famille").InnerText + "\". Elle a été remplacé par \"" + nom + "\"");

                    node.SelectSingleNode("famille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + article.Reference + " car il n'a pas la même famille que la famille présente dans la base de données");

                    error = true;
                }

            // SUB FAMILY
            nom = dbManager.getSousFamille(id: article.IdSousFamille).Nom;
            if (nom.CompareTo(node.SelectSingleNode("sousFamille").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("sousFamille").InnerText) <= 2) // Check if it's the same sousFamille
                {
                    updateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La sous famille de l'article " + article.Reference + " est \""
                        + node.SelectSingleNode("sousFamille").InnerText + "\". Elle a été remplacée par \"" + nom + "\"");

                    node.SelectSingleNode("sousFamille").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + article.Reference + " car il n'a pas la même sous famille que l'article présent dans la base de données");

                    error = true;
                }

            // BRAND
            nom = dbManager.getMarque(id: article.IdMarque).Nom;
            if (nom.CompareTo(node.SelectSingleNode("marque").InnerText) != 0) // Equals or not
                if (distanceLevenshtein(nom, node.SelectSingleNode("marque").InnerText) <= 2) // Check if it's the same marque
                {
                    updateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La marque de l'article " + article.Reference + " est \""
                        + node.SelectSingleNode("marque").InnerText + "\". Elle a été remplacée par \"" + nom + "\"");
                    node.SelectSingleNode("marque").InnerText = nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    updateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + article.Reference + " car il n'a pas la même marque que l'article présent dans la base de données");
                    error = true;
                }

            if (!error) // If all information are the same than database, increment quantity of the article
            {
                updateListView(TypeMessage.Succès, SubjectMessage.Modifier_article, "La quantité de l'article " + article.Reference + " a été incrémenté");
                dbManager.updateQuantiteArticle(node.SelectSingleNode("refArticle").InnerText);
            }   
        }
    }
}