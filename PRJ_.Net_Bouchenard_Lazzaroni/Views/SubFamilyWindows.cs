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
