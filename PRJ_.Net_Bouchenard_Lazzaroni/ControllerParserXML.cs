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

        // Compute the distance between tow string
        protected int distanceLevenshtein(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        protected Familles checkSpellingFamilles(string name)
        {
            int bestDistance = 255, tempDistance;
            Familles famille = null;

            List<Familles> listFamille = dbManager.getAllFamilles(); // Retrieve all famille of the database

            foreach (Familles oneOfList in listFamille)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    famille = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return famille;
            else
                return null;
        }

        protected SousFamilles checkSpellingSousFamilles(string name)
        {
            int bestDistance = 255, tempDistance;
            SousFamilles sousFamille = null;

            List<SousFamilles> listSousFamille = dbManager.getAllSousFamilles(); // Retrieve all SousFamille of the database

            foreach (SousFamilles oneOfList in listSousFamille)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    sousFamille = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return sousFamille;
            else
                return null;
        }

        protected Marques checkSpellingMarques(string name)
        {
            int bestDistance = 255, tempDistance;
            Marques marque = null;

            List<Marques> listMarque = dbManager.getAllMarques(); // Retrieve all Marques of the database

            foreach (Marques oneOfList in listMarque)
            {
                tempDistance = distanceLevenshtein(name, oneOfList.Nom); // Compute the distance

                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    marque = oneOfList;
                }
            }

            if (bestDistance <= 2) // Here we decide the degree of tolerance
                return marque;
            else
                return null;
        }
    }
}
