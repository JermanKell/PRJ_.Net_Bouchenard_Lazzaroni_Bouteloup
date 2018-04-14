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
    /// View to manage all family
    /// </summary>
    partial class FamilyWindows : PRJ_.Net_Bouchenard_Lazzaroni.Views.BaseWindows
    {
        private ControllerView_PFamily controller;

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public FamilyWindows()
        {
            InitializeComponent();
            controller = new ControllerView_PFamily();

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

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult dialogResult = MessageBox.Show("Confirmer la supression de famille?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < listView1.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (controller.ExistArticleFromFamily(Convert.ToInt32(listView1.SelectedItems[ILoop].Name)))    //At least one article uses a subfamily in this family 
                        {
                            DialogResult dialogArticle = MessageBox.Show("Au moins un article est associé à une sous famille de la famille <" + listView1.SelectedItems[ILoop].SubItems[1].Text +"> à supprimer.\n Si vous poursuivez, tous les articles de cette famille seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogArticle == DialogResult.Yes)
                            {
                                controller.DeleteElement(listView1.SelectedItems[ILoop].Name);
                            }
                            else
                            {
                                statusStrip.Items[0].Text = "L'opération de suppression de la famille <" + listView1.SelectedItems[ILoop].SubItems[1].Text + "> a été annulée";
                            }

                        }
                        else  //No one article uses a subfamily in this family
                        {
                            controller.DeleteElement(listView1.SelectedItems[ILoop].Name);
                        }
                        
                    }
                        
                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    listView1.SetSortIcon(GroupColumn, listView1.Sorting);

                    statusStrip.Items[0].Text = "La famille a bien été supprimé de la base";
                }
                catch (Exception ex)
                {
                    statusStrip.Items[0].Text = "Une erreur a empêché la supression de cette famille";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                statusStrip.Items[0].Text = "La supression de la famille a été annulée";
            }

            refreshOwnView();
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            AddUpdateFamily familyWindow = new AddUpdateFamily(controller);
            familyWindow.StartPosition = FormStartPosition.CenterParent;

            if (familyWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La famille a été ajoutée";
                refreshOwnView();
            }
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateFamily familyWindow = new AddUpdateFamily(controller, controller.GetFamily(Convert.ToInt16(listView1.SelectedItems[0].Name)));
            familyWindow.StartPosition = FormStartPosition.CenterParent;

            if (familyWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La famille a été mis à jour";
                refreshOwnView();
            }   
        }
    }
}