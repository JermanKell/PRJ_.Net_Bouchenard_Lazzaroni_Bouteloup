using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public partial class Main : Form
    {

        // Declare a Hashtable array in which to store the groups
        private List<Hashtable> groupsListView;
        // Declare a variable to store the current grouping column
        int groupColumn = 0;

        public Main()
        {
            InitializeComponent();
            initializeListViewArticle();
            //jeu d'essai
            /*SQLiteCommand sql = new SQLiteCommand(
             "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES (@reference, @description, @idSousFamille, @idMarque, @prixHT, @quantite)", DBConnection.getInstance().getDataBase());
            sql.Parameters.AddWithValue("@reference", 10);
            sql.Parameters.AddWithValue("@description", "blabla");
            sql.Parameters.AddWithValue("@idSousFamille", 1);
            sql.Parameters.AddWithValue("@idMarque", 1);
            sql.Parameters.AddWithValue("@prixHT", 30);
            sql.Parameters.AddWithValue("@quantite", 5);
            sql.ExecuteNonQuery();
            SQLiteCommand sql2 = new SQLiteCommand(
            "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES (@reference, @description, @idSousFamille, @idMarque, @prixHT, @quantite)", DBConnection.getInstance().getDataBase());
            sql2.Parameters.AddWithValue("@reference", 9);
            sql2.Parameters.AddWithValue("@description", "dabc");
            sql2.Parameters.AddWithValue("@idSousFamille", 1);
            sql2.Parameters.AddWithValue("@idMarque", 3);
            sql2.Parameters.AddWithValue("@prixHT", 15);
            sql2.Parameters.AddWithValue("@quantite", 10);
            sql2.ExecuteNonQuery();*/
        }

        private void selectXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXml SelectXML = new SelectXml();
            SelectXML.ShowDialog();
        }

        private void initializeListViewArticle()
        {
            //set default sort ascending 
            listViewArticle.Sorting = SortOrder.Ascending;

            DBManager dbm = new DBManager();

            //initialise columns
            List<string> listNameColumnTable = dbm.getNameColumnTable();
            for (int i=0; i < listNameColumnTable.Count; i++)
                listViewArticle.Columns.Add(listNameColumnTable.ElementAt(i), listViewArticle.Size.Width / listNameColumnTable.Count);

            //initialise data
            List<Articles> listArticles = dbm.getAllArticle();
            for(int i=0; i < listArticles.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[] {
                listArticles.ElementAt(i).Reference,
                listArticles.ElementAt(i).Description,
                listArticles.ElementAt(i).IdSousFamille.ToString(),
                listArticles.ElementAt(i).IdMarque.ToString(),
                listArticles.ElementAt(i).PrixHT.ToString(),
                listArticles.ElementAt(i).Quantite.ToString()
                });
                listViewArticle.Items.Add(item);
            }

            // Create the groupsTable array and populate it with one hash table for each column
            groupsListView = new List<Hashtable>();

            for (int column = 0; column < listViewArticle.Columns.Count; column++)
            {
                //Insert in the groupsListView a new hashtable containing all the groups needed for a single column
                groupsListView.Add(CreateGroupsByColumnListView(column));
            }

            // Start with the groups column refArticle
            SetGroups(0);

        }

        // Creates a Hashtable with one entry for each unique textItem value in the specified column
        private Hashtable CreateGroupsByColumnListView(int column)
        {
            Hashtable groupColumn = new Hashtable();

            // Iterate through the items in myListView
            for (int i=0; i < listViewArticle.Items.Count; i++)
            {
                ListViewItem item = listViewArticle.Items[i];
                string textItem = item.SubItems[column].Text;

                // If the groupColumn doesn't already contain a group for the textItem value,
                // add a new group using the textItem value for the group header and Hashtable key
                if (!groupColumn.Contains(textItem))
                {
                    groupColumn.Add(textItem, new ListViewGroup(textItem, HorizontalAlignment.Left));
                }
            }
            return groupColumn;
        }

        // Sets myListView to the groups created for the specified column
        private void SetGroups(int column)
        {
            listViewArticle.Groups.Clear();

            // Get the Hashtable corresponding to the column
            Hashtable groups = groupsListView.ElementAt(column);

            // Copy the groups for the column in Listgroups
            ListViewGroup[] Listgroups = new ListViewGroup[groups.Count];
            groups.Values.CopyTo(Listgroups, 0);

            // Sort the groups and add them to myListView
            Array.Sort(Listgroups, new ListViewGroupSorter(listViewArticle.Sorting));
            listViewArticle.Groups.AddRange(Listgroups);

            // Iterate through the items in myListView, assigning each one to the appropriate group
            for (int i=0; i < listViewArticle.Items.Count; i++)
            {
                ListViewItem item = listViewArticle.Items[i];
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
                
                int result = String.Compare(((ListViewGroup)x).Header, ((ListViewGroup)y).Header);
                //MessageBox.Show("Comparaison: " + ((ListViewGroup)x).Header + " avec " + ((ListViewGroup)y).Header + " resultat="+result);
                if (order == SortOrder.Ascending)
                    return result;
                else
                    return -result;
            }
        }

        // Groups the items using the groups created for the clicked column
        private void listViewArticle_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Set the sort order to ascending when changing column groups; otherwise, reverse the sort order
            if ((listViewArticle.Sorting == SortOrder.Descending) || (e.Column != groupColumn))
                listViewArticle.Sorting = SortOrder.Ascending;
            else
                listViewArticle.Sorting = SortOrder.Descending;
            groupColumn = e.Column;

            // Set the groups to those created for the clicked column
            SetGroups(e.Column);
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            //Refresh listViewArticle
            if (e.KeyCode == Keys.F5)
            {
                listViewArticle.Sorting = SortOrder.Ascending;
                // Set the groups to those created for the clicked column.
                SetGroups(0);

            }
        }

        private void listViewArticle_DoubleClick(object sender, EventArgs e)
        {
            //Modifier article
            VueArticle vA = new VueArticle();
            vA.ShowDialog();
        }

        private void listViewArticle_KeyUp(object sender, KeyEventArgs e)
        {
            //Modifier article
            if (e.KeyCode == Keys.Enter && listViewArticle.SelectedItems.Count != 0)
            {
                VueArticle vA = new VueArticle();
                vA.ShowDialog();
            }

            //Supprimer article
            if (e.KeyCode == Keys.Delete && listViewArticle.SelectedItems.Count != 0)
            {
                MessageBox.Show(listViewArticle.FocusedItem.Index.ToString());
            }
        }

        private void listViewArticle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listViewArticle.SelectedItems.Count != 0)
            {
                rightclickMenuStrip.Items.Clear();
                rightclickMenuStrip.Items.Add("Ajouter");
                rightclickMenuStrip.Items.Add("Modifier");
                rightclickMenuStrip.Items.Add("Supprimer");
                rightclickMenuStrip.Show(this, new Point(e.X+14, e.Y+4));
            }
            else if (e.Button == MouseButtons.Right && listViewArticle.SelectedItems.Count == 0)
            {
                rightclickMenuStrip.Items.Clear();
                rightclickMenuStrip.Items.Add("Ajouter");
                rightclickMenuStrip.Show(this, new Point(e.X+14, e.Y+4));
            }
        }

        private void rightclickMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Text)
            {
                case "Ajouter":
                    MessageBox.Show("ouvrir fenetre ajouter");
                    break;
                case "Modifier":
                    MessageBox.Show("ouvrir fenetre modifier");
                    break;
                case "Supprimer":
                    MessageBox.Show("supprimer article");
                    break;
            }
        }

        private void listViewArticle_Resize(object sender, EventArgs e)
        {
            for(int i=0; i < listViewArticle.Columns.Count; i++)
                listViewArticle.Columns[i].Width = (listViewArticle.Size.Width / listViewArticle.Columns.Count);
        }
    }
}
