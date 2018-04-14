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
        /// <param name="Obj">Object to add in the DB.</param>
        public override void AddElement(object Obj)
        {
            SousFamilles Subfam = (SousFamilles)Obj;
            SousFamilles ResSubFam = Manager.GetSousFamille(Subfam.Nom);

            if (ResSubFam == null)
                Manager.InsertSousFamille(ResSubFam);
            else
                MessageBox.Show("This object already exists in the DB");
        }

        /// <summary>
        /// Applies changes on the similar object stored on the DB with the object in parameter 
        /// </summary>
        /// <param name="Obj">Object with the changes</param>
        /// <returns>Returns true if done, false else</returns>
        public override int ChangeElement(object Obj)
        {
            int Var;
            SousFamilles SousFamille = (SousFamilles)(Obj);
            SousFamilles SousFam = Manager.GetSousFamille(SousFamille.Nom);

            if (SousFam != null)
            {
                Var = Manager.UpdateSousFamilles(SousFamille);

                if (Var == 1)
                    MessageBox.Show("The element in the DB has been modified");
                else
                    MessageBox.Show("An error occured while the program was changing the values");
            }
            else
            {
                MessageBox.Show("The element to modify does not exist in the DB");
                Var = -1;
            }
            return Var;
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
        /// <returns>Dictionary of int and SubFamily</returns>
        public Dictionary<int, SousFamilles> GetAllSousFamilles()
        {
            return Manager.GetAllSousFamilles();
        }

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        public override List<string> GetColumnHeader()
        {
            return Manager.GetNameColumnTable("SousFamilles");
        }

        /// <summary>
        /// Get one sub family by his id
        /// </summary>
        /// <param name="Id">The id of the sub family</param>
        /// <returns>The sub family founded</returns>
        public SousFamilles GetSubFamily(int Id)
        {
            return Manager.GetSousFamille(Id: Id);
        }
    }
}
