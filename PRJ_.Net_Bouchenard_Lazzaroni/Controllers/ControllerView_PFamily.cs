using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// The controller of the family view
    /// </summary>
    class ControllerView_PFamily : ControllerView
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public ControllerView_PFamily() : base()
        {}

        /// <summary>
        /// Adds a new element in the DB 
        /// </summary>
        /// <param name="obj">Object to add in the DB.</param>
        public override void AddElement(Object obj)
        {
            Familles Family = (Familles)obj;
            Familles FamilyFound = manager.getFamille(Family.Nom);

            if (FamilyFound == null)
                manager.insertFamille(Family);
            else
                throw new Exception("La famille " + FamilyFound.Nom + " existe déja dans la base");
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="obj">Object with the changes</param>
        /// <returns>Returns true if done, false else</returns>
        public override int ChangeElement(Object obj)
        {
            int Count;

            Familles Family = (Familles)(obj);
            Familles FamilyFound = manager.getFamille(id: Family.Id);


            if (FamilyFound != null)
            {
                Count = manager.updateFamilles(Family);
                if (Count != 1)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la modification de la famille " + Family.Nom);
                }
            }
            else
            {
                throw new Exception("La famille " + Family.Nom + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Deletes all articles, subfamily associated to the family and the family itself with its reference passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns the total number of rows removed</returns>
        public override int DeleteElement(string RefObj)
        {
            int Count = 0;

            if (manager.getFamille(id: Convert.ToInt32(RefObj)) != null)
            {
                Dictionary<int, SousFamilles> DictionaryAllSubFamilyInFamily = manager.getAllSubFamiliesFromFamily(Convert.ToInt32(RefObj));
                foreach (KeyValuePair<int, SousFamilles> SubFamily in DictionaryAllSubFamilyInFamily)
                {
                    Count += manager.removeArticleFromSubFamily(SubFamily.Key); //Remove all articles of a sub family
                    Count += manager.removeSubFamily(SubFamily.Key); //Remove the sub family
                }

                Count += manager.removeFamille(Convert.ToInt32(RefObj));
                if (Count == 0)
                {
                    throw new Exception("Une erreur liée à la base de données à empêcher la supression de la famille de reference " + RefObj);
                }
            }
            else
            {
                throw new Exception("La famille de référence " + RefObj + " n'existe pas dans la base");
            }
            return Count;
        }

        /// <summary>
        /// Know if an article belong or not to a family
        /// </summary>
        /// <param name="idFamily"> The id of the family </param>
        /// <returns> True if article founded, false else </returns>
        public bool ExistArticleFromFamily(int idFamily)
        {
            Dictionary < int, SousFamilles > DictionaryAllSubFamilyInFamily = manager.getAllSubFamiliesFromFamily(idFamily);
            foreach (KeyValuePair<int, SousFamilles> SubFamily in DictionaryAllSubFamilyInFamily)
            {
                if (manager.existArticleFromSubFamily(SubFamily.Key) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and Familles</returns>
        public Dictionary<int, Familles> getAllFamilles()
        {
            return manager.getAllFamilles();
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<String> getColumnHeader()
        {
            return manager.getNameColumnTable("Familles");
        }

        /// <summary>
        /// Get one family from his id
        /// </summary>
        /// <param name="id">The id of the family</param>
        /// <returns>The family founded</returns>
        public Familles GetFamily(int id)
        {
            return manager.getFamille(id: id);
        }
    }
}
