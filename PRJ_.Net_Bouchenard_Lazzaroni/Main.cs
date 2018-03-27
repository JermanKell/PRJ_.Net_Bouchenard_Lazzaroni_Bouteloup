using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ControllerParserXML parser = new ControllerParserXMLAdd("../../Mercure.xml");
            // parser.parse(); PAS ENCORE FONCTIONNEL

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

            updateListViewArticle();
        }

        private void selectXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXml SelectXML = new SelectXml();
            SelectXML.ShowDialog();
           /* CompoTest compo = new CompoTest();
            compo.ShowDialog();*/
        }

        public void initializeListViewArticle()
        {
            //initialise listviewArticle
            listViewArticle.Columns.Add("RefArticle");
            listViewArticle.Columns.Add("Description");
            listViewArticle.Columns.Add("RefSousFamille");
            listViewArticle.Columns.Add("RefMarque");
            listViewArticle.Columns.Add("PrixHT");
            listViewArticle.Columns.Add("Quantite");
        }

        public void updateListViewArticle(string columnsort = "RefArticle", bool ascending = true)
        {
            listViewArticle.Items.Clear();

            string order = "ASC";
            if (!ascending)
            {
                order = "DESC";
            }

            //Req a deplacer
            SQLiteDataAdapter ada = new SQLiteDataAdapter("select * from Articles group by " + columnsort + " order by " + columnsort + " " + order, DBConnection.getInstance().getDataBase());
            DataTable dt = new DataTable();
            ada.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["RefArticle"].ToString());
                listitem.SubItems.Add(dr["Description"].ToString());
                listitem.SubItems.Add(dr["RefSousFamille"].ToString());
                listitem.SubItems.Add(dr["RefMarque"].ToString());
                listitem.SubItems.Add(dr["PrixHT"].ToString());
                listitem.SubItems.Add(dr["Quantite"].ToString());
                listViewArticle.Items.Add(listitem);
            }
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            //Refresh listViewArticle
            if (e.KeyCode == Keys.F5)
            {
                updateListViewArticle();
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
            if (e.KeyCode == Keys.Enter)
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

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listViewArticle_MouseUp(object sender, MouseEventArgs e)
        {
            //clic droit
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("afficher menustrip");
            }
        }
    }
}
