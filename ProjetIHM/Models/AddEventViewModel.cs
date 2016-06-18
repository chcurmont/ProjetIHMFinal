using Library;
using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class AddEventViewModel:NotifyPropertyChangedBase
    {
        public DelegateCommand NormalCloseCommand { get; set; }
        public string AddOrEdit
        {
            get;
            set;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private string _name;

        public string Place
        {
            get
            {
                return _place;
            }
            set
            {
                _place = value;
                NotifyPropertyChanged("Place");
            }
        }
        private string _place;

        public string Date
        {
            get
            {
                if ((object)_date == null) { return ""; }
                return MaDate.DateToString(_date);
            }
            set
            {
                _date = new MaDate(value);
                NotifyPropertyChanged("Place");
            }
        }
        private MaDate _date;

        public bool NormalEnd
        {
            get
            {
                return _normalEnd;
            }
            set
            {
                _normalEnd = value;
            }
        }
        private bool _normalEnd;

        public int NbPlace
        {
            get
            {
                return _nbPlace;
            }
            set
            {
                _nbPlace = value;
                NotifyPropertyChanged("NbPlace");
            }
        }
        private int _nbPlace;

        public AddEventViewModel()
        {
            AddOrEdit = "Add";
            NormalEnd = false;
            NormalCloseCommand = new DelegateCommand(OnNormalCloseAction,CanExecuteNormalClose);
        }

        private void OnNormalCloseAction(object o)
        {
            NormalEnd = true;
        }

        public bool CanExecuteNormalClose(object o)
        {
            return !NormalEnd;
        }
    }
}
