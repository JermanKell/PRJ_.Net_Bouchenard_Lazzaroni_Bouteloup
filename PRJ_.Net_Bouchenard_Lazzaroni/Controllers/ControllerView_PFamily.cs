using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni.Controllers
{
    class ControllerView_PFamily : ControllerView
    {
        /// <summary>
        /// Dictionary of Familles objects
        /// </summary>
        private Dictionary<int, Familles> DicFam = null;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public ControllerView_PFamily() : base()
        {
            DicFam = new Dictionary<int, Familles>();
        }

        /// <summary>
        /// Adds a new element in the DB 
        /// </summary>
        /// <param name="obj">Object to add in the DB.</param>
        /// <returns>Returns true if done, false else</returns>
        public override int AddElement(Object obj)
        {
            int var;
            Familles fam = (Familles)obj;
            Familles resFam = manager.getFamille(fam.Nom);

            if (resFam == null)
            {
                var = manager.insertFamille(resFam);
                if (var != 0)
                {
                    MessageBox.Show("Insertion of this object succeed");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Insertion of this object failed");
                }
            }
            else
            {
                MessageBox.Show("This object already exists in the DB");
                var = -1;
            }
            return var;
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="obj">Object with the changes</param>
        /// <returns>Returns true if done, false else</returns>
        public override int ChangeElement(Object obj)
        {
            int var;
            Familles famille = (Familles)(obj);
            Familles fam = manager.getFamille(famille.Nom);

            if (fam != null)
            {
                var = manager.updateFamilles(famille);
                if (var == 1)
                {
                    MessageBox.Show("The element in the DB has been modified");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("An error occured while the program was changing the values");
                }
            }
            else
            {
                MessageBox.Show("The element to modify does not exist in the DB");
                var = -1;
            }
            return var;   
        }

        /// <summary>
        /// Deletes an element from the DB with the reference passed in parameter 
        /// </summary>
        /// <param name="RefObj">Reference of the element to delete</param>
        /// <returns>Returns true if done, false else</returns>
        public override int DeleteElement(string RefObj)
        {
            int var = -1;
            int Ref = Convert.ToInt32(RefObj);
            if (manager.getFamille("", Ref) != null)
            {
                var = manager.removeFamille(Ref);
                if (var == 1)
                {
                    MessageBox.Show("The associate family has been deleted");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("An error occured while deleting a family");
                }
            }
            else
            {
                MessageBox.Show("The associate family cannot be deleted because it was not found in the DB");
            }
            return var;
        }

        /// <summary>
        ///  Updates the dictionary of this controller
        /// </summary>
        public override void Refresh()
        {
            DicFam = manager.getAllFamilles();
        }

        /// <summary>
        /// Returns the dictionary to the associated view
        /// </summary>
        /// <returns>Dictionary of int and Familles</returns>
        public Dictionary<int, Familles> getDicFamilles()
        {
            return DicFam;
        }

        /// <summary>
        /// Sets the new Dictionary after refreshing it
        /// </summary>
        /// <param name="dict">New dictionary got after insertion or deletion of elements</param>
        public void setDicFamilles(Dictionary<int, Familles> dict)
        {
            DicFam = dict;
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<String> getColumnHeader()
        {
            return manager.getNameColumnTable("Familles");
        }
    }
}
