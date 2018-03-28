using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Data.SQLite;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    abstract class ControllerParserXML
    {
        protected XmlDocument xmlDocument; // Allow to navigate in the xml file
        protected DBManager dbManager; // Access SQL command
        protected string filename; // Content the path of the file
        protected Articles article;
        protected XmlNode node; // To store the current node when parsing

        abstract public void parse(); // Each child implement his own version of parsing.
        abstract protected void treatFamille(); // Called by parse function. Create Famille if doesn't exist.
        abstract protected void treatSousFamille(); // Called by parse function. Create SousFamille if doesn't exist and link to the article.
        abstract protected void treatMarque(); // Called by parse function. Create Marque if doesn't exist and link to the article.

        public ControllerParserXML(string filename) // Check if the file exist and if xmlDocument is able to load it.
        {
            this.filename = filename;
            dbManager = new DBManager();
            xmlDocument = new XmlDocument();
            article = null;

            try
            {
                xmlDocument.Load(filename); // Load the file into the XMLDocument
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        protected void verifyFile() // Verify the structure of the xml file
        {
            try
            {
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(null, "../../validateXMLFile.xsd"); // Add the xsd to the schema
                xmlDocument.Schemas.Add(schemaSet); // Add the schema to the xml document
                ValidationEventHandler veh = new ValidationEventHandler(sendSignal); // Send event when something goes wrong.
                xmlDocument.Validate(veh); // Run the validation
            } catch (Exception e) { MessageBox.Show(e.Message); }
        }

        protected static void sendSignal(object sender, ValidationEventArgs args)
        {
            MessageBox.Show("Problème");
            //MessageBox.Show(args.Message);
        }
    }
}
