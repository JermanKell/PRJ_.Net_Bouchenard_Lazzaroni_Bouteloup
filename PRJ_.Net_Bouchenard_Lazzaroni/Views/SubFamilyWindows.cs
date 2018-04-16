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
            DialogResult DialogResult = MessageBox.Show("Confirmer la supression de sous famille?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < ListView.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (Controller.ExistArticleFromSubFamily(Convert.ToInt32(ListView.SelectedItems[ILoop].Name)))    //At least one article uses this sub family
                        {
                            DialogResult dialogArticle = MessageBox.Show("Au moins un article est associé à la sous famille <" + ListView.SelectedItems[ILoop].SubItems[2].Text + "> à supprimer.\n Si vous poursuivez, tous les articles de cette sous famille seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogArticle == DialogResult.Yes)
                            {
                                Controller.DeleteElement(ListView.SelectedItems[ILoop].Name);
                            }
                            else
                            {
                                StatusStrip.Items[0].Text = "L'opération de suppression de la sous famille <" + ListView.SelectedItems[ILoop].SubItems[2].Text + "> a été annulée";
                            }

                        }
                        else  //No one article uses this sub family
                        {
                            Controller.DeleteElement(ListView.SelectedItems[ILoop].Name);
                        }

                    }

                    LoadDataListView();
                    InitialiseGroupsByColumnListView();

                    SetGroups(GroupColumn);
                    ListView.SetSortIcon(GroupColumn, ListView.Sorting);

                    StatusStrip.Items[0].Text = "La marque a bien été supprimé de la base";
                }
                catch (Exception ex)
                {
                    StatusStrip.Items[0].Text = "Une erreur a empêché la supression de cette marque";
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                StatusStrip.Items[0].Text = "La supression de la marque a été annulée";
            }

            RefreshOwnView();
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
            else
                StatusStrip.Items[0].Text = "";
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            AddUpdateSubFamily subFamilyWindow = new AddUpdateSubFamily(Controller, Controller.GetSubFamily(Convert.ToInt32(ListView.SelectedItems[0].Name)));
            subFamilyWindow.StartPosition = FormStartPosition.CenterParent;

            if (subFamilyWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La sous famille a été mis à jour";
                RefreshOwnView();
            }
            else
                StatusStrip.Items[0].Text = "";
        }
    }
}
