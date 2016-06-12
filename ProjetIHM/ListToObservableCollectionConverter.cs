﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Metier;

namespace ProjetIHM
{
    public class ListToObservableCollectionConverter
    {
        public static ObservableCollection<Intervenant> Convert(List<Intervenant> o) 
        {
            ObservableCollection<Intervenant> tmp = new ObservableCollection<Intervenant>();
            foreach (Intervenant i in o)
            {
                tmp.Add(i);
            }
            return tmp;
        }
        public static ObservableCollection<Internaute> Convert(List<Internaute> o)
        {
            ObservableCollection<Internaute> tmp = new ObservableCollection<Internaute>();
            foreach (Internaute i in o)
            {
                tmp.Add(i);
            }
            return tmp;
        }
    }
}
