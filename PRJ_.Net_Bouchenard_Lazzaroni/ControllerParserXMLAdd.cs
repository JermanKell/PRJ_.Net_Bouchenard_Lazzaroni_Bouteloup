using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class ControllerParserXMLAdd : ControllerParserXML
    {
        public ControllerParserXMLAdd(string filename) : base(filename)
        { }

        public override void parse()
        {
            verifyFile();

            XmlNodeList nodelist = xmlDocument.SelectNodes("/materiels/article"); // get all <article> nodes

            foreach (XmlNode node in nodelist) // for each <article> node
            {
                try
                {
                    string description = node.SelectSingleNode("description").InnerText;
                    string refArticle = node.SelectSingleNode("refArticle").InnerText;
                    string marque = node.SelectSingleNode("marque").InnerText;
                    string famille = node.SelectSingleNode("famille").InnerText;
                    string sousFamille = node.SelectSingleNode("sousFamille").InnerText;
                    string prixHT = node.SelectSingleNode("prixHT").InnerText;
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
        }
    }
}
