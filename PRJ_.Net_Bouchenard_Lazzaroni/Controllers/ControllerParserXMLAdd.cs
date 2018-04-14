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
        /// <param name="Filename"> The filename contains the path of the XML file </param>
        public ControllerParserXMLAdd(string Filename) : base(Filename)
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
                DbManager.DeleteTables(); // Clear the database

                XmlNodeList Nodelist = XmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes
                UpdateMaxRangeProgressBar(Nodelist.Count); // Send the max range of the progress bar to the view

                foreach (XmlNode Node in Nodelist) // for each <article> node
                {
                    this.Node = Node;

                    if (!CheckDoubleArticle()) // Check if the article already exist
                        AddArticle(); // Add new article
                    else
                        TreatDoubleArticle(); // When the article is already exist. Check if the information in XML file are the same than database.

                    UpdateProgressBar(); // Calculer ici le pourcentage à envoyer à chaque itération
                }
                XmlDocument.Save(Filename); // Apply modification to the document (fix spelling mistake).

                UpdateListView(TypeMessage.Succès, SubjectMessage.Terminé, 
                    "Succès : " + CounterTypeMessage[TypeMessage.Succès] + "   Avertissement : " + CounterTypeMessage[TypeMessage.Avertissement] + 
                    "   Erreur : " +CounterTypeMessage[TypeMessage.Erreur] + "   Critique : " + CounterTypeMessage[TypeMessage.Critique]);
            }
            catch (Exception E)
            {
                UpdateListView(TypeMessage.Critique, SubjectMessage.Structure_XML, E.Message);
            }
        }

        /// <summary>
        /// If the article already exist, check if xml information match to the database information
        /// </summary>
        private void TreatDoubleArticle()
        {
            bool Error = false;
            string Nom;

            // DESCRIPTION
            if (Article.Description.CompareTo(Node.SelectSingleNode("description").InnerText) != 0) // Equals or not
                if (DistanceLevenshtein(Article.Description, Node.SelectSingleNode("description").InnerText) <= 2) // Spelling mistake or not
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe, 
                        "La description de l'article " + Article.Reference + " est \"" 
                        + Node.SelectSingleNode("description").InnerText + "\". Elle a été remplacé par \"" + Article.Description + "\"");

                    Node.SelectSingleNode("sousFamille").InnerText = Article.Description; // Change the text of the XML to correct the spelling mistake
                }
                else // String totally different
                {
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information, 
                        "Impossible d'incrémenter la quantité de l'article " + Article.Reference + " car il n'a pas la même description que l'article dans la base de données");

                    Error = true;
                }

            // FAMILY
            Nom = DbManager.GetFamille(Id: Article.IdFamille).Nom;
            if (Nom.CompareTo(Node.SelectSingleNode("famille").InnerText) != 0) // Equals or not
                if (DistanceLevenshtein(Nom, Node.SelectSingleNode("famille").InnerText) <= 2) // Check if it's the same famille
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La famille de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("famille").InnerText + "\". Elle a été remplacé par \"" + Nom + "\"");

                    Node.SelectSingleNode("famille").InnerText = Nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + Article.Reference + " car il n'a pas la même famille que la famille présente dans la base de données");

                    Error = true;
                }

            // SUB FAMILY
            Nom = DbManager.GetSousFamille(Id: Article.IdSousFamille).Nom;
            if (Nom.CompareTo(Node.SelectSingleNode("sousFamille").InnerText) != 0) // Equals or not
                if (DistanceLevenshtein(Nom, Node.SelectSingleNode("sousFamille").InnerText) <= 2) // Check if it's the same sousFamille
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La sous famille de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("sousFamille").InnerText + "\". Elle a été remplacée par \"" + Nom + "\"");

                    Node.SelectSingleNode("sousFamille").InnerText = Nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + Article.Reference + " car il n'a pas la même sous famille que l'article présent dans la base de données");

                    Error = true;
                }

            // BRAND
            Nom = DbManager.GetMarque(Id: Article.IdMarque).Nom;
            if (Nom.CompareTo(Node.SelectSingleNode("marque").InnerText) != 0) // Equals or not
                if (DistanceLevenshtein(Nom, Node.SelectSingleNode("marque").InnerText) <= 2) // Check if it's the same marque
                {
                    UpdateListView(TypeMessage.Avertissement, SubjectMessage.Erreur_orthographe,
                        "La marque de l'article " + Article.Reference + " est \""
                        + Node.SelectSingleNode("marque").InnerText + "\". Elle a été remplacée par \"" + Nom + "\"");
                    Node.SelectSingleNode("marque").InnerText = Nom; // Change the text of the XML to correct the spelling mistake
                }
                else
                {
                    UpdateListView(TypeMessage.Erreur, SubjectMessage.Mauvaise_information,
                        "Impossible d'incrémenter la quantité de l'article " + Article.Reference + " car il n'a pas la même marque que l'article présent dans la base de données");
                    Error = true;
                }

            if (!Error) // If all information are the same than database, increment quantity of the article
            {
                UpdateListView(TypeMessage.Succès, SubjectMessage.Modifier_article, "La quantité de l'article " + Article.Reference + " a été incrémenté");
                DbManager.UpdateQuantiteArticle(Node.SelectSingleNode("refArticle").InnerText);
            }   
        }
    }
}