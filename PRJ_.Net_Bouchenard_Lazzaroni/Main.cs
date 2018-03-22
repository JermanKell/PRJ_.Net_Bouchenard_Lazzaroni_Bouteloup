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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ControllerParserXML parser = new ControllerParserXMLAdd("../../Mercure.xml");
            //parser.parse();
        }

        private void selectXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXml SelectXML = new SelectXml();
            SelectXML.ShowDialog();
           /* CompoTest compo = new CompoTest();
            compo.ShowDialog();*/
        }
    }
}
