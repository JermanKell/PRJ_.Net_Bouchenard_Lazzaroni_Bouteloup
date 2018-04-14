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
        /// <param name="obj">Object to add in the DB.</param>
        public override void AddElement(object obj)
        {
            Marques brand = (Marques)obj;
            Marques resBrand = manager.getMarque(brand.Nom);

            if (resBrand == null)
                manager.insertMarque(brand);
            else
                throw new Exception("La marque " + resBrand.Nom + " existe déja dans la base");
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="obj">Object with the changes</param>
        /// <returns>Returns true if done, false else</returns>
        public override int ChangeElement(object obj)
        {
            int Count;

            Marques Brand = (Marques)(obj);
            Marques BrandFound = manager.getMarque(id: Brand.Id);


            if (BrandFound != null)
            {
                Count = manager.updateMarque(Brand);
                if (Count != 1)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la modification de la marque " + Brand.Nom);
                }
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
            if (manager.getMarque(id: IdBrand) != null)
            {
                Count += manager.removeArticleFromBrand(IdBrand);
                Count += manager.removeMarque(IdBrand);
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
        public bool ExistArticleFromBrand(int idBrand)
        {
            if (manager.existArticleFromBrand(idBrand) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and Brand</returns>
        public Dictionary<int, Marques> getAllMarques()
        {
            return manager.getAllMarques();
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<string> getColumnHeader()
        {
            return manager.getNameColumnTable("Marques");
        }

        /// <summary>
        /// Return the brand corresponding to the id
        /// </summary>
        /// <param name="id"> The id of the brand that you want </param>
        /// <returns> The brand founded </returns>
        public Marques GetBrand(int id)
        {
            return manager.getMarque(id: id);
        }
    }
}
