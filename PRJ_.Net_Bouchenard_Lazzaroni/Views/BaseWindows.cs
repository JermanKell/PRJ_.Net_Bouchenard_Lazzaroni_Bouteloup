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
    [TypeDescriptionProvider(typeof(AbstractFormDescriptionProvider<BaseWindows, Form>))]
    abstract partial class BaseWindows : Form
    {
        // Declare a Hashtable array in which to store the groups
        protected List<Hashtable> GroupsListView;
        // Declare a variable to store the current grouping column
        protected int GroupColumn = 0;

        /// <summary>
        /// Constructor of this abstract class.
        /// </summary>
        public BaseWindows()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init the header of the listView
        /// </summary>
        abstract protected void InitHeader();

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        abstract protected void LoadDataListView();

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        abstract protected void DeleteObjectListView();

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        abstract protected void AddObjectListView();

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        abstract protected void UpdateObjectListView();

        // Creates a Hashtable with one entry for each unique textItem value in the specified column
        protected void InitialiseGroupsByColumnListView()
        {
            GroupsListView.Clear();

            //Create a hashtable for each column
            for (int column = 0; column < listView1.Columns.Count; column++)
            {
                Hashtable groupColumn = new Hashtable();

                // Iterate through the items in myListView
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    ListViewItem item = listView1.Items[i];
                    string textItem = item.SubItems[column].Text;

                    // If the groupColumn doesn't already contain a group for the textItem value, 
                    //add a new group using the textItem value for the group header and Hashtable key
                    if (!groupColumn.Contains(textItem))
                    {
                        groupColumn.Add(textItem, new ListViewGroup(textItem, HorizontalAlignment.Left));
                    }
                }
                GroupsListView.Add(groupColumn);
            }
        }

        protected void SetGroups(int column)
        {
            listView1.Groups.Clear();

            // Get the Hashtable corresponding to the column
            Hashtable groups = GroupsListView.ElementAt(column);

            // Copy the groups for the column in Listgroups
            ListViewGroup[] Listgroups = new ListViewGroup[groups.Count];
            groups.Values.CopyTo(Listgroups, 0);

            // Sort the groups and add them to myListView
            Array.Sort(Listgroups, new ListViewGroupSorter(listView1.Sorting));
            listView1.Groups.AddRange(Listgroups);

            // Iterate through the items in myListView, assigning each one to the appropriate group
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem item = listView1.Items[i];
                string textItem = item.SubItems[column].Text;

                // Assign the item to the matching group
                item.Group = (ListViewGroup)groups[textItem];
            }
        }

        // Sorts ListViewGroup objects by header value
        private class ListViewGroupSorter : IComparer
        {
            private SortOrder order;

            // Stores the sort order.
            public ListViewGroupSorter(SortOrder theOrder)
            {
                //MessageBox.Show(theOrder.ToString());
                order = theOrder;
            }

            // Compares the groups by header value, using the saved sort order to return the correct value
            public int Compare(object x, object y)
            {
                int result;
                double DoubleX, DoubleY;
                if (double.TryParse(((ListViewGroup)x).Header, out DoubleX) && double.TryParse(((ListViewGroup)y).Header, out DoubleY))
                {
                    if (DoubleX > DoubleY)
                        result = 1;
                    else
                        result = -1;
                    // MessageBox.Show("Comparaison number: '" + ((ListViewGroup)x).Header + "' avec '" + ((ListViewGroup)y).Header + "' resultat=" + result);
                }
                else
                {
                    result = String.Compare(((ListViewGroup)x).Header, ((ListViewGroup)y).Header);
                    //MessageBox.Show("Comparaison string: '" + ((ListViewGroup)x).Header + "' avec '" + ((ListViewGroup)y).Header + "' resultat=" + result);
                }

                if (order == SortOrder.Ascending)
                    return result;
                else
                    return -result;
            }
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        protected void RefreshListViewArticle()
        {
            listView1.Sorting = SortOrder.Ascending;
            SetGroups(0);
            listView1.SetSortIcon(0, SortOrder.Ascending);
            listView1.SelectedItems.Clear();
            if (listView1.FocusedItem != null)
            {
                listView1.FocusedItem.Focused = false;
            }
        }

        private void BaseWindows_DoubleClick(object sender, EventArgs e)
        {
            //Modifier article
            UpdateObjectListView();
        }

        private void BaseWindows_KeyUp(object sender, KeyEventArgs e)
        {
            //Refresh listViewArticle
            if (e.KeyCode == Keys.F5)
            {
                RefreshListViewArticle();
            }
        }

        // Groups the items using the groups created for the clicked column
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Set the sort order to ascending when changing column groups; otherwise, reverse the sort order
            if ((listView1.Sorting == SortOrder.Descending) || (e.Column != GroupColumn))
                listView1.Sorting = SortOrder.Ascending;
            else
                listView1.Sorting = SortOrder.Descending;
            GroupColumn = e.Column;

            // Set the groups to those created for the clicked column
            SetGroups(e.Column);
            listView1.SetSortIcon(e.Column, listView1.Sorting);
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            //Modifier article
            if (e.KeyCode == Keys.Enter && listView1.SelectedItems.Count != 0)
            {
                UpdateObjectListView();
            }

            //Supprimer article
            if (e.KeyCode == Keys.Delete && listView1.SelectedItems.Count != 0)
            {
                DeleteObjectListView();
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listView1.SelectedItems.Count != 0)
            {
                rightClickMenuStrip.Items.Clear();
                rightClickMenuStrip.Items.Add("Ajouter");
                rightClickMenuStrip.Items.Add("Modifier");
                rightClickMenuStrip.Items.Add("Supprimer");
                rightClickMenuStrip.Show(this, new Point(e.X + 14, e.Y + 4));
            }
            else if (e.Button == MouseButtons.Right && listView1.SelectedItems.Count == 0)
            {
                rightClickMenuStrip.Items.Clear();
                rightClickMenuStrip.Items.Add("Ajouter");
                rightClickMenuStrip.Show(this, new Point(e.X + 14, e.Y + 4));
            }
        }

        private void rightClickMenuStrip_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Ajouter":
                    AddObjectListView();
                    break;
                case "Modifier":
                    UpdateObjectListView();
                    break;
                case "Supprimer":
                    DeleteObjectListView();
                    break;
            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Columns.Count; i++)
                listView1.Columns[i].Width = (listView1.Size.Width / listView1.Columns.Count) - 4;
        }
    }
}