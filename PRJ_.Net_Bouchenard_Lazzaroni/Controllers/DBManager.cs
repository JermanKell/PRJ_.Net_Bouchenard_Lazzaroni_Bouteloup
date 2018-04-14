using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Unique class where you can run sql query.
    /// </summary>
    class DBManager
    {
        private SQLiteConnection Conn; // The XML connection

        /// <summary>
        /// Constructor per default
        /// </summary>
        public DBManager()
        {
            Conn = DBConnection.GetInstance().GetDataBase();
        }

        /// <summary>
        /// Insert article to the database
        /// </summary>
        /// <param name="Article"> The article to add </param>
        /// <returns> The id of the article added </returns>
        public int InsertArticle(Articles Article)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES (@reference, @description, @idSousFamille, @idMarque, @prixHT, @quantite)", Conn);
            Sql.Parameters.AddWithValue("@reference", Article.Reference);
            Sql.Parameters.AddWithValue("@description", Article.Description);
            Sql.Parameters.AddWithValue("@idSousFamille", Article.IdSousFamille);
            Sql.Parameters.AddWithValue("@idMarque", Article.IdMarque);
            Sql.Parameters.AddWithValue("@prixHT", Article.PrixHT);
            Sql.Parameters.AddWithValue("@quantite", Article.Quantite);

            try
            {
                Sql.ExecuteNonQuery();
                return Convert.ToInt16(Conn.LastInsertRowId);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Update an article to the database
        /// </summary>
        /// <param name="Article"> The article to update </param>
        /// <returns> The number of rticle updated </returns>
        public int UpdateArticle(Articles Article)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "UPDATE Articles SET description = @description, RefSousFamille = @idSousFamille, RefMarque = @idMarque, PrixHT = @prixHT, Quantite = @quantite " +
                "WHERE RefArticle = @reference", Conn);
            Sql.Parameters.AddWithValue("@reference", Article.Reference);
            Sql.Parameters.AddWithValue("@description", Article.Description);
            Sql.Parameters.AddWithValue("@idSousFamille", Article.IdSousFamille);
            Sql.Parameters.AddWithValue("@idMarque", Article.IdMarque);
            Sql.Parameters.AddWithValue("@prixHT", Article.PrixHT);
            Sql.Parameters.AddWithValue("@quantite", Article.Quantite);

            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Insert family to the database
        /// </summary>
        /// <param name="Famille"> The new family to insert </param>
        /// <returns> Id of the new family added </returns>
        public int InsertFamille(Familles Famille)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "INSERT INTO Familles (RefFamille, Nom) VALUES((SELECT ifnull((SELECT RefFamille FROM Familles ORDER BY RefFamille DESC LIMIT 1) + 1, 1)), @nom);", Conn);
            Sql.Parameters.AddWithValue("@nom", Famille.Nom);

            try
            {
                Sql.ExecuteNonQuery();
                return Convert.ToInt16(Conn.LastInsertRowId);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Insert a new sub family to the database
        /// </summary>
        /// <param name="SousFamille"> The new sub family to add </param>
        /// <returns> Id of the new sub family added </returns>
        public int InsertSousFamille(SousFamilles SousFamille)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "INSERT INTO SousFamilles (RefSousFamille, RefFamille, Nom) VALUES((SELECT ifnull((SELECT RefSousFamille FROM SousFamilles ORDER BY RefSousFamille DESC LIMIT 1) + 1, 1)), @refFamille, @nom);", Conn);
            Sql.Parameters.AddWithValue("@refFamille", SousFamille.IdFamille);
            Sql.Parameters.AddWithValue("@nom", SousFamille.Nom);

            try
            {
                sql.ExecuteNonQuery();
                return Convert.ToInt32(conn.LastInsertRowId);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Insert a new brand to the database
        /// </summary>
        /// <param name="Marque"> The new brand to add </param>
        /// <returns> Id of the new brand added </returns>
        public int InsertMarque(Marques Marque)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "INSERT INTO Marques (RefMarque, Nom) VALUES((SELECT ifnull((SELECT RefMarque FROM Marques ORDER BY RefMarque DESC LIMIT 1) + 1, 1)), @nom);", Conn);
            Sql.Parameters.AddWithValue("@nom", Marque.Nom);

            try
            {
                Sql.ExecuteNonQuery();
                return Convert.ToInt16(Conn.LastInsertRowId);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        
        /// <summary>
        /// Get all article of the database
        /// </summary>
        /// <param name="Columnsort"> Column to sort </param>
        /// <param name="Ascending"> The direction </param>
        /// <returns> The list of all article </returns>
        public Dictionary<string, Articles> GetAllArticles(string Columnsort = "RefArticle", bool Ascending = true)
        {
            string Order = "ASC";
            if (!Ascending)
            {
                Order = "DESC";
            }

            Dictionary<string, Articles> DictionaryArticles = new Dictionary<string, Articles>();

            SQLiteCommand Sql = new SQLiteCommand("select * from Articles order by " + Columnsort + " " + Order, Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            while (Reader.Read())
            {
                Articles Article = new Articles();
                Article.ConvertDataReaderToArticles(Reader); // Set attributes to the article thanks to the reader
                Article.IdFamille = GetSousFamille(Id: Article.IdSousFamille).IdFamille;
                DictionaryArticles[Article.Reference] = Article;
            }
            return DictionaryArticles;
        }

        /// <summary>
        /// Get all family
        /// </summary>
        /// <returns> A dictionnary of the family </returns>
        public Dictionary<int, Familles> GetAllFamilles()
        {
            Dictionary<int, Familles> ListFamille = new Dictionary<int, Familles>();
            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM Familles", Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            while(Reader.Read())
            {
                Familles Famille = new Familles();
                Famille.ConvertDataReaderToFamilles(Reader);
                ListFamille.Add(Famille.Id, Famille);
            }

            return ListFamille;
        }

        /// <summary>
        /// Get all sub family
        /// </summary>
        /// <returns>A dictionnary of the sub family</returns>
        public Dictionary<int, SousFamilles> GetAllSousFamilles()
        {
            Dictionary<int, SousFamilles> ListSousFamille = new Dictionary<int, SousFamilles>();
            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM SousFamilles", Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            while (Reader.Read())
            {
                SousFamilles SousFamille = new SousFamilles();
                SousFamille.ConvertDataReaderToSousFamilles(Reader);
                ListSousFamille.Add(SousFamille.Id, SousFamille);
            }

            return ListSousFamille;
        }

        /// <summary>
        /// Get all sub families belonging to a family
        /// </summary>
        /// <param name="IdFamily"> The id of a family </param>
        /// <returns> The list of sub family belonging to a family </returns>
        public Dictionary<int, SousFamilles> GetAllSubFamiliesFromFamily(int IdFamily)
        {
            Dictionary<int, SousFamilles> ListSubFamily = new Dictionary<int, SousFamilles>();
            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefFamille = @idFamily", Conn);
            Sql.Parameters.AddWithValue("@idFamily", IdFamily);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            while (Reader.Read())
            {
                SousFamilles SubFamily = new SousFamilles();
                SubFamily.ConvertDataReaderToSousFamilles(Reader);
                ListSubFamily.Add(SubFamily.Id, SubFamily);
            }
            return ListSubFamily;
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns>A dictionnary of all brands</returns>
        public Dictionary<int, Marques> GetAllMarques()
        {
            Dictionary<int, Marques> ListMarque = new Dictionary<int, Marques>();
            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM Marques", Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            while (Reader.Read())
            {
                Marques Marque = new Marques();
                Marque.ConvertDataReaderToMarques(Reader);
                ListMarque.Add(Marque.Id, Marque);
            }

            return ListMarque;
        }

        /// <summary>
        /// Get one article by reference
        /// </summary>
        /// <param name="Reference"> The reference of the article to get </param>
        /// <returns> The article searched </returns>
        public Articles GetArticle(string Reference)
        {
            Articles Article = new Articles();

            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM Articles WHERE RefArticle = @reference", Conn);
            Sql.Parameters.AddWithValue("@reference", Reference);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            if (Reader.Read())
            {
                Article.ConvertDataReaderToArticles(Reader);
                Article.IdFamille = GetSousFamille(Id: Convert.ToInt16(Reader.GetValue(2))).IdFamille; // Get the Famille linked to the sousFamille to return a complete article
                return Article;
            }
            else
                return null;
        }

        /// <summary>
        /// Remove an article from a reference
        /// </summary>
        /// <param name="Reference"> The article reference </param>
        /// <returns> The number of rows removed in the DB </returns>
        public int RemoveArticle(string Reference)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "DELETE FROM Articles WHERE RefArticle = @reference", Conn);
            Sql.Parameters.AddWithValue("@reference", Reference);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Remove an article from a sub family id
        /// </summary>
        /// <param name="IdSubFamily"> The sub family id </param>
        /// <returns> The number of rows removed in the DB </returns>
        public int RemoveArticleFromSubFamily(int IdSubFamily)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "DELETE FROM Articles WHERE RefSousFamille = @idFamily", Conn);
            Sql.Parameters.AddWithValue("@idFamily", IdSubFamily);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Remove an article from a brand id
        /// </summary>
        /// <param name="IdBrand"> The brand id </param>
        /// <returns> The number of rows removed in the DB </returns>
        public int RemoveArticleFromBrand(int IdBrand)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "DELETE FROM Articles WHERE RefMarque = @idBrand", Conn);
            Sql.Parameters.AddWithValue("@idBrand", IdBrand);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Get one family by name OR id
        /// </summary>
        /// <param name="Name"> The name of the family to get </param>
        /// <param name="Id"> The id of the family to get </param>
        /// <returns> The family searched </returns>
        public Familles GetFamille(string Name = "", int Id = -1)
        {
            Familles Famille = new Familles();
            SQLiteCommand Sql;

            if (Name.CompareTo("") != 0)
            {
                Sql = new SQLiteCommand("SELECT * FROM Familles WHERE Nom = @name", Conn);
                Sql.Parameters.AddWithValue("@name", Name);
            }
            else
            {
                Sql = new SQLiteCommand("SELECT * FROM Familles WHERE RefFamille = @idFamille", Conn);
                Sql.Parameters.AddWithValue("@idFamille", Id);
            }
            
            SQLiteDataReader Reader = Sql.ExecuteReader();

            if (Reader.Read())
            {
                Famille.ConvertDataReaderToFamilles(Reader);
                return Famille;
            }
            else
                return null;
        }
        
        /// <summary>
        /// Remove one family by his id from the database
        /// </summary>
        /// <param name="RefId">The id of the family</param>
        /// <returns>The number of family removed</returns>
        public int RemoveFamille(int RefId)
        {
            SQLiteCommand Sql = new SQLiteCommand("DELETE FROM Familles WHERE RefFamille = @reference", Conn);
            Sql.Parameters.AddWithValue("@reference", RefId);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Get one sub family by name or id
        /// </summary>
        /// <param name="Name"> The name of the sub family to get </param>
        /// <param name="Id"> The id of the sub family to get </param>
        /// <returns> The sub family searched </returns>
        public SousFamilles GetSousFamille(string Name = "", int Id = -1)
        {
            SousFamilles SousFamille = new SousFamilles();
            SQLiteCommand Sql;

            if (Name.CompareTo("") != 0)
            {
                Sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE Nom = @name", Conn);
                Sql.Parameters.AddWithValue("@name", Name);
            }
            else
            {
                Sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefSousFamille = @idSousFamille", Conn);
                Sql.Parameters.AddWithValue("@idSousFamille", Id);
            }

            SQLiteDataReader Reader = Sql.ExecuteReader();

            if (Reader.Read())
            {
                SousFamille.ConvertDataReaderToSousFamilles(Reader);
                return SousFamille;
            }
            else
                return null;
        }

        /// <summary>
        /// Remove one sub family by his id
        /// </summary>
        /// <param name="IdSubFamily">The id of the sub family to remove</param>
        /// <returns>The number of sub family removed</returns>
        public int RemoveSubFamily(int IdSubFamily)
        {
            SQLiteCommand Sql = new SQLiteCommand(
                "DELETE FROM SousFamilles WHERE RefSousFamille = @idSubFamily", Conn);
            Sql.Parameters.AddWithValue("@idSubFamily", IdSubFamily);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Get one brand by name or id
        /// </summary>
        /// <param name="Name"> The name of the brand to get </param>
        /// <param name="Id"> The id of the brand to get </param>
        /// <returns> The brand searched </returns>
        public Marques GetMarque(string Name = "", int Id = -1)
        {
            Marques Marque = new Marques();
            SQLiteCommand Sql;

            if (Name.CompareTo("") != 0)
            {
                Sql = new SQLiteCommand("SELECT * FROM Marques WHERE Nom = @name", Conn);
                Sql.Parameters.AddWithValue("@name", Name);
            }
            else
            {
                Sql = new SQLiteCommand("SELECT * FROM Marques WHERE RefMarque = @idMarque", Conn);
                Sql.Parameters.AddWithValue("@idMarque", Id);
            }

            SQLiteDataReader Reader = Sql.ExecuteReader();

            if (Reader.Read())
            {
                Marque.ConvertDataReaderToMarques(Reader);
                return Marque;
            }
            else
                return null;
        }

        /// <summary>
        /// Remove one brand from his id
        /// </summary>
        /// <param name="RefId">The id of the brand to remove</param>
        /// <returns>The number of brands removed</returns>
        public int RemoveMarque(int RefId)
        {
            SQLiteCommand Sql = new SQLiteCommand("DELETE FROM Marques WHERE RefMarque = @reference", Conn);
            Sql.Parameters.AddWithValue("@reference", RefId);
            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Set the new family to the database
        /// </summary>
        /// <param name="Family"> The family to update </param>
        /// <returns>The number of line modified </returns>
        public int updateFamilles(Familles Family)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE Familles SET Nom = @nom WHERE RefFamille = @reference", conn);
            sql.Parameters.AddWithValue("@nom", Family.Nom);
            sql.Parameters.AddWithValue("@reference", Family.Id);

            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Set the new family to the database
        /// </summary>
        /// <param name="Subfamily"> The family to update </param>
        /// <returns>The number of line modified </returns>
        public int updateSousFamilles(SousFamilles Subfamily)
        {
            SQLiteCommand sql = new SQLiteCommand("UPDATE SousFamilles SET Nom = @name, RefFamille = @idFamily WHERE RefSousFamille = @idSubFamily", conn);
            sql.Parameters.AddWithValue("@name", Subfamily.Nom);
            sql.Parameters.AddWithValue("@idFamily", Subfamily.IdFamille);
            sql.Parameters.AddWithValue("@idSubFamily", Subfamily.Id);

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
        /// Set the new brand to the database
        /// </summary>
        /// <param name="Brand"> The brand to update </param>
        /// <returns>The number of line modified </returns>
        public int UpdateMarque(Marques Brand)
        {
            SQLiteCommand Sql = new SQLiteCommand("UPDATE Marques SET Nom = @nom WHERE RefMarque = @reference", Conn);
            Sql.Parameters.AddWithValue("@nom", Brand.Nom);
            Sql.Parameters.AddWithValue("@reference", Brand.Id);

            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Increment the quantity of the article by one
        /// </summary>
        /// <param name="RefArticle"> The quantity of the article to increment </param>
        /// <returns> The number of article updated </returns>
        public int UpdateQuantiteArticle(string RefArticle)
        {
            SQLiteCommand Sql = new SQLiteCommand("UPDATE Articles SET Quantite = Quantite + 1 WHERE RefArticle = @refArticle", Conn);
            Sql.Parameters.AddWithValue("@refArticle", RefArticle);

            try
            {
                return Sql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        
        /// <summary>
        /// Check if the sub family correspond to the good family
        /// </summary>
        /// <param name="IdSousFamille"> The sub family id to check </param>
        /// <param name="IdFamille"> The family id to check </param>
        /// <returns> True if OK else false </returns>
        public bool ExistSousFamilleInFamille(int IdSousFamille, int IdFamille)
        {
            SQLiteCommand Sql = new SQLiteCommand("SELECT * FROM SousFamilles WHERE RefSousFamille = @idSousFamille AND RefFamille = @idFamille", Conn);
            Sql.Parameters.AddWithValue("@idSousFamille", IdSousFamille);
            Sql.Parameters.AddWithValue("@idFamille", IdFamille);
            SQLiteDataReader Reader = Sql.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if at least one article exists for a subfamily
        /// </summary>
        /// <param name="IdSubFamily"> The sub family id to check </param>
        /// <returns> The number of articles associated with the subfamily </returns>
        public int ExistArticleFromSubFamily(int IdSubFamily)
        {
            SQLiteCommand Sql = new SQLiteCommand("SELECT COUNT(*) FROM Articles WHERE RefSousFamille = @idSubFamily", Conn);
            Sql.Parameters.AddWithValue("@idSubFamily", IdSubFamily);
            SQLiteDataReader Reader = Sql.ExecuteReader();
            Reader.Read();
            return Reader.GetInt32(0);
        }

        /// <summary>
        /// Check if at least one article exists for a brand
        /// </summary>
        /// <param name="idSubFamily"> The brand id to check </param>
        /// <returns> The number of articles associated with the brand </returns>
        public int ExistArticleFromBrand(int IdBrand)
        {
            SQLiteCommand Sql = new SQLiteCommand("SELECT COUNT(*) FROM Articles WHERE RefMarque = @idBrand", Conn);
            Sql.Parameters.AddWithValue("@idBrand", IdBrand);
            SQLiteDataReader Reader = Sql.ExecuteReader();
            Reader.Read();
            return Reader.GetInt32(0);
        }

        /// <summary>
        /// Get all table name of the database
        /// </summary>
        /// <returns> The list of all name table </returns>
        public List<string> GetTableBdd()
        {
            List<string> ListTablesName = new List<string>();
            SQLiteCommand Sql = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();
            while (Reader.Read())
                ListTablesName.Add(Reader.GetValue(0).ToString());
            return ListTablesName;
        }

        /// <summary>
        /// Get all column of a table
        /// </summary>
        /// <param name="Table_name"></param>
        /// <returns>Columns's name</returns>
        public List<string> GetNameColumnTable(string Table_name = "Articles")
        {
            List<string> ListNameColumnTable = new List<string>();
            SQLiteCommand Sql = new SQLiteCommand("PRAGMA table_info(" + Table_name + ")", Conn);
            SQLiteDataReader Reader = Sql.ExecuteReader();
            while (Reader.Read())
                ListNameColumnTable.Add(Reader.GetValue(1).ToString());
            return ListNameColumnTable;
        }

        /// <summary>
        /// To delete all table of the database
        /// </summary>
        /// <param name="Name"> The databse to delete or empty if all </param>
        public void DeleteTables(string Name = "") // Recurisivity -- No param to delete all - Set param to delete once.
        {
            List<string> NameAll = new List<string>();
            SQLiteCommand Sql = Conn.CreateCommand();

            if (Name.CompareTo("") == 0)
            {
                NameAll = GetTableBdd();

                foreach (string NameOne in NameAll)
                    DeleteTables(NameOne);
            }
            else
            {
                Sql.CommandText = "DELETE FROM " + Name;

                try
                {
                    Sql.ExecuteNonQuery();
                }
                catch (Exception Ex) { throw new Exception(Ex.Message); }
            }
        }
    }
}