using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class ControllerParserXMLUpdate : ControllerParserXML
    {

        /// <summary>
        /// Comfort constructor
        /// </summary>
        /// <param name="filename"> The filename contains the path of the XML file </param>
        public ControllerParserXMLUpdate(string filename) : base(filename)
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

                XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes
                updateMaxRangeProgressBar(nodelist.Count); // Send the max range of the progress bar to the view

                foreach (XmlNode node in nodelist) // for each <article> node
                {
                    this.node = node;

                    if (!checkDoubleArticle()) // Check if the article already exist
                        addArticle();
                    else
                        updateArticle(); // When the article is already exist. Update information to the database

                    updateProgressBar(); //Send an event to the view to increment the progress bar
                }
                xmlDocument.Save(filename); // Apply modification to the document (fix spelling mistake).

                updateListView(TypeMessage.Succès, SubjectMessage.Terminé,
                    "Succès : " + counterTypeMessage[TypeMessage.Succès] + "   Avertissement : " + counterTypeMessage[TypeMessage.Avertissement] +
                    "   Erreur : " + counterTypeMessage[TypeMessage.Erreur] + "   Critique : " + counterTypeMessage[TypeMessage.Critique]);
            }
            catch (Exception e)
            {
                updateListView(TypeMessage.Critique, SubjectMessage.Structure_XML, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update value of the article
        /// </summary>
        private void updateArticle()
        {
            article.Description = node.SelectSingleNode("description").InnerText; // Update the description

            Familles famille = dbManager.getFamille(node.SelectSingleNode("famille").InnerText);
            if (famille == null)
                newFamille();
            else
                article.IdFamille = famille.Id; // Set the new id of the of famille

            SousFamilles sousFamille = dbManager.getSousFamille(node.SelectSingleNode("sousFamille").InnerText);
            if (sousFamille == null)
                newSousFamille();
            else
            {
                if (dbManager.existSousFamilleInFamille(sousFamille.Id, article.IdFamille))
                    article.IdSousFamille = sousFamille.Id; // Set the new id of the sub family
                else
                {
                    // Generate error because a sousFamille don't belong to twice family. (this sub family has already a family)
                    updateListView(TypeMessage.Erreur, SubjectMessage.Modifier_famille,
                        "L'article " + article.Reference + ". Sa famille n'a pas été mis à jour car sa sous famille ne correspond pas avec la nouvelle famille");
                }
            }
                
            Marques marque = dbManager.getMarque(node.SelectSingleNode("marque").InnerText);
            if (marque == null)
                newMarque();
            else
                article.IdMarque = marque.Id; // Set the new id of the brand

            article.PrixHT = Convert.ToDouble(node.SelectSingleNode("prixHT").InnerText); // Update prixHT

            dbManager.updateArticle(article); // Update information to the database
        }
    }
}