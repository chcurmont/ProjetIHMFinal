using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIHM
{
    class ExceptionViewModel
    {
        public string exceptionString
        {
            get;
            set;
        }

        public ExceptionViewModel(Exception E)
        {
            exceptionString = E.Message + E.StackTrace; ;
        }
    }
}
