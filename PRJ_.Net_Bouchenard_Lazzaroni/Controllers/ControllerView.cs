using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    abstract class ControllerView
    {
        /// <summary>
        /// This attribut is a "list" of all objects found and stored in the DB
        /// </summary>
        protected Dictionary<int, Object> DicObject = null;

        /// <summary>
        /// Handles all requests to do on the DB.
        /// </summary>
        protected DBManager manager = new DBManager();

        /// <summary>
        /// Constructor of this abstract class.
        /// </summary>
        public ControllerView()
        {
            manager = new DBManager();
        }

        /// <summary>
        ///  Adds an element in the DB depending on the value of the reference generated
        ///       Returns a boolean value: TRUE when succeed and false else
        /// </summary>
        /// <param name="obj">Object to add in the DB.</param>
        /// <returns>Returns true if done and false else</returns>
        abstract public bool AddElement(Object obj);


        /// <summary>
        /// Changes the values about an element stored in the DB depending on the reference
        /// </summary>
        /// <param name="obj">Elements of this objects have to change in the DB.</param>
        /// <returns>Returns true if done and false else</returns>
        abstract public bool ChangeElement(Object obj);
        

        /// <summary>
        /// Deletes the element stored in the DB with its reference passed in arguments
        ///     Returns a boolean value: TRUE when succeed and false else
        /// </summary>
        /// <param name="RefObj">Reference of the object to delete from the DB.</param>
        /// <returns>Returns true if done and false else</returns>
        abstract public bool DeleteElement(int RefObj);

        /// <summary>
        /// Gets a dictionary of all products families stored in the BD and store it in this controller.
        /// </summary>
        abstract protected void Refresh();
    }
}
