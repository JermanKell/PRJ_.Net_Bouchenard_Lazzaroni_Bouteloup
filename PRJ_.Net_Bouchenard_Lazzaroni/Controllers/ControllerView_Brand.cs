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
            int var;
            Marques brand = (Marques)obj;
            Marques resBrand = manager.getMarque(brand.Nom);

            if (resBrand == null)
            {
                var = manager.insertMarque(resBrand);
                if (var != 0)
                    MessageBox.Show("Insertion of this object succeed");
                else
                    MessageBox.Show("Insertion of this object failed");
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
        public override int ChangeElement(object obj)
        {
            int var;
            Marques brand = (Marques)(obj);
            Marques resBrand = manager.getMarque(brand.Nom);

            if (resBrand != null)
            {
                var = manager.updateMarque(brand);

                if (var == 1)
                    MessageBox.Show("The element in the DB has been modified");
                else
                    MessageBox.Show("An error occured while the program was changing the values");
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
            // TODO
            return 0;
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
