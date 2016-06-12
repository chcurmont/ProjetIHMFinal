using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class MaDate: IComparable<MaDate> , IEquatable<MaDate>
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public int Jour { get; set; }
        public int Heure { get; set; }
        public int Minute { get; set; }

        public MaDate(string date)
        {
            string[] elementDate = date.Split(new[] { '-' });
            Annee = int.Parse(elementDate[0]);
            Mois = int.Parse(elementDate[1]);
            Jour = int.Parse(elementDate[2]);
            Heure = int.Parse(elementDate[3]);
            Minute = int.Parse(elementDate[4]);
        }

        /*
         *But: Convertir un type MaDate en type string.
         *Paramètre: aucun.
         *Retour: string.
        */
        public override string ToString()
        {
            return (Jour<10?"0"+Jour.ToString():Jour.ToString()) + "/" + (Mois<10?"0"+Mois.ToString():Mois.ToString()) + "/" + Annee + " " + (Heure<10?"0"+Heure.ToString():Heure.ToString()) + ":" + (Minute<10?"0"+Minute.ToString():Minute.ToString());
        }




        /// <summary>
        /// returns a hash code in order to use this class in hash table
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Minute+Heure+Jour+Mois+Annee;
        }

        /// <summary>
        /// checks if the "right" object is equal to this Sommet or not
        /// </summary>
        /// <param name="right">the other object to be compared with this Sommet</param>
        /// <returns>true if equals, false if not</returns>
        public override bool Equals(object right)
        {
            if (!(right is MaDate))
            {
                return false;
            }
            return Equals((MaDate)right);
        }

        /// <summary>
        /// checks if this Sommet is equal to the other Sommet
        /// </summary>
        /// <param name="other">the other Sommet to be compared with</param>
        /// <returns>true if equals</returns>
        public bool Equals(MaDate other)
        {
            return (this.Annee == other.Annee && this.Mois == other.Mois && this.Jour == other.Jour && this.Heure == other.Heure && this.Minute == other.Minute);
        }

        /// <summary>
        /// opérateur ==
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>true if equals</returns>
        public static bool operator ==(MaDate d1, MaDate d2)
        {
            return d1.Equals(d2);
        }

        /// <summary>
        /// opérateur !=
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>true if different</returns>
        public static bool operator !=(MaDate d1, MaDate d2)
        {
            return !d1.Equals(d2);
        }


        /*
         *But: Comparer deux dates entre elles, nottement pour pouvoir trier une liste de date avec la méthode sort().
         *Paramètre:-MaDate other: La date qui doit être comparer à la date qui forme le contexte actuel.
         *Retour: int: 0 si les 2 dates ont des attributs égaux, -1 si la date contextuelle est inférieure à la date passée en paramètre ou 1 si la date contextuelle est suppérieure à la date passée en paramètre.
        */
       public int CompareTo(MaDate other)
        {
            if (this==other)
                return 0;
            else
            {
                if (Annee < other.Annee)
                    return -1;
                else
                {
                    if (Annee > other.Annee)
                        return 1;
                    else
                    {
                        if (Mois < other.Mois)
                            return -1;
                        else
                        {
                            if (Mois > other.Mois)
                                return 1;
                            else
                            {
                                if (Jour < other.Jour)
                                    return -1;
                                else
                                {
                                    if (Jour > other.Jour)
                                        return 1;
                                    else
                                    {
                                        if (Heure < other.Heure)
                                            return -1;
                                        else
                                        {
                                            if (Heure > other.Heure)
                                                return 1;
                                            else
                                            {
                                                if (Minute < other.Minute)
                                                    return -1;
                                                else
                                                    return 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
