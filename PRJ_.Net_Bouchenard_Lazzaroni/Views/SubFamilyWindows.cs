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
    /// View to manage all sub family
    /// </summary>
    partial class SubFamilyWindows : PRJ_.Net_Bouchenard_Lazzaroni.Views.BaseWindows
    {
        private ControllerView_SubFamily controller;

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public SubFamilyWindows()
        {
            InitializeComponent();
            controller = new ControllerView_SubFamily();

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

            foreach (KeyValuePair<int, SousFamilles> sousFamilles in controller.getAllSousFamilles())
            {
                ListViewItem item = new ListViewItem(new string[] {

                    sousFamilles.Value.Id.ToString(),
                    sousFamilles.Value.IdFamille.ToString(),
                    sousFamilles.Value.Nom,
                });
                item.Name = sousFamilles.Key.ToString();    //Set reference as item name
                listView1.Items.Add(item);
            }
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult dialogResult = MessageBox.Show("Confirmer la supression de sous famille?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < listView1.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (controller.ExistArticleFromSubFamily(Convert.ToInt32(listView1.SelectedItems[ILoop].Name)))    //At least one article uses this sub family
                        {
                            DialogResult dialogArticle = MessageBox.Show("Au moins un article est associé à la sous famille <" + listView1.SelectedItems[ILoop].SubItems[2].Text + "> à supprimer.\n Si vous poursuivez, tous les articles de cette sous famille seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogArticle == DialogResult.Yes)
                            {
                                controller.DeleteElement(listView1.SelectedItems[ILoop].Name);
                            }
                            else
                            {
                                statusStrip.Items[0].Text = "L'opération de suppression de la sous famille <" + listView1.SelectedItems[ILoop].SubItems[2].Text + "> a été annulée";
                            }

                        }
                        else  //No one article uses this sub family
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
            AddUpdateSubFamily subFamilyWindow = new AddUpdateSubFamily(controller);
            subFamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (subFamilyWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La sous famille a été ajoutée";
                refreshOwnView();
            }
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateSubFamily subFamilyWindow = new AddUpdateSubFamily(controller, controller.GetSubFamily(Convert.ToInt16(listView1.SelectedItems[0].Name)));
            subFamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (subFamilyWindow.ShowDialog() == DialogResult.OK)
            {
                statusStrip.Items[0].Text = "La sous famille a été mis à jour";
                refreshOwnView();
            }
        }
    }
}
