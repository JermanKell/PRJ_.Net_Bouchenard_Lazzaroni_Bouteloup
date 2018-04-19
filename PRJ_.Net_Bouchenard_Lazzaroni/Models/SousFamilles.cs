using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public class SousFamilles
    {
        public int Id { get; set; }
        public int IdFamille { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public SousFamilles() { }

        /// <summary>
        /// Comfort constructor of this class
        /// </summary>
        /// <param name="IdFamille">The family id of the sub family</param>
        /// <param name="Nom">The name of the sub family</param>
        public SousFamilles(int IdFamille, string Nom)
        {
            this.IdFamille = IdFamille;
            this.Nom = Nom;
        }

        /// <summary>
        /// Transfort a reader object into sub family object
        /// </summary>
        /// <param name="Reader">The reader to convert into sub family object</param>
        public void ConvertDataReaderToSousFamilles(SQLiteDataReader Reader)
        {
            if (Reader != null)
            {
                Id = Convert.ToInt32(Reader.GetValue(0));
                IdFamille = Convert.ToInt32(Reader.GetValue(1));
                Nom = Reader.GetValue(2).ToString();
            }
        }
    }
}
