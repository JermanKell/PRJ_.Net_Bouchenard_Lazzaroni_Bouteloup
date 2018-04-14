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
