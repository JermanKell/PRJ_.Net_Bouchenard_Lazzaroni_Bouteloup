using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
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
        /// Deletes an element from the DB with the reference passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns true if done, false else</returns>
        public override int DeleteElement(string RefObj)
        {
            int Count;
            if (manager.getFamille(id: Convert.ToInt32(RefObj)) != null)
            {
                Count = manager.removeFamille(Convert.ToInt32(RefObj));
                if (Count != 1)
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

        public Familles GetFamily(int id)
        {
            return manager.getFamille(id: id);
        }
    }
}
