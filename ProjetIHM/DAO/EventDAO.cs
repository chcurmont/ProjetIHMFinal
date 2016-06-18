using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    class EventDAO
    {
        public static List<Evenement> GetAllEvent()
        {
            SiteWeb sw = new SiteWeb();
            return sw.MiseAJour();
        }

        public static void SetAllEvent(List<Evenement> l)
        {
            SiteWeb.MiseAjourReverse(l);
        }
    }
}
