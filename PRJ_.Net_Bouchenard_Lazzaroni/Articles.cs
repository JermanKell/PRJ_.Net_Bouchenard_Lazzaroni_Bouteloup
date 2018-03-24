using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class Articles
    {
        private string description;
        private string reference;
        private int idFamille;
        private int idSousFamille;
        private int idMarque;
        private double prixHT;
        private int quantite;

        public void convertDataReaderToArticles(SQLiteDataReader reader)
        {
            if (reader != null)
            {
                reference = reader.GetValue(0).ToString();
                description = reader.GetValue(1).ToString();
                idSousFamille = Convert.ToInt16(reader.GetValue(2));
                idMarque = Convert.ToInt16(reader.GetValue(3));
                prixHT = Convert.ToDouble(reader.GetValue(4));
                quantite = Convert.ToInt16(reader.GetValue(5));
            }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public int IdFamille
        {
            get { return idFamille; }
            set { idFamille = value; }
        }

        public int IdSousFamille
        {
            get { return idSousFamille; }
            set { idSousFamille = value; }
        }

        public int IdMarque
        {
            get { return idMarque; }
            set { idMarque = value; }
        }

        public double PrixHT
        {
            get { return prixHT; }
            set { prixHT = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set { quantite = value; }
        }
    }
}
