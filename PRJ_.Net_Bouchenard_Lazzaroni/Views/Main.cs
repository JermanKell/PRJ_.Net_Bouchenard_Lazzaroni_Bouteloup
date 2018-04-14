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
            refreshOwnView();
        }

        /// <summary>
        /// Init the header of the listView
        /// </summary>
        protected override void InitHeader()
        {
            //initialise columns
            List<string> listNameColumnTable = ControllerArticles.getColumnHeader();
            for (int i = 0; i < listNameColumnTable.Count; i++)
            {
                ColumnHeader colHdr = new ColumnHeader();
                colHdr.Name = listNameColumnTable.ElementAt(i); //Set a ColumnHeader name 
                colHdr.Text = listNameColumnTable.ElementAt(i);
                colHdr.Width = listView1.Size.Width / listNameColumnTable.Count;
                listView1.Columns.Add(colHdr);
            }
        }

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        protected override void LoadDataListView()
        {
            listView1.Items.Clear();

            foreach (KeyValuePair<string, Articles> article in ControllerArticles.GetAllArticles())
            {
                ListViewItem item = new ListViewItem(new string[] {

                    article.Value.Reference,
                    article.Value.Description,
                    article.Value.IdSousFamille.ToString(),
                    article.Value.IdMarque.ToString(),
                    article.Value.PrixHT.ToString(),
                    article.Value.Quantite.ToString()
                });
                item.Name = article.Key;    //Set reference as item name
                listView1.Items.Add(item);
            }
        }

        /// <summary>
        /// When the user want to import an xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportationXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXml SelectXML = new SelectXml();
            SelectXML.ShowDialog();
            refreshOwnView();
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult dialogResult = MessageBox.Show("Confirmer la supression d'article?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < listView1.SelectedItems.Count; ILoop++)   //Remove all selected items
                        ControllerArticles.DeleteElement(listView1.SelectedItems[ILoop].Name);  //get id refArticle with item name

                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    listView1.SetSortIcon(GroupColumn, listView1.Sorting);
                    statusStrip.Items[0].Text = "L'article a bien été supprimé";
                }
                catch (Exception ex)
                {
                    statusStrip.Items[0].Text = "Une erreur a empêché la supression de cet article";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                statusStrip.Items[0].Text = "La supression d'article a été annulée";
            }
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            VueArticle VA = new VueArticle(ControllerArticles);

            if (VA.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "L'article a été ajouté";
                refreshOwnView();
            }
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            VueArticle VA = new VueArticle(ControllerArticles, ControllerArticles.GetArticle(listView1.SelectedItems[0].Name));

            if (VA.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "L'article a été mis à jour";
                refreshOwnView();
            }
        }

        /// <summary>
        /// When the user want to print the view for managing family
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void familleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Items[0].Text = ""; // Reset the message of the status bar

            FamilyWindows FW = new FamilyWindows();
            FW.ShowDialog();

            refreshOwnView();
        }

        /// When the user want to print the view for managing sub family
        private void sousFamilleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Items[0].Text = ""; // Reset the message of the status bar

            SubFamilyWindows SFW = new SubFamilyWindows();
            SFW.ShowDialog();

            refreshOwnView();
        }

        /// When the user want to print the view for managing brand
        private void marqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Items[0].Text = ""; // Reset the message of the status bar

            BrandWindows BW = new BrandWindows();
            BW.ShowDialog();

            refreshOwnView();
        }
    }
}
