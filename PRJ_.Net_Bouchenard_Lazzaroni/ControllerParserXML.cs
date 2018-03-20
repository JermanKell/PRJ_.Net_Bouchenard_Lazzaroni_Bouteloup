using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    abstract class ControllerParserXML
    {
        private XmlDocument xmlDocument;

        abstract public void parse();

        public ControllerParserXML()
        {
            xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load("../../Mercure.xml");
            } catch (Exception e) { MessageBox.Show("Fichier introuvable. \n\n" + e.Message); }
        }

        public void sendSignal()
        {
            // TODO Envoyer un signal à chaque fois que l'on ajoute, modifie, supprime un enregistrement dans la base.
            // En cas d'erreur d'insertion, de modification ou de suppression, envoyer également un signal.
        }
    }
}
