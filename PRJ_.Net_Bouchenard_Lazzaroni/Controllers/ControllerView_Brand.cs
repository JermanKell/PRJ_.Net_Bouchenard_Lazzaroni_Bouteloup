using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Controller of the brand view
    /// </summary>
    class ControllerView_Brand : ControllerView
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public ControllerView_Brand() : base()
        {}

        /// <summary>
        /// Adds a new element in the DB 
        /// </summary>
        /// <param name="Obj">Object to add in the DB.</param>
        public override void AddElement(object Obj)
        {
            Marques Brand = (Marques)Obj;
            Marques ResBrand = Manager.GetMarque(Brand.Nom);

            if (ResBrand == null)
                Manager.InsertMarque(Brand);
            else
                throw new Exception("La marque " + ResBrand.Nom + " existe déja dans la base");
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="Obj">Object with the changes</param>
        /// <returns>Returns true if done, false else</returns>
        public override int ChangeElement(object Obj)
        {
            int Count;

            Marques Brand = (Marques)(Obj);
            Marques BrandFound = Manager.GetMarque(Id: Brand.Id);

            if (BrandFound != null)
            {
                if (Manager.GetMarque(Brand.Nom) == null) // Check if the brand already exist or not
                {
                    Count = Manager.UpdateMarque(Brand);
                    if (Count != 1)
                        throw new Exception("Une erreur liée à la base de données à empêcher la modification de la marque " + Brand.Nom);
                }
                else
                    throw new Exception("La marque " + Brand.Nom + " existe déjà dans la base");
            }
            else
            {
                throw new Exception("La marque " + Brand.Nom + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Deletes all articles and a brand with the reference of the brand passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns the total number of rows removed</returns>
        public override int DeleteElement(string RefObj)
        {
            int IdBrand = Convert.ToInt32(RefObj);
            int Count = 0;
            if (Manager.GetMarque(Id: IdBrand) != null)
            {
                Count += Manager.RemoveArticleFromBrand(IdBrand);
                Count += Manager.RemoveMarque(IdBrand);
                if (Count == 0)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la supression de la marque de reference " + RefObj);
                }
            }
            else
            {
                throw new Exception("La marque de référence " + RefObj + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Check if at least one article exists from a id brand
        /// </summary>
        /// <param name="idBrand">Reference of brand</param>
        /// <returns>Returns true is an article exists, else false</returns>
        public bool ExistArticleFromBrand(int IdBrand)
        {
            if (Manager.ExistArticleFromBrand(IdBrand) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and Brand</returns>
        public Dictionary<int, Marques> GetAllMarques()
        {
            return Manager.GetAllMarques();
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<string> GetColumnHeader()
        {
            return Manager.GetNameColumnTable("Marques");
        }

        /// <summary>
        /// Return the brand corresponding to the id
        /// </summary>
        /// <param name="Id"> The id of the brand that you want </param>
        /// <returns> The brand founded </returns>
        public Marques GetBrand(int Id)
        {
            return Manager.GetMarque(Id: Id);
        }
    }
}
