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
        private SQLiteConnection conn; // The XML connection

        /// <summary>
        /// Constructor per default
        /// </summary>
        public DBManager()
        {
            conn = DBConnection.getInstance().getDataBase();
        }

        /// <summary>
        /// Insert article to the database
        /// </summary>
        /// <param name="article"> The article to add </param>
        /// <returns> The id of the article added </returns>
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

        /// <summary>
        /// Update an article to the database
        /// </summary>
        /// <param name="article"> The article to update </param>
        /// <returns> The number of rticle updated </returns>
        public int updateArticle(Articles article)
        {
            SQLiteCommand sql = new SQLiteCommand(
                "UPDATE Articles SET description = @description, RefSousFamille = @idSousFamille, RefMarque = @idMarque, PrixHT = @prixHT, Quantite = @quantite " +
                "WHERE RefArticle = @reference", conn);
            sql.Parameters.AddWithValue("@reference", article.Reference);
            sql.Parameters.AddWithValue("@description", article.Description);
            sql.Parameters.AddWithValue("@idSousFamille", article.IdSousFamille);
            sql.Parameters.AddWithValue("@idMarque", article.IdMarque);
            sql.Parameters.AddWithValue("@prixHT", article.PrixHT);
            sql.Parameters.AddWithValue("@quantite", article.Quantite);

            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Insert family to the database
        /// </summary>
        /// <param name="famille"> The new family to insert </param>
        /// <returns> Id of the new family added </returns>
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

        /// <summary>
        /// Insert a new sub family to the database
        /// </summary>
        /// <param name="sousFamille"> The new sub family to add </param>
        /// <returns> Id of the new sub family added </returns>
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

        /// <summary>
        /// Insert a new brand to the database
        /// </summary>
        /// <param name="marque"> The new brand to add </param>
        /// <returns> Id of the new brand added </returns>
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
        
        /// <summary>
        /// Get all article of the database
        /// </summary>
        /// <param name="columnsort"> Column to sort </param>
        /// <param name="ascending"> The direction </param>
        /// <returns> The list of all article </returns>
        public Dictionary<string, Articles> getAllArticles(string columnsort = "RefArticle", bool ascending = true)
        {
            string order = "ASC";
            if (!ascending)
            {
                order = "DESC";
            }

            Dictionary<string, Articles> DictionaryArticles = new Dictionary<string, Articles>();

            SQLiteCommand sql = new SQLiteCommand("select * from Articles order by " + columnsort + " " + order, conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Articles Article = new Articles();
                Article.convertDataReaderToArticles(reader); // Set attributes to the article thanks to the reader
                Article.IdFamille = getSousFamille(id: Article.IdSousFamille).IdFamille;
                DictionaryArticles[Article.Reference] = Article;
            }
            return DictionaryArticles;
        }

        public Dictionary<int, Familles> getAllFamilles()
        {
            Dictionary<int, Familles> listFamille = new Dictionary<int, Familles>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Familles", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while(reader.Read())
            {
                Familles famille = new Familles();
                famille.convertDataReaderToFamilles(reader);
                listFamille.Add(famille.Id, famille);
            }

            return listFamille;
        }

        public Dictionary<int, SousFamilles> getAllSousFamilles()
        {
            Dictionary<int, SousFamilles> listSousFamille = new Dictionary<int, SousFamilles>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM SousFamilles", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                SousFamilles sousFamille = new SousFamilles();
                sousFamille.convertDataReaderToSousFamilles(reader);
                listSousFamille.Add(sousFamille.Id, sousFamille);
            }

            return listSousFamille;
        }

        public Dictionary<int, Marques> getAllMarques()
        {
            Dictionary<int, Marques> listMarque = new Dictionary<int, Marques>();
            SQLiteCommand sql = new SQLiteCommand("SELECT * FROM Marques", conn);
            SQLiteDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Marques marque = new Marques();
                marque.convertDataReaderToMarques(reader);
                listMarque.Add(marque.Id, marque);
            }

            return listMarque;
        }

        /// <summary>
        /// Get one article by reference
        /// </summary>
        /// <param name="reference"> The reference of the article to get </param>
        /// <returns> The article searched </returns>
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

        public int removeArticle(string reference)
        {
            SQLiteCommand sql = new SQLiteCommand(
                "DELETE FROM Articles WHERE RefArticle = @reference", conn);
            sql.Parameters.AddWithValue("@reference", reference);
            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get one family by name OR id
        /// </summary>
        /// <param name="name"> The name of the family to get </param>
        /// <param name="id"> The id of the family to get </param>
        /// <returns> The family searched </returns>
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
        
        public int removeFamille(int refId)
        {
            SQLiteCommand sql = new SQLiteCommand("DELETE FROM Familles WHERE RefFamille = @reference", conn);
            sql.Parameters.AddWithValue("@reference", refId);
            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get one sub family by name or id
        /// </summary>
        /// <param name="name"> The name of the sub family to get </param>
        /// <param name="id"> The id of the sub family to get </param>
        /// <returns> The sub family searched </returns>
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

        /// <summary>
        /// Get one brand by name or id
        /// </summary>
        /// <param name="name"> The name of the brand to get </param>
        /// <param name="id"> The id of the brand to get </param>
        /// <returns> The brand searched </returns>
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
        
        /// <summary>
        /// Set the new family to the database
        /// </summary>
        /// <param name="fam"> The family to update </param>
        /// <returns>The number of line modified </returns>
        public int updateFamilles(Familles fam)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE Familles SET Nom = @nom WHERE RefFamille = @reference", conn);
            sql.Parameters.AddWithValue("@nom", fam.Nom);
            sql.Parameters.AddWithValue("@reference", fam.Id);

            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Set the new family to the database
        /// </summary>
        /// <param name="fam"> The family to update </param>
        /// <returns>The number of line modified </returns>
        public int updateSousFamilles(SousFamilles subfam)
        {
            // TODO
            return 0;
        }

        /// <summary>
        /// Set the new brand to the database
        /// </summary>
        /// <param name="brand"> The brand to update </param>
        /// <returns>The number of line modified </returns>
        public int updateMarque(Marques brand)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE Marques SET Nom = @nom WHERE RefMarque = @reference", conn);
            sql.Parameters.AddWithValue("@nom", brand.Nom);
            sql.Parameters.AddWithValue("@reference", brand.Id);

            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Increment the quantity of the article by one
        /// </summary>
        /// <param name="refArticle"> The quantity of the article to increment </param>
        /// <returns> The number of article updated </returns>
        public int updateQuantiteArticle(string refArticle)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE Articles SET Quantite = Quantite + 1 WHERE RefArticle = @refArticle", conn);
            sql.Parameters.AddWithValue("@refArticle", refArticle);

            try
            {
                return sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Check if the sub family correspond to the good family
        /// </summary>
        /// <param name="idSousFamille"> The sub family id to check </param>
        /// <param name="idFamille"> The family id to check </param>
        /// <returns> True if OK else false </returns>
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

        /// <summary>
        /// Get all table name of the database
        /// </summary>
        /// <returns> The list of all name table </returns>
        public List<string> getTableBdd()
        {
            List<string> listTablesName = new List<string>();
            SQLiteCommand sql = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", conn);
            SQLiteDataReader reader = sql.ExecuteReader();
            while (reader.Read())
                listTablesName.Add(reader.GetValue(0).ToString());
            return listTablesName;
        }

        /// <summary>
        /// Get all column of a table
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns>Columns's name</returns>
        public List<string> getNameColumnTable(string table_name = "Articles")
        {
            List<string> listNameColumnTable = new List<string>();
            SQLiteCommand sql = new SQLiteCommand("PRAGMA table_info(" + table_name + ")", conn);
            SQLiteDataReader reader = sql.ExecuteReader();
            while (reader.Read())
                listNameColumnTable.Add(reader.GetValue(1).ToString());
            return listNameColumnTable;
        }

        /// <summary>
        /// To delete all table of the database
        /// </summary>
        /// <param name="name"> The databse to delete or empty if all </param>
        public void deleteTables(string name = "") // Recurisivity -- No param to delete all - Set param to delete once.
        {
            List<string> nameAll = new List<string>();
            SQLiteCommand sql = conn.CreateCommand();

            if (name.CompareTo("") == 0)
            {
                nameAll = getTableBdd();

                foreach (string nameOne in nameAll)
                    deleteTables(nameOne);
            }
            else
            {
                sql.CommandText = "DELETE FROM " +name;

                try
                {
                    sql.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
        }
    }
}