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
        public ControllerParserXMLUpdate(string filename) : base(filename)
        { }

        public override void parse()
        {
            try
            {
                loadDocument();
                verifyFile();

                XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes

                foreach (XmlNode node in nodelist) // for each <article> node
                {
                    this.node = node;

                    if (!checkDoubleArticle()) // Check if the article already exist
                        addArticle();
                    else
                        updateArticle(); // When the article is already exist. Update information to the database
                }
                //xmlDocument.Save(filename); // Apply modification to the document (fix spelling mistake).
            }
            catch (Exception e)
            {
                sendSignal(TypeMessage.Critical, SubjectMessage.Xml_Structure, e.Message);
                throw;
            }
        }

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
                    article.IdSousFamille = sousFamille.Id; // Set the new id of the sousFamille
                else
                {
                    // Generate error because a sousFamille don't belong to twice famille. (this sousFamille has already a famille)
                    sendSignal(TypeMessage.Error, SubjectMessage.Update_Famille,
                        "Article " + article.Reference + ". His familly has not been updated because his subfamily does not match with the new familly");
                }
            }
                
            Marques marque = dbManager.getMarque(node.SelectSingleNode("marque").InnerText);
            if (marque == null)
                newMarque();
            else
                article.IdMarque = marque.Id; // Set the new id of the marque

            article.PrixHT = Convert.ToDouble(node.SelectSingleNode("prixHT").InnerText); // Update prixHT

            dbManager.updateArticle(article); // Update information to the database
        }
    }
}