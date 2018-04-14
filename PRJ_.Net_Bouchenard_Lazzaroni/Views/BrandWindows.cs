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
    /// <summary>
    /// View to manage brands
    /// </summary>
    partial class BrandWindows : PRJ_.Net_Bouchenard_Lazzaroni.Views.BaseWindows
    {
        private ControllerView_Brand controller;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public BrandWindows()
        {
            InitializeComponent();
            controller = new ControllerView_Brand();

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

            foreach (KeyValuePair<int, Marques> familles in controller.getAllMarques())
            {
                ListViewItem item = new ListViewItem(new string[] {

                    familles.Value.Id.ToString(),
                    familles.Value.Nom,
                });
                item.Name = familles.Key.ToString();    //Set reference as item name
                listView1.Items.Add(item);
            }
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult dialogResult = MessageBox.Show("Confirmer la supression de marque?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < listView1.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (controller.ExistArticleFromBrand(Convert.ToInt32(listView1.SelectedItems[ILoop].Name)))    //At least one article uses this brand
                        {
                            DialogResult dialogArticle = MessageBox.Show("Au moins un article est associé à la marque <" + listView1.SelectedItems[ILoop].SubItems[1].Text + "> à supprimer.\n Si vous poursuivez, tous les articles de cette marque seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogArticle == DialogResult.Yes)
                            {
                                controller.DeleteElement(listView1.SelectedItems[ILoop].Name);
                            }
                            else
                            {
                                statusStrip.Items[0].Text = "L'opération de suppression de la marque <" + listView1.SelectedItems[ILoop].SubItems[1].Text + "> a été annulée";
                            }

                        }
                        else  //No one article uses this brand
                        {
                            controller.DeleteElement(listView1.SelectedItems[ILoop].Name);
                        }

                    }

                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    listView1.SetSortIcon(GroupColumn, listView1.Sorting);

                    statusStrip.Items[0].Text = "La marque a bien été supprimé de la base";
                }
                catch (Exception ex)
                {
                    statusStrip.Items[0].Text = "Une erreur a empêché la supression de cette marque";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                statusStrip.Items[0].Text = "La supression de la marque a été annulée";
            }

            refreshOwnView();
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            AddUpdateBrand brandWindow = new AddUpdateBrand(controller);
            brandWindow.StartPosition = FormStartPosition.CenterParent;

            if (brandWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La marque a été ajoutée";
                refreshOwnView();
            }
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateBrand brandWindow = new AddUpdateBrand(controller, controller.GetBrand(Convert.ToInt16(listView1.SelectedItems[0].Name)));
            brandWindow.StartPosition = FormStartPosition.CenterParent;

            if (brandWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La marque a été mis à jour";
                refreshOwnView();
            }
        }
    }
}