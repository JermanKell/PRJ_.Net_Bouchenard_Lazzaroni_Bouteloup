using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using PRJ_.Net_Bouchenard_Lazzaroni.Views;
using System.Runtime.InteropServices;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// View to manage all articles
    /// </summary>
    partial class Main : BaseWindows
    {
        private ControllerViewArticle ControllerArticles;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Main() : base()
        {
            InitializeComponent();
            ControllerArticles = new ControllerViewArticle();

            InitHeader(); // Init header of the listView
            GroupsListView = new List<Hashtable>();
            RefreshOwnView();
        }

        /// <summary>
        /// Init the header of the listView
        /// </summary>
        protected override void InitHeader()
        {
            //initialise columns
            List<string> listNameColumnTable = ControllerArticles.GetColumnHeader();
            for (int ILoop = 0; ILoop < listNameColumnTable.Count; ILoop++)
            {
                ColumnHeader ColHdr = new ColumnHeader();
                ColHdr.Name = listNameColumnTable.ElementAt(ILoop); //Set a ColumnHeader name 
                ColHdr.Text = listNameColumnTable.ElementAt(ILoop);
                ColHdr.Width = ListView.Size.Width / listNameColumnTable.Count;
                ListView.Columns.Add(ColHdr);
            }
        }

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        protected override void LoadDataListView()
        {
            ListView.Items.Clear();

            foreach (KeyValuePair<string, Articles> Article in ControllerArticles.GetAllArticles())
            {
                ListViewItem Item = new ListViewItem(new string[] {

                    Article.Value.Reference,
                    Article.Value.Description,
                    Article.Value.IdSousFamille.ToString(),
                    Article.Value.IdMarque.ToString(),
                    Article.Value.PrixHT.ToString(),
                    Article.Value.Quantite.ToString()
                });
                Item.Name = Article.Key;    //Set reference as item name
                ListView.Items.Add(Item);
            }
        }

        /// <summary>
        /// When the user want to import an xml file
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ImportationXMLToolStripMenuItem_Click(object Sender, EventArgs E)
        {
            ImportXMLFile SelectXML = new ImportXMLFile();
            SelectXML.ShowDialog();
            RefreshOwnView();
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult DialogResult = MessageBox.Show("Confirmer la supression d'article?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < ListView.SelectedItems.Count; ILoop++)   //Remove all selected items
                        ControllerArticles.DeleteElement(ListView.SelectedItems[ILoop].Name);  //get id refArticle with item name

                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    ListView.SetSortIcon(GroupColumn, ListView.Sorting);
                    StatusStrip.Items[0].Text = "L'article a bien été supprimé";
                }
                catch (Exception Ex)
                {
                    StatusStrip.Items[0].Text = "Une erreur a empêché la supression de cet article";
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                StatusStrip.Items[0].Text = "La supression d'article a été annulée";
            }
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            AddUpdateArticle VA = new AddUpdateArticle(ControllerArticles);

            if (VA.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "L'article a été ajouté";
                RefreshOwnView();
            }
            else
                StatusStrip.Items[0].Text = "";
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateArticle VA = new AddUpdateArticle(ControllerArticles, ControllerArticles.GetArticle(ListView.SelectedItems[0].Name));

            if (VA.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "L'article a été mis à jour";
                RefreshOwnView();
            }
            else
                StatusStrip.Items[0].Text = "";
        }

        /// <summary>
        /// When the user want to print the view for managing family
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FamilleToolStripMenuItem_Click(object Sender, EventArgs E)
        {
            StatusStrip.Items[0].Text = ""; // Reset the message of the status bar

            FamilyWindows FW = new FamilyWindows();
            FW.ShowDialog();

            RefreshOwnView();
        }

        /// When the user want to print the view for managing sub family
        private void SousFamilleToolStripMenuItem_Click(object Sender, EventArgs E)
        {
            StatusStrip.Items[0].Text = ""; // Reset the message of the status bar

            SubFamilyWindows SFW = new SubFamilyWindows();
            SFW.ShowDialog();

            RefreshOwnView();
        }

        /// When the user want to print the view for managing brand
        private void MarqueToolStripMenuItem_Click(object Sender, EventArgs E)
        {
            StatusStrip.Items[0].Text = ""; // Reset the message of the status bar

            BrandWindows BW = new BrandWindows();
            BW.ShowDialog();

            RefreshOwnView();
        }
    }
}
