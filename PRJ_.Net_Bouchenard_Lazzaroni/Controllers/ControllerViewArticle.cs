using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// The controller of the article view
    /// </summary>
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
        /// <param name="Obj">Object article  to add in the DB.</param>
        public override void AddElement(Object Obj)
        {
            Articles Article = (Articles)Obj;
            Articles ArticleFound = Manager.GetArticle(Article.Reference);

            if (ArticleFound == null)
                Manager.InsertArticle(Article);
            else
                throw new Exception("L'article de référence " + ArticleFound.Reference + " existe déja dans la base");
        }

        /// <summary>
        /// Changes the values about an article stored in the DB depending on the reference
        /// </summary>
        /// <param name="Obj">Object article with the changes</param>
        /// <returns>Returns the number of articles updated</returns>
        public override int ChangeElement(Object Obj)
        {
            int Count;
            Articles Article = (Articles)(Obj);

            if (Manager.GetArticle(Article.Reference) != null)
            {
                Count = Manager.UpdateArticle(Article);
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
            if (Manager.GetArticle(RefObj) != null)
            {
                Count = Manager.RemoveArticle(RefObj);
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
            return Manager.GetAllArticles();
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and family</returns>
        public Dictionary<int, Familles> GetAllFamilles()
        {
            return Manager.GetAllFamilles();
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and sub family</returns>
        public Dictionary<int, SousFamilles> GetAllSousFamilles()
        {
            return Manager.GetAllSousFamilles();
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and brand</returns>
        public Dictionary<int, Marques> GetAllMarques()
        {
            return Manager.GetAllMarques();
        }

        /// <summary>
        /// Get name's column of table Article
        /// </summary>
        /// <returns> A list of the column of the table Article in the database </returns>
        public override List<String> GetColumnHeader()
        {
            return Manager.GetNameColumnTable();
        }

        /// <summary>
        /// Get one article by his reference
        /// </summary>
        /// <param name="Reference">The reference of the article</param>
        /// <returns>The article founded</returns>
        public Articles GetArticle(string Reference)
        {
            return Manager.GetArticle(Reference);
        }
    }
}
