using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public override bool AddElement(Object obj)
        {
            bool var;
            

            return var;
        }

        /// <summary>
        /// Applies changes on the element passed in parameter 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool ChangeElement(Object obj)
        {
            bool var;

            return var;   
        }

        public override bool DeleteElement(int RefObj)
        {
            bool var;

            return var;
        }

        protected override void Refresh()
        {
            DicFam = manager.getAllFamilles();
        }

        public Dictionary<int, Familles> getDicFamilles()
        {
            return DicFam;
        }

        public void setDicFamilles(Dictionary<int, Familles> dict)
        {
            DicFam = dict;
        }
    }
}
