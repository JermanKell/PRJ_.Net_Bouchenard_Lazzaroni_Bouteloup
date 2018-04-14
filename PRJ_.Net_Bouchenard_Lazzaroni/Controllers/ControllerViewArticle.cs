using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class ControllerViewArticle : ControllerView
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public ControllerViewArticle() : base()
        {}

        /// <summary>
        /// Adds a new article in the DB 
        /// </summary>
        /// <param name="obj">Object article  to add in the DB.</param>
        /// <returns>Returns the number of article added</returns>
        public override int AddElement(Object obj)
        {
            int Count;
            Articles Article = (Articles)obj;
            Articles ArticleFound = manager.getArticle(Article.Reference);

            if (ArticleFound == null)
            {
                Count = manager.insertArticle(Article);
                if (Count == 0)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher l'ajout de l'article de référence " + Article.Reference);
                }
            }
            else
            {
                throw new Exception("L'article de référence " + Article.Reference + " existe déja dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Changes the values about an article stored in the DB depending on the reference
        /// </summary>
        /// <param name="obj">Object article with the changes</param>
        /// <returns>Returns the number of articles updated</returns>
        public override int ChangeElement(Object obj)
        {
            int Count;
            Articles Article = (Articles)(obj);

            if (manager.getArticle(Article.Reference) != null)
            {
                Count = manager.updateArticle(Article);
                if (Count != 1)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la modification de l'article de référence " + Article.Reference);
                }
            }
            else
            {
                throw new Exception("L'article de référence " + Article.Reference + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Deletes an article from the DB with the reference passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the article to delete</param>
        /// <returns>Returns the number of articles deleted</returns>
        public override int DeleteElement(string RefObj)
        {
            int Count;
            if (manager.getArticle(RefObj) != null)
            {
                Count = manager.removeArticle(RefObj);
                if (Count != 1)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la supression de l'article de référence " + RefObj);
                }
            }
            else
            {
                throw new Exception("L'article de référence " + RefObj + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of string and Articles</returns>
        public Dictionary<string, Articles> GetAllArticles()
        {
            return manager.getAllArticles();
        }

        /// <summary>
        /// Get name's column of table Article
        /// </summary>
        /// <returns> A list of the column of the table Article in the database </returns>
        public override List<String> getColumnHeader()
        {
            return manager.getNameColumnTable();
        }

        public Articles GetArticle(string reference)
        {
            return manager.getArticle(reference);
        }
    }
}
