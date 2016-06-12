using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Intervenant :Personne,IEquatable<Intervenant>
    {
        public string TitreDIntervention
        { get;
          set;
        }


        public Intervenant(string nom,string titre) :base(nom)
        {
            TitreDIntervention = titre;
        }
 
        /*
         *But: Ajouter un intervenant à un Evenement.
         *Paramètres:-Evenement e: L'Evenement auquel ajouter l'intervenant.
         *Retour: void.
        */
        public override void Inscrire(Evenement e)
        {
            e.InscrireIntervant(this);
        }

        /*
         *But: Retirer un intervenant d'un Evenement.
         *Paramètre:-Evenement e: L'Evenement duquel retirer l'intervenant.
         *Retour: void.
        */
        public override void Annuler(Evenement e)
        {
            e.AnnulerIntervenant(this);
        }

        /*
         *But: Convertir un type Intervenant en type string.
         *Paramètre: aucun.
         *Retour: string.
        */
        public override string ToString()
        {
            return Nom;
        }

        //ancien tostring:
        /*public override string ToString()
        {
            if (TitreDIntervention != "/")
               return Nom + "\tTitre d'intervention : " + TitreDIntervention;
            else
                return Nom;
        }
        */


        /// <summary>
        /// checks if the "right" object is equal to this Sommet or not
        /// </summary>
        /// <param name="right">the other object to be compared with this Sommet</param>
        /// <returns>true if equals, false if not</returns>
        public override bool Equals(object right)
        {
            if (!(right is Intervenant))
            {
                return false;
            }
            return Equals((Intervenant)right);
        }

        /// <summary>
        /// checks if this Sommet is equal to the other Sommet
        /// </summary>
        /// <param name="other">the other Sommet to be compared with</param>
        /// <returns>true if equals</returns>
        public bool Equals(Intervenant other)
        {
            if ((object)other == null)
            {
                if ((object)this == null)
                {
                    return true;
                }
                return false;
            }
            return (this.Nom.Equals(other.Nom));
        }

        /// <summary>
        /// opérateur ==
        /// </summary>
        /// <param name="sommet1"></param>
        /// <param name="sommet2"></param>
        /// <returns>true if equals</returns>
        public static bool operator ==(Intervenant i1, Intervenant i2)
        {
            if ((object)i1 == null)
            {
                if ((object)i2 == null)
                {
                    return true;
                }
                return false;
            }
            return i1.Equals(i2);
        }

        /// <summary>
        /// opérateur !=
        /// </summary>
        /// <param name="sommet1"></param>
        /// <param name="sommet2"></param>
        /// <returns>true if different</returns>
        public static bool operator !=(Intervenant i1, Intervenant i2)
        {
            if ((object)i1 == null)
            {
                if ((object)i2 == null)
                {
                    return false;
                }
                return true;
            }
            return !i1.Equals(i2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
