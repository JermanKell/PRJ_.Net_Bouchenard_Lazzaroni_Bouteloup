using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Represents an article in the database
    /// </summary>
    public class Articles
    {
        public string Description { get; set; }
        public string Reference { get; set; }
        public int IdFamille { get; set; }
        public int IdSousFamille { get; set; }
        public int IdMarque { get; set; }
        public double PrixHT { get; set; }
        public int Quantite { get; set; }

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public Articles() {}

        /// <summary>
        /// Comfort constructor of this class
        /// </summary>
        /// <param name="Reference">The reference of the article</param>
        /// <param name="Description">The description of the article</param>
        /// <param name="IdFamille">The id family of the article</param>
        /// <param name="IdSousFamille">The id sub family of the article</param>
        /// <param name="IdMarque">The brand id of the article </param>
        /// <param name="PrixHT">The price of the article</param>
        /// <param name="Quantite">The quantity of the article</param>
        public Articles(string Reference, string Description, int IdFamille, int IdSousFamille, int IdMarque, double PrixHT, int Quantite)
        {
            this.Reference = Reference;
            this.Description = Description;
            this.IdFamille = IdFamille;
            this.IdSousFamille = IdSousFamille;
            this.IdMarque = IdMarque;
            this.PrixHT = PrixHT;
            this.Quantite = Quantite;
        }

        /// <summary>
        /// Transfort a reader object into Article object
        /// </summary>
        /// <param name="Reader">The reader to convert into article object</param>
        public void ConvertDataReaderToArticles(SQLiteDataReader Reader)
        {
            if (Reader != null)
            {
                Reference = Reader.GetValue(0).ToString();
                Description = Reader.GetValue(1).ToString();
                IdSousFamille = Convert.ToInt32(Reader.GetValue(2));
                IdMarque = Convert.ToInt32(Reader.GetValue(3));
                PrixHT = Convert.ToDouble(Reader.GetValue(4));
                Quantite = Convert.ToInt32(Reader.GetValue(5));
            }
        }
    }
}
