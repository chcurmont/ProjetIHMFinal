using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metier
{
    public class SiteWeb
    {
        public List<Evenement> Evenements;

        public SiteWeb()
        {
            Evenements = new List<Evenement>();
        }

        public override string ToString()
        {
            string chaine = "Liste des évènements : \n\n";
            foreach (Evenement e in Evenements)
            {
                chaine += e;
            }
            return chaine;
        }

        /*
         *But: Effectuez une requête LinQ pour retourner tous les Evenements se déroulant un moi donné.
         *Paramètre:-int mois: Le mois pour lequel rechercher des Evenements.
         *Retour: List<Evenement>.
        */
        public List<Evenement> AfficherMois(int mois)
        {
            return Evenements.Where(e => e.Date.Mois == mois).ToList();
        }

        /*
         *But: Retournez par une requète LinQ la liste des Evenement ou il y a au moins un intervenant ordonnée par le lieu, puis par la date.
         *Paramètre: aucun.
         *Retour: List<Evenement>.
        */
        public List<Evenement> AfficherIntervenant()
        {
            return Evenements.Where(e => e.Intervenants.Count == 1).OrderBy(e => e.Lieu).OrderBy(e => e.Date).ToList();
        }

        public List<Evenement> AfficherLieu()
        {
            List<Evenement> l = Evenements.OrderBy(e => e.Date).ToList();
            l.GroupBy(e => e.Lieu).OrderBy(group => group.Key).ToList();
            return l; 
        }

        public List<Evenement> AfficherLieuMois(int mois, string lieu)
        {
            List<Evenement> l = Evenements.Where(e => e.Date.Mois == mois && e.Lieu.Nom == lieu).ToList();
            l.GroupBy(e => e.Date.Jour).OrderBy(group => group.Key).ToList();
            return l;
        }




       
        /*
         *But: Demander à un WebService de lire un fichier.
         *Paramètre: aucun.
         *Retour: void.
        */
        public List<Evenement> MiseAJour()
        {
            Evenements = WebService.LireFicher("Donnee.txt");
            return Evenements;
        }

       public static void MiseAjourReverse(List<Evenement> l)
        {
            WebService.EnregistrerFichier(l, "Donnee.txt");
        }
    }
}
