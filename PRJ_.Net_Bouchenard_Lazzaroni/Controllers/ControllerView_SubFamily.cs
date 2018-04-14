using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// The controller of the sub family view
    /// </summary>
    class ControllerView_SubFamily : ControllerView
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public ControllerView_SubFamily() : base()
        {}

        /// <summary>
        /// Adds a new element in the DB 
        /// </summary>
        /// <param name="obj">Object to add in the DB.</param>
        public override void AddElement(Object obj)
        {
            SousFamilles SubFamily = (SousFamilles)obj;
            SousFamilles SubFamilyFound = manager.getSousFamille(SubFamily.Nom);

            if (SubFamilyFound == null)
                manager.insertSousFamille(SubFamily);
            else
                throw new Exception("La sous famille " + SubFamily.Nom + " existe déja dans la base");
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="obj">Object with the changes</param>
        /// <returns>Returns the number of rows changed</returns>
        public override int ChangeElement(object obj)
        {
            int Count;

            SousFamilles SubFamily = (SousFamilles)(obj);
            SousFamilles SubFamilyFound = manager.getSousFamille(id: SubFamily.Id);


            if (SubFamilyFound != null)
            {
                Count = manager.updateSousFamilles(SubFamily);
                if (Count != 1)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la modification de la sous famille " + SubFamily.Nom);
                }
            }
            else
            {
                throw new Exception("La sous famille " + SubFamily.Nom + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Deletes all articles and a sub family with the reference of the sub family passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns the total number of rows removed</returns>
        public override int DeleteElement(string RefObj)
        {
            int IdSubFamily = Convert.ToInt32(RefObj);
            int Count = 0;
            if (manager.getSousFamille(id: IdSubFamily) != null)
            {
                Count += manager.removeArticleFromSubFamily(IdSubFamily);
                Count += manager.removeSubFamily(IdSubFamily);
                if (Count == 0)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la supression de la sous famille de reference " + RefObj);
                }
            }
            else
            {
                throw new Exception("La sous famille de référence " + RefObj + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Check if at least one article exists from a id sub family
        /// </summary>
        /// <param name="idBrand">Reference of sub family</param>
        /// <returns>Returns true is an article exists, else false</returns>
        public bool ExistArticleFromSubFamily(int idSubFamily)
        {
            if (manager.existArticleFromSubFamily(idSubFamily) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get a dictionary of all families
        /// </summary>
        /// <returns>Dictionary of int and Families</returns>
        public Dictionary<int, Familles> getAllFamilies()
        {
            return manager.getAllFamilles();
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and SubFamily</returns>
        public Dictionary<int, SousFamilles> getAllSousFamilles()
        {
            return manager.getAllSousFamilles();
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<string> getColumnHeader()
        {
            return manager.getNameColumnTable("SousFamilles");
        }

        /// <summary>
        /// Get one sub family by his id
        /// </summary>
        /// <param name="id">The id of the sub family</param>
        /// <returns>The sub family founded</returns>
        public SousFamilles GetSubFamily(int id)
        {
            return manager.getSousFamille(id: id);
        }
    }
}
