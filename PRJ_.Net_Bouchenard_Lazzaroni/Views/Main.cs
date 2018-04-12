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
    partial class Main : BaseWindows
    {
        private ControllerViewArticle ControllerArticles;

        public Main() : base()
        {
            InitializeComponent();

            ControllerArticles = new ControllerViewArticle();
            InitHeader();
            LoadDataListView();

            GroupsListView = new List<Hashtable>();

            //Insert in the groupsListView a new hashtable containing all the groups needed for a single column
            InitialiseGroupsByColumnListView();

            RefreshListViewArticle();
        }

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

        private void ImportationXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXml SelectXML = new SelectXml();
            SelectXML.ShowDialog();
        }

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
                    //statusStrip1.Items[0].Text = "L'article a bien été supprimé de la base";
                }
                catch (Exception ex)
                {
                    //statusStrip1.Items[0].Text = "Une erreur a empêché la supression de cet article";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //statusStrip1.Items[0].Text = "La supression d'article a été annulée";
            }
        }

        protected override void AddObjectListView()
        {
            VueArticle VA = new VueArticle();
            VA.StartPosition = FormStartPosition.CenterParent;
            VA.ShowDialog();
        }

        protected override void UpdateObjectListView()
        {
            VueArticle VA = new VueArticle(ControllerArticles.GetArticle(listView1.SelectedItems[0].Name));
            VA.StartPosition = FormStartPosition.CenterParent;
            VA.ShowDialog();
        }

        private void familleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FamilyWindows FW = new FamilyWindows();
            FW.ShowDialog();
        }

        private void sousFamilleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubFamilyWindows SFW = new SubFamilyWindows();
            SFW.ShowDialog();
        }

        private void marqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrandWindows BW = new BrandWindows();
            BW.ShowDialog();
        }
    }
}
