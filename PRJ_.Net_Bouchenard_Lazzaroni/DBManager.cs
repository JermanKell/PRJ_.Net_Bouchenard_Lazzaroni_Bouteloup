using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class DBManager
    {
        private SQLiteConnection conn;

        public DBManager()
        {
            conn = DBConnection.getInstance().getDataBase();
        }

        public int insertArticle(Articles article)
        {
            SQLiteCommand sql = new SQLiteCommand(
                "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite)" +
                " VALUES (@reference, @description, @idSousFamille, @idMarque, @prixHT, @quantite)", conn);
            sql.Parameters.AddWithValue("@reference", article.Reference);
            sql.Parameters.AddWithValue("@description", article.Description);
            sql.Parameters.AddWithValue("@idSousFamille", article.IdSousFamille);
            sql.Parameters.AddWithValue("@idMarque", article.IdMarque);
            sql.Parameters.AddWithValue("@prixHT", article.PrixHT);
            sql.Parameters.AddWithValue("@quantite", article.Quantite);

            try
            {
                sql.ExecuteNonQuery();
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertFamille(Familles famille)
        {
            // TODO
            return -1;
        }

        public int insertSousFamille(SousFamilles sousFamille)
        {
            // TODO
            return -1;
        }

        public int insertMarque(Marques marque)
        {
            // TODO
            return -1;
        }

        public Articles getArticle(string reference)
        {
            // TODO
            return null;
        }

        public Familles getFamille(string name)
        {
            // TODO
            return null;
        }

        public SousFamilles getSousFamille(string name)
        {
            // TODO
            return null;
        }

        public Marques getMarque(string name)
        {
            // TODO
            return null;
        }

        public bool existSousFamilleInFamille(int idFamille, int idSousFamille)
        {
            // TODO
            return true;
        }
    }
}
