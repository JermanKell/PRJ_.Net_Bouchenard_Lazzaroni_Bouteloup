using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class Marques
    {
        private int id;
        private string nom;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public void convertDataReaderToMarques(SQLiteDataReader reader)
        {
            if (reader != null)
            {
                id = Convert.ToInt16(reader.GetValue(0));
                nom = reader.GetValue(1).ToString();
            }
        }
    }
}
