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
        }
    }
}
