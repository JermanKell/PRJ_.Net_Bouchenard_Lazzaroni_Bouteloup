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
        private ControllerView_Brand Controller;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public BrandWindows()
        {
            InitializeComponent();
            Controller = new ControllerView_Brand();

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

            for (int i = 0; i < ListNameColumnTable.Count; i++)
            {
                ColumnHeader ColHdr = new ColumnHeader();
                ColHdr.Name = ListNameColumnTable.ElementAt(i); //Set a ColumnHeader name 
                ColHdr.Text = ListNameColumnTable.ElementAt(i);
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

            foreach (KeyValuePair<int, Marques> Marques in Controller.GetAllMarques())
            {
                ListViewItem Item = new ListViewItem(new string[] {

                    Marques.Value.Id.ToString(),
                    Marques.Value.Nom,
                });
                Item.Name = Marques.Key.ToString();    //Set reference as item name
                ListView.Items.Add(Item);
            }
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            DialogResult DialogResult = MessageBox.Show("Confirmer la supression de marque?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                try
                {
                    for (int ILoop = 0; ILoop < ListView.SelectedItems.Count; ILoop++)   //Remove all selected items
                    {
                        if (Controller.ExistArticleFromBrand(Convert.ToInt32(ListView.SelectedItems[ILoop].Name)))    //At least one article uses this brand
                        {
                            DialogResult DialogArticle = MessageBox.Show("Au moins un article est associé à la marque <" + ListView.SelectedItems[ILoop].SubItems[1].Text + "> à supprimer.\n Si vous poursuivez, tous les articles de cette marque seront également supprimés!", "Poursuivre?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (DialogArticle == DialogResult.Yes)
                            {
                                Controller.DeleteElement(ListView.SelectedItems[ILoop].Name);
                            }
                            else
                            {
                                StatusStrip.Items[0].Text = "L'opération de suppression de la marque <" + ListView.SelectedItems[ILoop].SubItems[1].Text + "> a été annulée";
                            }

                        }
                        else  //No one article uses this brand
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
                catch (Exception Ex)
                {
                    StatusStrip.Items[0].Text = "Une erreur a empêché la supression de cette marque";
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            AddUpdateBrand BrandWindow = new AddUpdateBrand(Controller);
            BrandWindow.StartPosition = FormStartPosition.CenterParent;

            if (BrandWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La marque a été ajoutée";
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
            AddUpdateBrand BrandWindow = new AddUpdateBrand(Controller, Controller.GetBrand(Convert.ToInt16(ListView.SelectedItems[0].Name)));
            BrandWindow.StartPosition = FormStartPosition.CenterParent;

            if (BrandWindow.ShowDialog() == DialogResult.OK)
            {
                StatusStrip.Items[0].Text = "La marque a été mis à jour";
                RefreshOwnView();
            }
            else
                StatusStrip.Items[0].Text = "";
        }
    }
}