using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    abstract class ControllerView
    {
        /// <summary>
        /// This attribut is a "list" of all objects found and stored in the DB
        /// </summary>
        protected Dictionary<String, Object> DicObject;

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
        abstract public bool DeleteElement(String RefObj);

        /// <summary>
        /// Returns the objects dictionary of the current controller object
        /// </summary>
        /// <returns>A list of objects to display</returns>
        public Dictionary<String, Object> getDictionary()
        {
            return DicObject;
        }

        /// <summary>
        /// Sets the objects dictionary of this controller object
        /// </summary>
        /// <param name="lObj">List of objects got from the DB to display</param>
        public void setDictionary(Dictionary<String, Object> lObj)
        {
            DicObject = lObj;
        }
    }
}
