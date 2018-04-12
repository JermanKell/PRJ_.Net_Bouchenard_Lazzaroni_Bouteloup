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
        protected Dictionary<int, Object> DicObject;             // INUTILE?!

        /// <summary>
        /// Handles all requests to do on the DB.
        /// </summary>
        protected DBManager manager;

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
        /// <returns>Returns the row id of the most recent successful element inserted</returns>
        abstract public int AddElement(Object obj);


        /// <summary>
        /// Changes the values about an element stored in the DB depending on the reference
        /// </summary>
        /// <param name="obj">Elements of this objects have to change in the DB.</param>
        /// <returns>Returns the number of elements updated</returns>
        abstract public int ChangeElement(Object obj);

        /// <summary>
        /// Deletes the element stored in the DB with its reference passed in arguments
        /// <param name="RefObj">Reference of the object to delete from the DB.</param>
        /// <returns>Returns the number of elements deleted</returns>
        abstract public int DeleteElement(string RefObj);

        /// <summary>
        /// Get name's column of one table
        /// </summary>
        /// <param name="tableName"> Name of the table in the database </param>
        abstract public List<string> getColumnHeader();
    }
}