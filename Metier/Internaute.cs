using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Internaute :Personne
    {
        public List<Evenement> evenements;

        public Internaute(string nom) :base(nom)
        {
            evenements = new List<Evenement>();
        }

        /*
         *But: Inscrire un internaute à un Evenement.
         *Paramètre:-Evenement e: l'évenement pour lequel l'internaute s'inscrit.
         *Retour: void.
        */
        public override void Inscrire(Evenement e)
        {
            e.InscrireInternaute(this);
        }

        /*
         *But: retirer l'internaute d'un Evenement.
         *Paramètre:-Evenement e: L'Evenement pour lequel l'internaute se désinscrit.
         *Retour: void.
        */
        public override void Annuler(Evenement e)
        {
            e.AnnulerInternaute(this);
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
