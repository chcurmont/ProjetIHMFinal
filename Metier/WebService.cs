﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class WebService
    {

        /*
         *But:Lire un fichier et en retourner les Evenements.
         *Paramètre:-string nomfichier: nom du fichier.
         *Retour: List<Evenement>.
        */
        public static List<Evenement> LireFicher(string nomfichier)
        {
            string ligne;
            List<Evenement> listtemp = new List<Evenement>();

            System.IO.StreamReader file = new System.IO.StreamReader(nomfichier);
            string[] elementEvent; 
            while ((ligne = file.ReadLine()) != null)
            {

                elementEvent= ligne.Split(new[] { '|' });

                MaDate date = new MaDate(elementEvent[1]);
                int nbP = int.Parse(elementEvent[2]);
                Lieu lieu = new Lieu(elementEvent[3]);

                Evenement e = new Evenement(elementEvent[0], date, nbP, lieu);

                for (int i = 4; i < elementEvent.Length; i += 2)
                {
                    Intervenant inter = new Intervenant(elementEvent[i], elementEvent[i + 1]);
                    e.Intervenants.Add(inter);
                }

                listtemp.Add(e);
            }
            file.Close();

            return listtemp;
        }




    }

}
