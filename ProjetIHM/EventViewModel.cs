using Library;
using Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIHM
{
    public class EventViewModel:NotifyPropertyChangedBase
    {
        #region commands
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        #endregion

        public List<Evenement> ListeEvent
        {
            get { return mListeEvent; }
            set
            {
                mListeEvent = value;
                NotifyPropertyChanged("ListeEvent");
                NotifyPropertyChanged("Event");
            }
        }
        private List<Evenement> mListeEvent;

        public Internaute spectator
        {
            get { return mSpectator; }
            set
            {
                mSpectator = value;
                NotifyPropertyChanged("spectator");
                NotifyPropertyChanged("Event.Inscrits");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Internaute mSpectator;

        public Intervenant speaker
        {
            get { return mSpeaker; }
            set
            {
                mSpeaker = value;
                NotifyPropertyChanged("speaker");
                NotifyPropertyChanged("Event.Intervenants");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Intervenant mSpeaker;

        public Evenement Event
        {
            get { return mEvent; }
            set
            {
                mEvent = value;
                NotifyPropertyChanged("Event");
                NotifyPropertyChanged("ListeEvent");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if ((object)ListeSpeaker == null) { ListeSpeaker=new ObservableCollection<Intervenant>(); }
                if ((object)ListeSpectator == null) { ListeSpectator=new ObservableCollection<Internaute>(); }
                if (mEvent.Inscrits != ListeSpectator.ToList()) { ListeSpectator = ListToObservableCollectionConverter.Convert(mEvent.Inscrits); }
                if (mEvent.Intervenants != ListeSpeaker.ToList()) { ListeSpeaker = ListToObservableCollectionConverter.Convert(mEvent.Intervenants); }
                NotifyPropertyChanged("Event");
                NotifyPropertyChanged("ListeEvent");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Evenement mEvent;

        public ObservableCollection<Internaute> ListeSpectator
        {
            get { return mListeSpectator; }
            set
            {
                mListeSpectator = value;
                if (Event == null)
                {
                    return;
                }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("spectator");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if ((object)Event.Inscrits != null) { if (mListeSpectator != ListToObservableCollectionConverter.Convert(Event.Inscrits)) { Event.Inscrits = mListeSpectator.ToList(); } }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("spectator");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<Internaute> mListeSpectator;

        public ObservableCollection<Intervenant> ListeSpeaker
        {
            get { return mListeSpeaker; }
            set
            {
                mListeSpeaker = value;
                NotifyPropertyChanged("ListeSpeaker");
                NotifyPropertyChanged("speaker");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if (Event == null) { return; }
                if ((object)Event.Intervenants!=null) { if (mListeSpeaker!= ListToObservableCollectionConverter.Convert(Event.Intervenants)) { Event.Intervenants = mListeSpeaker.ToList(); } }
                NotifyPropertyChanged("ListeSpeaker");
                NotifyPropertyChanged("speaker");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<Intervenant> mListeSpeaker;
        
        public EventViewModel()
        {
            AddCommand = new DelegateCommand(OnAddCommand, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanExecuteEdit);
            DeleteCommand = new DelegateCommand(OnDeleteCommand, CanExecuteDelete);
            ListeEvent = EventDAO.GetAllEvent();
            ListeSpeaker = ListToObservableCollectionConverter.Convert(new List<Intervenant>());
            ListeSpectator = ListToObservableCollectionConverter.Convert(new List<Internaute>());
        }

        private void OnAddCommand(object o)
        {
            try
            {
                AddView ajouter = new AddView();
                AddViewModel modele = new AddViewModel(ajouter);
                ajouter.DataContext = modele;
                ajouter.ShowDialog();
                if (!modele.NormalEnd)
                {
                    return;
                }
                if (modele.Nom == " ") { throw (new ArgumentException("Vous n'avez pas rentrez de nom. Erreur: \n")); }
                if (!modele.TestI)
                {
                    if (modele.Intervention == " ") { throw (new ArgumentException("Vous n'avez pas rentrez d'intervention. Erreur: \n")); }
                    List<Intervenant> tmp = Event.Intervenants;
                    tmp.Add(new Intervenant(modele.Nom, modele.Intervention));
                    Event.Intervenants = tmp;
                }
                else
                {
                    List<Internaute> tmp = Event.Inscrits;
                    tmp.Add(new Internaute(modele.Nom));
                    Event.Inscrits = tmp;
                }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("ListeSpeaker");
            }
            catch (Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
            
        }

        private void OnEditCommand(object o)
        {
            try
            {
                EditView modifier = new EditView();
                EditViewModel modele = new EditViewModel(modifier);
                modifier.DataContext = modele;
                modifier.ShowDialog();
                if (!modele.NormalEnd)
                {
                    return;
                }
                if (modele.Nom == " ") { throw (new ArgumentException("Vous n'avez pas rentrez de nom. Erreur: \n")); }
                if (!modele.TestI)
                {
                    if (speaker == null) { throw (new ArgumentException("Vous n'avez pas sélectioner d'intervenants. Erreur:\n")); }
                    if(speaker.Nom==modele.Nom && speaker.TitreDIntervention == modele.Intervention) { throw (new ArgumentException("Vous n'avez pas modifier l'intervenant. Erreur:\n")); }
                    if (modele.Intervention == " ") { throw (new ArgumentException("Vous n'avez pas rentrez d'intervention. Erreur: \n")); }
                    ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == speaker.Nom && Intervenant.TitreDIntervention == speaker.TitreDIntervention))].Nom = modele.Nom;
                    ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == speaker.Nom && Intervenant.TitreDIntervention == speaker.TitreDIntervention))].TitreDIntervention = modele.Intervention;
                    speaker = ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == modele.Nom && Intervenant.TitreDIntervention == modele.Intervention))];
                }
                else
                {
                    if (spectator == null) { throw (new ArgumentException("Vous n'avez pas sélectionner de spectateurs. Erreur:\n")); }
                    if (spectator.Nom == modele.Nom) { throw (new ArgumentException("Vous n'avez pas modifier le spectateur. Erreur:\n")); }
                    ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == spectator.Nom))].Nom = modele.Nom;
                    spectator = ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == modele.Nom))];
                }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("ListeSpeaker");
            }
            catch (Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        private void OnDeleteCommand(object o)
        {
            try
            {
                DeleteView supprimer = new DeleteView();
                DeleteViewModel modele = new DeleteViewModel(supprimer);
                supprimer.DataContext = modele;
                supprimer.ShowDialog();
                if (!modele.NormalEnd)
                {
                    return;
                }
               // if (modele.Nom == " ") { throw (new ArgumentException("Vous n'avez pas rentrez de nom. Erreur: \n")); }
                if (!modele.TestI)
                {
                    ListeSpeaker.RemoveAt(ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == speaker.Nom)));
                    Event.Intervenants = ListeSpeaker.ToList();
                }
                else
                {
                    ListeSpectator.RemoveAt(ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == spectator.Nom)));
                    Event.Inscrits = ListeSpectator.ToList();
                }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("ListeSpeaker");
            }
            catch (Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        private bool CanExecuteAdd(object o)
        {
            return Event != null;
        }

        private bool CanExecuteEdit(object o)
        {
            return speaker != null || spectator != null;
        }

        private bool CanExecuteDelete(object o)
        {
            return speaker != null || spectator != null;
        }
    }
}
