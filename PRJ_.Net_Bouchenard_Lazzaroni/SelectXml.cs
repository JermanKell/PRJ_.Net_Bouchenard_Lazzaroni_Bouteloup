using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
// A degager après test
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public partial class SelectXml : Form
    {
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
                Stream stream = DocOpen_Window.OpenFile();
            }
           
            if (Update_XML.Checked == true)
                // Ouaish, methode a mettre
            if (Integration_XML.Checked == true)
            {
                //Ouaish
            }


            //foreach(XmlNode xNode in xml.DocumentElement.ChildNodes)


        }
    }
}
