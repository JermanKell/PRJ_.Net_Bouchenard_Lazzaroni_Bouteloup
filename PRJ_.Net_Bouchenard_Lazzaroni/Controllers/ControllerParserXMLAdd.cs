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
                try
                {
                    if (!checkDoubleArticle(node)); // Check if the article already exist
                    {
                        Articles article = new Articles();

                        article.Description = node.SelectSingleNode("description").InnerText;
                        article.Reference = node.SelectSingleNode("refArticle").InnerText;

                        Familles famille = dbManager.getFamille(node.SelectSingleNode("famille").InnerText); // Check if the famille already exist
                        if (famille == null) // Famille does not exist
                        {
                            famille = new Familles();
                            famille.Nom = node.SelectSingleNode("famille").InnerText;
                            article.IdFamille = dbManager.insertFamille(famille); // Insert return the last id of the famille added.
                        }
                        else
                            article.IdFamille = famille.Id;

                        SousFamilles sousFamille = dbManager.getSousFamille(node.SelectSingleNode("sousFamille").InnerText); // Check if the sousFamille already exist
                        if (sousFamille == null) // If the sousFamille does not exist
                        {
                            sousFamille = new SousFamilles();
                            sousFamille.IdFamille = article.IdFamille;
                            sousFamille.Nom = node.SelectSingleNode("sousFamille").InnerText;
                            article.IdSousFamille = dbManager.insertSousFamille(sousFamille); // Insert return the last id of the sousFamille added.
                        }
                        else
                        {
                            // Generate error when the sousFamille don't belong to the good famille
                            if (!dbManager.existSousFamilleInFamille(article.IdSousFamille, article.IdFamille))
                            {
                                // TODO
                                sendSignal(null, null);
                            }
                            article.IdSousFamille = sousFamille.Id;
                        }

                        Marques marque = dbManager.getMarque(node.SelectSingleNode("marque").InnerText);
                        if (marque == null)
                        {
                            marque = new Marques();
                            marque.Nom = node.SelectSingleNode("marque").InnerText;
                            article.IdMarque = dbManager.insertMarque(marque); // Insert return the last id of the marque added.
                        }
                        else
                            article.IdMarque = marque.Id;

                        article.PrixHT = Convert.ToDouble(node.SelectSingleNode("prixHT").InnerText);

                        dbManager.insertArticle(article);
                    }   
                }  
                catch (Exception e) {/* MessageBox.Show(e.Message);*/ }
            }
        }

        public bool checkDoubleArticle(XmlNode node)
        {
            // Check si l'article en question est déjà présent dans la base (vérification avec le champ référence de l'article)
            // Il faut donc vérifier que l'XML contient les même informations que celui présent en base sinon levé une exception (affichage dans la vue).
            // Ne pas oublier de faire l'update sur la quantité
            return false;
        }
    }
}
