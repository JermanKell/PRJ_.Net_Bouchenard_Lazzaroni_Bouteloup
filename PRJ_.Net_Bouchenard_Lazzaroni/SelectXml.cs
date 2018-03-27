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
                stream = DocOpen_Window.OpenFile();
                lab_FName.Text = DocOpen_Window.SafeFileName;
            }


            //foreach(XmlNode xNode in xml.DocumentElement.ChildNodes)


        }

        private void btnIntegrate_Click(object sender, EventArgs e)
        {
            if (Update_XML.Checked == true) MessageBox.Show("Ca marche 2");
            if (Integration_XML.Checked == true) MessageBox.Show("Ca marche 1");
            //ExempleRequete();
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
