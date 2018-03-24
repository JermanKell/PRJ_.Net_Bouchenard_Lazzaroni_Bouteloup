using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class SousFamilles
    {
        private int id;
        private int idFamille;
        private string nom;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdFamille
        {
            get { return idFamille; }
            set { idFamille = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public void convertDataReaderToSousFamilles(SQLiteDataReader reader)
        {
            if (reader != null)
            {
                id = Convert.ToInt16(reader.GetValue(0));
                idFamille = Convert.ToInt16(reader.GetValue(1));
                nom = reader.GetValue(2).ToString();
            }
        }
    }
}
