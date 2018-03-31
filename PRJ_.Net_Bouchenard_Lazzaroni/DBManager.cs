using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections;

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
            SQLiteCommand sql = new SQLiteCommand(
                "INSERT INTO Familles (RefFamille, Nom) VALUES((SELECT ifnull((SELECT RefFamille FROM Familles ORDER BY RefFamille DESC LIMIT 1) + 1, 1)), @nom);", conn);
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
                "INSERT INTO SousFamilles (RefSousFamille, RefFamille, Nom) VALUES((SELECT ifnull((SELECT RefSousFamille FROM SousFamilles ORDER BY RefSousFamille DESC LIMIT 1) + 1, 1)), @refFamille, @nom);", conn);
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
            SQLiteCommand sql = new SQLiteCommand(
                "INSERT INTO Marques (RefMarque, Nom) VALUES((SELECT ifnull((SELECT RefMarque FROM Marques ORDER BY RefMarque DESC LIMIT 1) + 1, 1)), @nom);", conn);
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

        public List<Familles> getAllFamilles()
        {
            List<Familles> listFamille = new List<Familles>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Familles", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while(reader.Read())
            {
                Familles famille = new Familles();
                famille.convertDataReaderToFamilles(reader);
                listFamille.Add(famille);
            }

            return listFamille;
        }

        public List<SousFamilles> getAllSousFamilles()
        {
            List<SousFamilles> listSousFamille = new List<SousFamilles>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM SousFamilles", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                SousFamilles sousFamille = new SousFamilles();
                sousFamille.convertDataReaderToSousFamilles(reader);
                listSousFamille.Add(sousFamille);
            }

            return listSousFamille;
        }

        public List<Marques> getAllMarques()
        {
            List<Marques> listMarque = new List<Marques>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Marques", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Marques marque = new Marques();
                marque.convertDataReaderToMarques(reader);
                listMarque.Add(marque);
            }

            return listMarque;
        }

        public Articles getArticle(string reference)
        {
            Articles article = new Articles();

            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Articles WHERE RefArticle = @reference", conn);
            sql.Parameters.AddWithValue("@reference", reference);
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                article.convertDataReaderToArticles(reader);
                article.IdFamille = getSousFamille(id: Convert.ToInt16(reader.GetValue(2))).IdFamille; // Get the Famille linked to the sousFamille to return a complete article
                return article;
            }
            else
                return null;
        }

        public Familles getFamille(string name = "", int id = -1)
        {
            Familles famille = new Familles();
            SQLiteCommand sql;

            if (name.CompareTo("") != 0)
            {
                sql = new SQLiteCommand("SELECT * FROM Familles WHERE Nom = @name", conn);
                sql.Parameters.AddWithValue("@name", name);
            }
            else
            {
                sql = new SQLiteCommand("SELECT * FROM Familles WHERE RefFamille = @idFamille", conn);
                sql.Parameters.AddWithValue("@idFamille", id);
            }
            
            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                famille.convertDataReaderToFamilles(reader);
                return famille;
            }
            else
                return null;
        }

        public SousFamilles getSousFamille(string name = "", int id = -1)
        {
            SousFamilles sousFamille = new SousFamilles();
            SQLiteCommand sql;

            if (name.CompareTo("") != 0)
            {
                sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE Nom = @name", conn);
                sql.Parameters.AddWithValue("@name", name);
            }
            else
            {
                sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefSousFamille = @idSousFamille", conn);
                sql.Parameters.AddWithValue("@idSousFamille", id);
            }

            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                sousFamille.convertDataReaderToSousFamilles(reader);
                return sousFamille;
            }
            else
                return null;
        }

        public Marques getMarque(string name = "", int id = -1)
        {
            Marques marque = new Marques();
            SQLiteCommand sql;

            if (name.CompareTo("") != 0)
            {
                sql = new SQLiteCommand("SELECT * FROM Marques WHERE Nom = @name", conn);
                sql.Parameters.AddWithValue("@name", name);
            }
            else
            {
                sql = new SQLiteCommand("SELECT * FROM Marques WHERE RefMarque = @idMarque", conn);
                sql.Parameters.AddWithValue("@idMarque", id);
            }

            SQLiteDataReader reader = sql.ExecuteReader();

            if (reader.Read())
            {
                marque.convertDataReaderToMarques(reader);
                return marque;
            }
            else
                return null;
        }

        public void updateQuantiteArticle(string refArticle)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE Articles SET Quantite = Quantite + 1 WHERE RefArticle = @refArticle", conn);
            sql.Parameters.AddWithValue("@refArticle", refArticle);

            try
            {
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
