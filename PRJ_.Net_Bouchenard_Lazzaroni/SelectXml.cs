using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
// A degager après test
using System.Xml;


namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public partial class SelectXml : Form
    {
        private Stream stream;
        private string filename;

        public SelectXml()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DocOpen_Window = new OpenFileDialog();
            DocOpen_Window.Filter = "XML Files (.xml) | *.xml";
            DocOpen_Window.FilterIndex = 1;
            DocOpen_Window.Title = "Select an XML File to parse";
            DocOpen_Window.RestoreDirectory = true;
            XmlDocument xml = new XmlDocument();

            if (DocOpen_Window.ShowDialog() == DialogResult.OK)
            {
                //stream = DocOpen_Window.OpenFile(); // Pourquoi tu a besoin d'ouvrir le fichier ? Cela me pose des problèmes quand je veux y appliquer des modifications.
                filename = DocOpen_Window.FileName; // Full path of the file
                lab_FName.Text = DocOpen_Window.SafeFileName;
            }

            //foreach(XmlNode xNode in xml.DocumentElement.ChildNodes)
        }

        private void btnIntegrate_Click(object sender, EventArgs e)
        {
            listView.Items.Clear(); // Remove all items<

            if (lab_FName.Text.CompareTo("") != 0) // If no file has selected
            {
                ControllerParserXML controllerParser;

                if (Update_XML.Checked == true)
                {
                    controllerParser = new ControllerParserXMLUpdate(filename);
                    controllerParser.sendMessageToView += eventReceived;
                    controllerParser.parse();
                }
                if (Integration_XML.Checked == true)
                {
                    controllerParser = new ControllerParserXMLAdd(filename);
                    controllerParser.sendMessageToView += eventReceived;
                    controllerParser.parse();
                }
            }

            //ExempleRequete();
        }

        void eventReceived(object sender, MyEventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem(new[] { e.message, e.type.ToString(), e.subject.ToString()});

            if (e.type == TypeMessage.Success)
                listViewItem.ForeColor = Color.Green;
            else if (e.type == TypeMessage.Warning)
                listViewItem.ForeColor = Color.Brown;
            else if (e.type == TypeMessage.Error)
                listViewItem.ForeColor = Color.Red;
            else
            {
                listViewItem.ForeColor = Color.Black;
                listViewItem.BackColor = Color.Red;
            }

            listView.Items.Add(listViewItem);
            listView.Refresh();

            listView.EnsureVisible(listView.Items.Count - 1); // Auto scroll down
        }

        public void ExempleRequete()
        {
            
            SQLiteConnection db = DBConnection.getInstance().getDataBase();


            string myInsertQuery = "INSERT INTO MARQUES VALUES (1,'UnNom')";
            SQLiteCommand sqCommand = new SQLiteCommand(myInsertQuery, db);
            sqCommand.ExecuteNonQuery();

            //avec prepare
            SQLiteCommand commandPrepare = new SQLiteCommand("INSERT INTO Marques (RefMarque, Nom) VALUES (@refMarque, @nom)", db);
            commandPrepare.Prepare();
            commandPrepare.Parameters.AddWithValue("@refMarque", 2);
            commandPrepare.Parameters.AddWithValue("@nom", "MonNomPrepare");
            commandPrepare.ExecuteNonQuery();

            //récupérer le nom de toutes les tables
            SQLiteCommand select = new SQLiteCommand("SELECT * FROM Marques", db);
            SQLiteDataReader r = select.ExecuteReader();
            String nom = "";
            while (r.Read())
            {
                nom += r["RefMarque"] + " " + r["Nom"] + "\n";
            }
            MessageBox.Show(nom);

            //récupérer le nom de toutes les tables
            SQLiteCommand selectTables = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", db);
            SQLiteDataReader r2 = selectTables.ExecuteReader();
            String nom2 = "";
            while (r2.Read())
            {
                nom2 += r2.GetValue(0).ToString() + "\n";
            }
            db.Close();
            MessageBox.Show(nom2);
        }
    }
}
