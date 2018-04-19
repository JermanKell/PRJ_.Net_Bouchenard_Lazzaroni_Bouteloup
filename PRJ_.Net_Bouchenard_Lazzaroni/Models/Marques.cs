using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public class Marques
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public Marques() { }

        /// <summary>
        /// Comfort constructor of this class
        /// </summary>
        /// <param name="Nom">The name of the brand</param>
        public Marques(string Nom)
        {
            this.Nom = Nom;
        }

        /// <summary>
        /// Transfort a reader object into brand object
        /// </summary>
        /// <param name="Reader">The reader to convert into brand object</param>
        public void ConvertDataReaderToMarques(SQLiteDataReader Reader)
        {
            if (Reader != null)
            {
                Id = Convert.ToInt32(Reader.GetValue(0));
                Nom = Reader.GetValue(1).ToString();
            }
        }
    }
}
