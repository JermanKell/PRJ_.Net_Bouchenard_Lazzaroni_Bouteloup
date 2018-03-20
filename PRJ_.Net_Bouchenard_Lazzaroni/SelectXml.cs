using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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


        }
    }
}
