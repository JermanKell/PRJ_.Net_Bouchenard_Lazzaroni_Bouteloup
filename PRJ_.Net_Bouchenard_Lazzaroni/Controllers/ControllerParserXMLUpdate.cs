using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Controller of the XML parser. Called by view when the user want make an update of the database
    /// </summary>
    class ControllerParserXMLUpdate : ControllerParserXML
    {

        /// <summary>
        /// Comfort constructor
        /// </summary>
        /// <param name="Filename"> The filename contains the path of the XML file </param>
        public ControllerParserXMLUpdate(string Filename) : base(Filename)
        { }

        /// <summary>
        /// Parse the XML file
        /// </summary>
        public override void Parse()
        {
            try
            {
                LoadDocument();
                VerifyFile();

                XmlNodeList Nodelist = XmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes
                UpdateMaxRangeProgressBar(Nodelist.Count); // Send the max range of the progress bar to the view

                foreach (XmlNode Node in Nodelist) // for each <article> node
                {
                    this.Node = Node;

                    if (!CheckDoubleArticle()) // Check if the article already exist
                        AddArticle();
                    else
                        UpdateArticle(); // When the article is already exist. Update information to the database

                    UpdateProgressBar(); //Send an event to the view to increment the progress bar
                }
                XmlDocument.Save(Filename); // Apply modification to the document (fix spelling mistake).

                UpdateListView(TypeMessage.Succès, SubjectMessage.Terminé,
                    "Succès : " + CounterTypeMessage[TypeMessage.Succès] + "   Avertissement : " + CounterTypeMessage[TypeMessage.Avertissement] +
                    "   Erreur : " + CounterTypeMessage[TypeMessage.Erreur] + "   Critique : " + CounterTypeMessage[TypeMessage.Critique]);
            }
            catch (Exception e)
            {
                UpdateListView(TypeMessage.Critique, SubjectMessage.Structure_XML, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update value of the article
        /// </summary>
        private void UpdateArticle()
        {
            Article.Description = Node.SelectSingleNode("description").InnerText; // Update the description

            Familles Famille = DbManager.GetFamille(Node.SelectSingleNode("famille").InnerText);
            if (Famille == null)
                NewFamille();
            else
                Article.IdFamille = Famille.Id; // Set the new id of the of famille

            SousFamilles SousFamille = DbManager.GetSousFamille(Node.SelectSingleNode("sousFamille").InnerText);
            if (SousFamille == null)
                NewSousFamille();
            else
            {
                if (DbManager.ExistSousFamilleInFamille(SousFamille.Id, Article.IdFamille))
                    Article.IdSousFamille = SousFamille.Id; // Set the new id of the sub family
                else
                {
                    // Generate error because a sousFamille don't belong to twice family. (this sub family has already a family)
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Modifier_famille,
                        "L'article " + Article.Reference + ". Sa famille n'a pas été mis à jour car sa sous famille ne correspond pas avec la nouvelle famille");
                }
            }
                
            Marques Marque = DbManager.GetMarque(Node.SelectSingleNode("marque").InnerText);
            if (Marque == null)
                NewMarque();
            else
                Article.IdMarque = Marque.Id; // Set the new id of the brand

            Article.PrixHT = Convert.ToDouble(Node.SelectSingleNode("prixHT").InnerText); // Update prixHT

            DbManager.UpdateArticle(Article); // Update information to the database
        }
    }
}