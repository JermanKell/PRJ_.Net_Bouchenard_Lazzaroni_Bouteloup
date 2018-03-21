using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class Articles
    {
        private string description;
        private string reference;
        private int idSousFamille;
        private int idMarque;
        private double prixHT;
        private int quantite;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public int IdSousFamille
        {
            get { return idSousFamille; }
            set { idSousFamille = value; }
        }

        public int IdMarque
        {
            get { return idMarque; }
            set { idMarque = value; }
        }

        public double PrixHT
        {
            get { return prixHT; }
            set { prixHT = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set { quantite = value; }
        }
    }
}
