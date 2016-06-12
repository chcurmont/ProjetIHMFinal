using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIHM
{
    class EventDAO
    {
        public static List<Evenement> GetAllEvent()
        {
            SiteWeb sw = new SiteWeb();
            return sw.MiseAJour();
        }
    }
}
