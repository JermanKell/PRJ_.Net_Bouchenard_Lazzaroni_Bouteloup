using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

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
                "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES (@reference, @description, @idSousFamille, @idMarque, @prixHT, @quantite)", conn);
            sql.Parameters.AddWithValue("@reference", article.Reference);
            sql.Parameters.AddWithValue("@description", article.Description);
            sql.Parameters.AddWithValue("@idSousFamille", article.IdSousFamille);
            sql.Parameters.AddWithValue("@idMarque", article.IdMarque);
            sql.Parameters.AddWithValue("@prixHT", article.PrixHT);
            sql.Parameters.AddWithValue("@quantite", article.Quantite);

            try
            {
                sql.ExecuteNonQuery();
                return Convert.ToInt16(conn.LastInsertRowId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertFamille(Familles famille)
        {
            SQLiteCommand sql = new SQLiteCommand("INSERT INTO Familles (RefFamille, Nom) VALUES (NULL, @nom)", conn);
            sql.Parameters.AddWithValue("@nom", famille.Nom);

            try
            {
                sql.ExecuteNonQuery();
                return Convert.ToInt16(conn.LastInsertRowId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertSousFamille(SousFamilles sousFamille)
        {
            SQLiteCommand sql = new SQLiteCommand(
                "INSERT INTO SousFamilles (RefSousFamille, RefFamille, nom) VALUES (NULL, @refFamille, @nom)", conn);
            sql.Parameters.AddWithValue("@refFamille", sousFamille.IdFamille);
            sql.Parameters.AddWithValue("@nom", sousFamille.Nom);

            try
            {
                sql.ExecuteNonQuery();
                return Convert.ToInt16(conn.LastInsertRowId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertMarque(Marques marque)
        {
            SQLiteCommand sql = new SQLiteCommand("INSERT INTO Marques (RefMarque, nom) VALUES (NULL, @nom)", conn);
            sql.Parameters.AddWithValue("@nom", marque.Nom);

            try
            {
                sql.ExecuteNonQuery();
                return Convert.ToInt16(conn.LastInsertRowId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Articles getArticle(string reference)
        {
            Articles article = new Articles();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Articles WHERE RefArticle = @reference", conn);
            sql.Parameters.AddWithValue("@reference", reference);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                article.convertDataReaderToArticles(reader); // Set attributes to the article thanks to the reader
                article.IdFamille = getSousFamille(Convert.ToInt16(reader.GetValue(2))).IdFamille; // Get the Famille linked to the sousFamille to return a complete article
                return article;
            }
            else
                return null;
        }

       public List<Articles> getAllArticle(string columnsort = "RefArticle", bool ascending = true)
        {
            string order = "ASC";
            if (!ascending)
            {
                order = "DESC";
            }

            List<Articles> listArticles = new List<Articles>();

            SQLiteCommand sql = new SQLiteCommand("select * from Articles order by " + columnsort + " " + order, conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Articles article = new Articles();
                article.convertDataReaderToArticles(reader); // Set attributes to the article thanks to the reader
                listArticles.Add(article);
            }
            return listArticles;
        }

        public Familles getFamille(string name)
        {
            Familles famille = new Familles();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Familles WHERE Nom = @name", conn);
            sql.Parameters.AddWithValue("@name", name);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                famille.convertDataReaderToFamilles(reader); // Set attributes to the famille thanks to the reader
                return famille;
            }
            else
                return null;
        }

        public SousFamilles getSousFamille(string name)
        {
            SousFamilles sousFamille = new SousFamilles();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE Nom = @name", conn);
            sql.Parameters.AddWithValue("@name", name);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                sousFamille.convertDataReaderToSousFamilles(reader); // Set attributes to the sousFamille thanks to the reader
                return sousFamille;
            }
            else
                return null;
        }

        public SousFamilles getSousFamille(int idSousFamille)
        {
            SousFamilles sousFamille = new SousFamilles();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefSousFamille = @idSousFamille", conn);
            sql.Parameters.AddWithValue("@idSousFamille", idSousFamille);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                sousFamille.convertDataReaderToSousFamilles(reader); // Set attributes to the sousFamille thanks to the reader
                return sousFamille;
            }
            else
                return null;
        }

        public Marques getMarque(string name)
        {
            Marques marque = new Marques();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Marques WHERE Nom = @name", conn);
            sql.Parameters.AddWithValue("@name", name);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                marque.convertDataReaderToMarques(reader); // Set attributes to the marque thanks to the reader
                return marque;
            }
            else
                return null;
        }

        public bool existSousFamilleInFamille(int idSousFamille, int idFamille)
        {
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefSousFamille = @idSousFamille AND RefFamille = @idFamille", conn);
            sql.Parameters.AddWithValue("@idSousFamille", idSousFamille);
            sql.Parameters.AddWithValue("@idFamille", idFamille);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
                return true;
            else
                return false;
        }

        public List<string> getTableBdd()
        {
            List<string> listTablesName = new List<string>();
            SQLiteCommand sql = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", conn);
            SQLiteDataReader reader = sql.ExecuteReader();
            while (reader.Read())
                listTablesName.Add(reader.GetValue(0).ToString());
            return listTablesName;
        }

        public List<string> getNameColumnTable(string table_name = "Articles")
        {
            List<string> listNameColumnTable = new List<string>();
            SQLiteCommand sql = new SQLiteCommand("PRAGMA table_info(" + table_name + ")", conn);
            SQLiteDataReader reader = sql.ExecuteReader();
            while (reader.Read())
                listNameColumnTable.Add(reader.GetValue(1).ToString());
            return listNameColumnTable;
        }
    }
}
