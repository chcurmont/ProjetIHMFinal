using Library;
using Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WaitingLineViewModel:NotifyPropertyChangedBase
    {
        public ObservableCollection<Internaute> MainList
        {
            get
            {
                return _mainList;
            }
            set
            {
                _mainList = value;
                NotifyPropertyChanged("MainList");
            }
        }
        private ObservableCollection<Internaute> _mainList;

        public Internaute Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                NotifyPropertyChanged("Selected");
            }
        }
        private Internaute _selected;
    }
}
