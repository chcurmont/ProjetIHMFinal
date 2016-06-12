using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIHM
{
    class AddViewModel:NotifyPropertyChangedBase
    {
        public DelegateCommand IntervenantCommand { get; set; }
        public DelegateCommand InternauteCommand { get; set; }
        public DelegateCommand NormalCloseCommand { get; set; }

        public string Intervention
        {
            get;
            set;
        }
        public string Nom
        {
            get;
            set;
        }
        public bool TestI
        {
            get { return mTestI; }
            set
            {
                mTestI = value;
                if ((object)this != null)
                {
                    NotifyPropertyChanged("Intervention");
                    NotifyPropertyChanged("TestI");
                    InternauteCommand.RaiseCanExecuteChanged();
                    IntervenantCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private bool mTestI;

        public bool NormalEnd
        {
            get { return mNormalEnd; }
            set
            {
                mNormalEnd = value;
            }
        }
        private bool mNormalEnd;

        public AddViewModel(AddView ajouter)
        {
            Intervention = " ";
            Nom = " ";
            IntervenantCommand = new DelegateCommand(OnIntervenantAction, CanExecuteIntervenant);
            InternauteCommand = new DelegateCommand(OnInternauteAction, CanExecuteInternaute);
            NormalCloseCommand = new DelegateCommand(OnNormalCloseAction, CanExecuteNormalClose);
            TestI = true;
            NormalEnd = false;
        }

        public void OnIntervenantAction(object o)
        {
            Intervention = " ";
            TestI = false;
        }
        public void OnInternauteAction(object o)
        {
            TestI = true;
        }
        public void OnNormalCloseAction(object o)
        {
            NormalEnd = true;
        }

        public bool CanExecuteInternaute(object o)
        {
            if (!TestI)
            {
                return true;
            }
            return false;
        }
        public bool CanExecuteIntervenant(object o)
        {
            if (!TestI)
            {
                return false;
            }
            return true;
        }
        public bool CanExecuteNormalClose(object o)
        {
            return !NormalEnd;
        }
    }
}
