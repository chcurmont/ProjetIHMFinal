using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Evenement : IEquatable<Evenement>
    {
        #region Properties
        public string Nom
        {
            get;
            set;
        }

        public MaDate Date
        {
            get;
            set;
        }

        public int NbPlaces
        {
            get;
            set;
        }

        public Lieu Lieu
        {
            get;
            set;
        }   

        public List<Intervenant> Intervenants
        {
            get;
            set;
        }

        public List<Internaute> Inscrits
        {
            get;
            set;
        }

        public Queue<Internaute> ListeAttente
        {
            get;
            set;
        }
        #endregion
        #region Constructor
        public Evenement(string nom, MaDate date, int nbplaces, Lieu lieu)
        {
            Nom = nom;
            Date = date;
            NbPlaces = nbplaces;
            Lieu = lieu;

            Intervenants = new List<Intervenant>();
            Inscrits = new List<Internaute>();
            ListeAttente = new Queue<Internaute>();

        }
        #endregion
        #region Methods
        /*
         *But: ajouter un internaute soit à la file d'attente s'il n'y a plus de place, soit dans la liste des inscrits sinon.
         *Paramètre:-Internaute internaute: L'internaute à ajouter.
         *Retour: void.
        */
        public void InscrireInternaute(Internaute internaute)
        {
                if (NbPlaces > 0)
                {
                    Inscrits.Add(internaute);
                    NbPlaces--;
                }

                else
                {
                    ListeAttente.Enqueue(internaute);
                }

                internaute.evenements.Add(this);
        }

        /*
         *But: Ajouter un intervenant à la liste d'intervenant.
         *Paramètre:- Intervenant intervenant: L'intervenant à ajouter.
         *Retour: void.
        */
        public void InscrireIntervant(Intervenant intervenant)
        {
            Intervenants.Add(intervenant);
        }

        /*
         *But: Suprimmer un internaute de la liste et le remplacer par un autre internaute de la file d'attente représenter par le type Queue ListeAttente.
         *Paramètre:- Internaute internaute: l'internaute à retirer.
         *Retour: void.
        */
        public void AnnulerInternaute(Internaute internaute)
        {
            Inscrits.Remove(internaute);
            internaute.evenements.Remove(this);
            if (NbPlaces == 0)
            {
                Internaute i = ListeAttente.Dequeue();
                Inscrits.Add(i);
            }
            else
                NbPlaces++;
        }

        /*
         *But: Retirer un intervenant de la liste des intervenants.
         *Paramètre:- Intervenant intervenant: L'intervenant à retirer.
         *Retour: void.
        */
        public void AnnulerIntervenant(Intervenant intervenant)
        {
            Intervenants.Remove(intervenant);
        }

        /*
         *But: Convertir un type Evenement en type string.
         *Paramètre: aucun.
         *Retour: string.
        */
        public override string ToString()
        {
            return "- "+Nom;
        }

        //ancien tostring:
        /*public override string ToString()
        {
            string chaine = "Nom : " + Nom + " \nDate : " + Date + " \nLieu : " + Lieu + "\nNombre de places : " + NbPlaces;
            if(Intervenants.ToArray().Length>0)
                chaine+="\n  Liste des intervenants : ";
            foreach(Intervenant i in Intervenants)
            {
               chaine += "\nIntervenant n° "+(Intervenants.IndexOf(i)+1)+" : "+i ;
            }
            return chaine + "\n\n";
        }
        */







        /// <summary>
        /// returns a hash code in order to use this class in hash table
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return NbPlaces;
        }

        /// <summary>
        /// checks if the "right" object is equal to this Sommet or not
        /// </summary>
        /// <param name="right">the other object to be compared with this Sommet</param>
        /// <returns>true if equals, false if not</returns>
        public override bool Equals(object right)
        {
            if (right == null)
            {
                return false;
            }
            if (!(right is Evenement))
            {
                return false;
            }
            return Equals((Evenement)right);
        }

        /// <summary>
        /// checks if this Sommet is equal to the other Sommet
        /// </summary>
        /// <param name="other">the other Sommet to be compared with</param>
        /// <returns>true if equals</returns>
        public bool Equals(Evenement other)
        {
            if((object) other == null)
            {
                if((object) this == null)
                {
                    return true;
                }
                return false;
            }
            return (this.Nom.Equals(other.Nom) && this.Lieu.Equals(other.Lieu) && this.Date.Equals(other.Date));
        }

        /// <summary>
        /// opérateur ==
        /// </summary>
        /// <param name="sommet1"></param>
        /// <param name="sommet2"></param>
        /// <returns>true if equals</returns>
        public static bool operator ==(Evenement e1, Evenement e2)
        {
            if ((object)e1 == null)
            {
                if ((object)e2 == null)
                {
                    return true;
                }
                return false;
            }
            return e1.Equals(e2);
        }

        /// <summary>
        /// opérateur !=
        /// </summary>
        /// <param name="sommet1"></param>
        /// <param name="sommet2"></param>
        /// <returns>true if different</returns>
        public static bool operator !=(Evenement e1, Evenement e2)
        {
            if ((object) e1 ==null)
            {
                if ((object) e2 == null)
                {
                    return false;
                }
                return true;
            }
            return !e1.Equals(e2);
        }
        #endregion
    }
}
