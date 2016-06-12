using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Lieu:IEquatable<Lieu>,IComparable<Lieu>
    {
        #region Properties
        public string Nom
        {
            get;
            set;
        }

        public Lieu(string nom)
        {
            Nom = nom;
        }
        #endregion
        #region Methods
        /*
         *But: Convertir un type Lieu en type string.
         *Paramètre: aucun.
         *Retour: string.
        */
        public override string ToString()
        {
            return Nom;
        }

        /// <summary>
        /// returns a hash code in order to use this class in hash table
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode();
        }

        /// <summary>
        /// checks if the "right" object is equal to this Sommet or not
        /// </summary>
        /// <param name="right">the other object to be compared with this Sommet</param>
        /// <returns>true if equals, false if not</returns>
        public override bool Equals(object right)
        {
            if (!(right is Lieu))
            {
                return false;
            }
            return Equals((Lieu)right);
        }

        /// <summary>
        /// checks if this Sommet is equal to the other Sommet
        /// </summary>
        /// <param name="other">the other Sommet to be compared with</param>
        /// <returns>true if equals</returns>
        public bool Equals(Lieu other)
        {
            return (this.Nom.Equals(other.Nom));
        }

        /// <summary>
        /// opérateur ==
        /// </summary>
        /// <param name="sommet1"></param>
        /// <param name="sommet2"></param>
        /// <returns>true if equals</returns>
        public static bool operator ==(Lieu l1, Lieu l2)
        {
            return l1.Equals(l2);
        }

        /// <summary>
        /// opérateur !=
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns>true if different</returns>
        public static bool operator !=(Lieu l1, Lieu l2)
        {
            return !l1.Equals(l2);
        }

        /*
         *But: Comparer le nom d'un lieu à celui d'un autre passé en paramètre.
         *Paramètre:-Lieu other: Le lieu à  comparer avec le lieu contextuel.
         *Retour: int.
        */
        public int CompareTo(Lieu other)
        {
            return Nom.CompareTo(other.Nom);
        }
        #endregion
    }
}
