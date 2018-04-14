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
    /// The base view, contain a list view and a status bar.
    /// </summary>
    partial class BaseWindows : Form
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
        virtual protected void InitHeader() { }

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        virtual protected void LoadDataListView() { }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        virtual protected void DeleteObjectListView() { }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        virtual protected void AddObjectListView() { }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        virtual protected void UpdateObjectListView() { }

        /// <summary>
        /// Creates a Hashtable with one entry for each unique textItem value in the specified column 
        /// </summary>
        protected void InitialiseGroupsByColumnListView()
        {
            GroupsListView.Clear();

            //Create a hashtable for each column
            for (int Column = 0; Column < ListView.Columns.Count; Column++)
            {
                Hashtable GroupColumn = new Hashtable();

                // Iterate through the items in myListView
                for (int I = 0; I < ListView.Items.Count; I++)
                {
                    ListViewItem Item = ListView.Items[I];
                    string TextItem = Item.SubItems[Column].Text;

                    // If the groupColumn doesn't already contain a group for the textItem value, 
                    //add a new group using the textItem value for the group header and Hashtable key
                    if (!GroupColumn.Contains(TextItem))
                    {
                        GroupColumn.Add(TextItem, new ListViewGroup(TextItem, HorizontalAlignment.Left));
                    }
                }
                GroupsListView.Add(GroupColumn);
            }
        }

        /// <summary>
        /// Sort column by group
        /// </summary>
        /// <param name="Column"> The column to sort </param>
        protected void SetGroups(int Column)
        {
            ListView.Groups.Clear();

            // Get the Hashtable corresponding to the column
            Hashtable Groups = GroupsListView.ElementAt(Column);

            // Copy the groups for the column in Listgroups
            ListViewGroup[] Listgroups = new ListViewGroup[Groups.Count];
            Groups.Values.CopyTo(Listgroups, 0);

            // Sort the groups and add them to myListView
            Array.Sort(Listgroups, new ListViewGroupSorter(ListView.Sorting));
            ListView.Groups.AddRange(Listgroups);

            // Iterate through the items in myListView, assigning each one to the appropriate group
            for (int I = 0; I < ListView.Items.Count; I++)
            {
                ListViewItem Item = ListView.Items[I];
                string TextItem = Item.SubItems[Column].Text;

                // Assign the item to the matching group
                Item.Group = (ListViewGroup)Groups[TextItem];
            }
        }

        /// <summary>
        /// Sorts ListViewGroup objects by header value
        /// </summary>
        private class ListViewGroupSorter : IComparer
        {
            private SortOrder Order;

            /// <summary>
            /// Stores the sort order
            /// </summary>
            /// <param name="TheOrder"></param>
            public ListViewGroupSorter(SortOrder TheOrder)
            {
                //MessageBox.Show(theOrder.ToString());
                Order = TheOrder;
            }

            /// <summary>
            /// Compares the groups by header value, using the saved sort order to return the correct value
            /// </summary>
            /// <param name="X">Object one to compare</param>
            /// <param name="Y">The second object to compare</param>
            /// <returns>The result</returns>
            public int Compare(object X, object Y)
            {
                int Result;
                double DoubleX, DoubleY;
                if (double.TryParse(((ListViewGroup)X).Header, out DoubleX) && double.TryParse(((ListViewGroup)Y).Header, out DoubleY))
                {
                    if (DoubleX > DoubleY)
                        Result = 1;
                    else
                        Result = -1;
                    // MessageBox.Show("Comparaison number: '" + ((ListViewGroup)x).Header + "' avec '" + ((ListViewGroup)y).Header + "' resultat=" + result);
                }
                else
                {
                    Result = String.Compare(((ListViewGroup)X).Header, ((ListViewGroup)Y).Header);
                    //MessageBox.Show("Comparaison string: '" + ((ListViewGroup)x).Header + "' avec '" + ((ListViewGroup)y).Header + "' resultat=" + result);
                }

                if (Order == SortOrder.Ascending)
                    return Result;
                else
                    return -Result;
            }
        }

        /// <summary>
        /// Refresh the view to be up to date with the database
        /// </summary>
        protected void RefreshOwnView()
        {
            LoadDataListView();

            //Insert in the groupsListView a new hashtable containing all the groups needed for a single column
            InitialiseGroupsByColumnListView();

            RefreshListViewArticle(GroupColumn, ListView.Sorting);
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        protected void RefreshListViewArticle(int ColumnSorted = 0, SortOrder SortOrder = SortOrder.Ascending)
        {
            ListView.Sorting = SortOrder;
            SetGroups(ColumnSorted);
            ListView.SetSortIcon(ColumnSorted, SortOrder);
            ListView.SelectedItems.Clear();
            if (ListView.FocusedItem != null)
            {
                ListView.FocusedItem.Focused = false;
            }
        }

        /// <summary>
        /// Refresh the view when the user press F5
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void BaseWindows_KeyUp(object Sender, KeyEventArgs E)
        {
            //Refresh listViewArticle
            if (E.KeyCode == Keys.F5)
            {
                RefreshListViewArticle();
            }
        }

        /// <summary>
        /// Groups the items using the groups created for the clicked column
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ListView_ColumnClick(object Sender, ColumnClickEventArgs E)
        {
            // Set the sort order to ascending when changing column groups; otherwise, reverse the sort order
            if ((ListView.Sorting == SortOrder.Descending) || (E.Column != GroupColumn))
                ListView.Sorting = SortOrder.Ascending;
            else
                ListView.Sorting = SortOrder.Descending;
            GroupColumn = E.Column;

            // Set the groups to those created for the clicked column
            SetGroups(E.Column);
            ListView.SetSortIcon(E.Column, ListView.Sorting);
        }

        /// <summary>
        /// Modify an article and the user press enter remove when press delete
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ListView_KeyUp(object Sender, KeyEventArgs E)
        {
            //Modifier article
            if (E.KeyCode == Keys.Enter && ListView.SelectedItems.Count != 0)
            {
                UpdateObjectListView();
            }

            //Supprimer article
            if (E.KeyCode == Keys.Delete && ListView.SelectedItems.Count != 0)
            {
                DeleteObjectListView();
            }
        }

        /// <summary>
        /// Print the menu when the user do a right click
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ListView_MouseUp(object Sender, MouseEventArgs E)
        {
            if (E.Button == MouseButtons.Right && ListView.SelectedItems.Count != 0)
            {
                RightClickMenuStrip.Items.Clear();
                RightClickMenuStrip.Items.Add("Ajouter");
                RightClickMenuStrip.Items.Add("Modifier");
                RightClickMenuStrip.Items.Add("Supprimer");
                RightClickMenuStrip.Show(this, new Point(E.X + 14, E.Y + 4));
            }
            else if (E.Button == MouseButtons.Right && ListView.SelectedItems.Count == 0)
            {
                RightClickMenuStrip.Items.Clear();
                RightClickMenuStrip.Items.Add("Ajouter");
                RightClickMenuStrip.Show(this, new Point(E.X + 14, E.Y + 4));
            }
        }

        /// <summary>
        /// To recover the selection of the user when he did a right click
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void RightClickMenuStrip_ItemClicked(object Sender, ToolStripItemClickedEventArgs E)
        {
            switch (E.ClickedItem.Text)
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

        /// <summary>
        /// Resize the window
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ListView_Resize(object Sender, EventArgs E)
        {
            for (int I = 0; I < ListView.Columns.Count; I++)
                ListView.Columns[I].Width = (ListView.Size.Width / ListView.Columns.Count) - 4;
        }

        /// <summary>
        /// Update an article when the user double on each one
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ListView_DoubleClick(object Sender, EventArgs E)
        {
            //Modifier article
            UpdateObjectListView();
        }
    }
}