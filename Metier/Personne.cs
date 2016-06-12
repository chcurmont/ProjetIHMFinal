using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public abstract class Personne
    {
        #region Properties
        public string Nom
        {
            get;
            set;
        }

        public Personne(string nom)
        {
            Nom = nom;
        }
        #endregion
        #region Methods
        public abstract void Inscrire(Evenement e);

        public abstract void Annuler(Evenement e);
        #endregion
    }
}
