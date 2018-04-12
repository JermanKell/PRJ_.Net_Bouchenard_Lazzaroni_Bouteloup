using PRJ_.Net_Bouchenard_Lazzaroni.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    partial class FamilyWindows : PRJ_.Net_Bouchenard_Lazzaroni.Views.BaseWindows
    {
        private ControllerView_PFamily controller = new ControllerView_PFamily();

        public FamilyWindows()
        {
            InitializeComponent();

            controller = new ControllerView_PFamily();
            InitHeader(); // Init header of the listView
            LoadDataListView();

            GroupsListView = new List<Hashtable>();

            //Insert in the groupsListView a new hashtable containing all the groups needed for a single column
            InitialiseGroupsByColumnListView();

            RefreshListViewArticle();
        }

        protected override void InitHeader()
        {
            //initialise columns
            List<string> listNameColumnTable = controller.getColumnHeader();

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

            foreach (KeyValuePair<int, Familles> familles in controller.getAllFamilles())
            {
                ListViewItem item = new ListViewItem(new string[] {

                    familles.Value.Id.ToString(),
                    familles.Value.Nom,
                });
                item.Name = familles.Key.ToString();    //Set reference as item name
                listView1.Items.Add(item);
            }
        }

        protected override void DeleteObjectListView()
        {
            DialogResult dialogResult = MessageBox.Show("Confirmer la supression d'article?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < listView1.SelectedItems.Count; ILoop++)   //Remove all selected items
                        controller.DeleteElement(listView1.SelectedItems[ILoop].Name);  //get id refArticle with item name

                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    listView1.SetSortIcon(GroupColumn, listView1.Sorting);
                    statusStrip1.Items[0].Text = "L'article a bien été supprimé de la base";
                }
                catch (Exception ex)
                {
                    statusStrip1.Items[0].Text = "Une erreur a empêché la supression de cet article";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                statusStrip1.Items[0].Text = "La supression d'article a été annulée";
            }
        }

        protected override void AddObjectListView()
        {
            /*VueArticle VA = new VueArticle();
            VA.StartPosition = FormStartPosition.CenterParent;
            VA.ShowDialog();*/
        }

        protected override void UpdateObjectListView()
        {
            //VueArticle VA = new VueArticle(DictionaryArticles[listView1.SelectedItems[0].Name]);
            //VA.StartPosition = FormStartPosition.CenterParent;
            //VA.ShowDialog();
        }
    }
}
