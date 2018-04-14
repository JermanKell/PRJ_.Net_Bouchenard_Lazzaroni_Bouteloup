using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
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
        /// <returns>Returns true if done, false else</returns>
        public override int AddElement(object obj)
        {
            int Count;
            Marques Brand = (Marques)obj;
            Marques BrandFound = manager.getMarque(Brand.Nom);

            if (BrandFound == null)
            {
                Count = manager.insertMarque(Brand);
                if (Count == 0)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher l'ajout de la marque " + Brand.Nom);
                }
            }
            else
            {
                throw new Exception("La marque " + Brand.Nom + " existe déja dans la base");
            }
            return Count;
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
        /// Deletes an element from the DB with the reference passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns true if done, false else</returns>
        public override int DeleteElement(string RefObj)
        {
            int Count;
            if (manager.getMarque(id: Convert.ToInt32(RefObj)) != null)
            {
                Count = manager.removeMarque(Convert.ToInt32(RefObj));
                if (Count != 1)
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

        public Marques GetBrand(int id)
        {
            return manager.getMarque(id: id);
        }
    }
}
