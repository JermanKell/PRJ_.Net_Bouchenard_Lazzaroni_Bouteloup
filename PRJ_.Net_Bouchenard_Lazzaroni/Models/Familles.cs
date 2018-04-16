using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public class Familles
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public Familles() { }

        /// <summary>
        /// Comfort constructor of this class
        /// </summary>
        /// <param name="Nom"></param>
        public Familles(string Nom)
        {
            this.Nom = Nom;
        }

        /// <summary>
        /// Transfort a reader object into family object
        /// </summary>
        /// <param name="Reader">The reader to convert into family object</param>
        public void ConvertDataReaderToFamilles(SQLiteDataReader Reader)
        {
            if (Reader != null)
            {
                Id = Convert.ToInt32(Reader.GetValue(0));
                Nom = Reader.GetValue(1).ToString();
            }
        }
    }
}
