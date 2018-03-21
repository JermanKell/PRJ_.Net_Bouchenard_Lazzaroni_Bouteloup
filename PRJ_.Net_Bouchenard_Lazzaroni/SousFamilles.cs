using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class SousFamilles
    {
        private int id;
        private int idFamille;
        private string nom;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdFamille
        {
            get { return idFamille; }
            set { idFamille = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
    }
}
