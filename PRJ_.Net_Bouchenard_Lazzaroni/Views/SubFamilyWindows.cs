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
        private ControllerView_SubFamily Controller;

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public SubFamilyWindows()
        {
            InitializeComponent();
            Controller = new ControllerView_SubFamily();

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
            List<string> ListNameColumnTable = Controller.GetColumnHeader();

            for (int ILoop = 0; ILoop < ListNameColumnTable.Count; ILoop++)
            {
                ColumnHeader ColHdr = new ColumnHeader();
                ColHdr.Name = ListNameColumnTable.ElementAt(ILoop); //Set a ColumnHeader name 
                ColHdr.Text = ListNameColumnTable.ElementAt(ILoop);
                ColHdr.Width = ListView.Size.Width / ListNameColumnTable.Count;
                ListView.Columns.Add(ColHdr);
            }
        }

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        protected override void LoadDataListView()
        {
            ListView.Items.Clear();

            foreach (KeyValuePair<int, SousFamilles> SousFamilles in Controller.GetAllSousFamilles())
            {
                ListViewItem Item = new ListViewItem(new string[] {

                    SousFamilles.Value.Id.ToString(),
                    SousFamilles.Value.IdFamille.ToString(),
                    SousFamilles.Value.Nom,
                });
                Item.Name = SousFamilles.Key.ToString();    //Set reference as item name
                ListView.Items.Add(Item);
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
            AddUpdateSubFamily subFamilyWindow = new AddUpdateSubFamily(Controller);
            subFamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (subFamilyWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La sous famille a été ajoutée";
                RefreshOwnView();
            }
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateSubFamily subFamilyWindow = new AddUpdateSubFamily(Controller, Controller.GetSubFamily(Convert.ToInt16(ListView.SelectedItems[0].Name)));
            subFamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (subFamilyWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La sous famille a été mis à jour";
                RefreshOwnView();
            }
        }
    }
}
