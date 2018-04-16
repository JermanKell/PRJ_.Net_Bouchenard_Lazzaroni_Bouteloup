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
        private ControllerView_PFamily Controller;

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public FamilyWindows()
        {
            InitializeComponent();
            Controller = new ControllerView_PFamily();

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

            foreach (KeyValuePair<int, Familles> Familles in Controller.GetAllFamilles())
            {
                ListViewItem Item = new ListViewItem(new string[] {

                    Familles.Value.Id.ToString(),
                    Familles.Value.Nom,
                });
                Item.Name = Familles.Key.ToString();    //Set reference as item name
                ListView.Items.Add(Item);
            }
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult DialogResult = MessageBox.Show("Confirmer la supression de famille?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < ListView.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (Controller.ExistArticleFromFamily(Convert.ToInt32(ListView.SelectedItems[ILoop].Name)))    //At least one article uses a subfamily in this family 
                        {
                            DialogResult DialogArticle = MessageBox.Show("Au moins un article est associé à une sous famille de la famille <" + ListView.SelectedItems[ILoop].SubItems[1].Text +"> à supprimer.\n Si vous poursuivez, tous les articles de cette famille seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (DialogArticle == DialogResult.Yes)
                            {
                                Controller.DeleteElement(ListView.SelectedItems[ILoop].Name);
                                StatusStrip.Items[0].Text = "La famille a bien été supprimé de la base";
                            }
                            else
                            {
                                StatusStrip.Items[0].Text = "L'opération de suppression de la famille <" + ListView.SelectedItems[ILoop].SubItems[1].Text + "> a été annulée";
                            }
                        }
                        else  //No one article uses a subfamily in this family
                        {
                            Controller.DeleteElement(ListView.SelectedItems[ILoop].Name);
                            StatusStrip.Items[0].Text = "La famille a bien été supprimé de la base";
                        }
                        
                    }
                        
                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    ListView.SetSortIcon(GroupColumn, ListView.Sorting);
                }
                catch (Exception Ex)
                {
                    StatusStrip.Items[0].Text = "Une erreur a empêché la supression de cette famille";
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                StatusStrip.Items[0].Text = "La supression de la famille a été annulée";
            }

            RefreshOwnView();
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            AddUpdateFamily FamilyWindow = new AddUpdateFamily(Controller);
            FamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (FamilyWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La famille a été ajoutée";
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
            AddUpdateFamily FamilyWindow = new AddUpdateFamily(Controller, Controller.GetFamily(Convert.ToInt32(ListView.SelectedItems[0].Name)));
            FamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (FamilyWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La famille a été mis à jour";
                RefreshOwnView();
            }
            else
                StatusStrip.Items[0].Text = "";
        }
    }
}