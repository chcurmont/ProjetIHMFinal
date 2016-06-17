using Library;
using Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetIHM
{
    public class EventViewModel:NotifyPropertyChangedBase
    {
        #region commands
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand MainVocalSearch { get; set; }
        #endregion

        #region RecognitionTools
        public SpeechToTextTools SpeechRecognitionTool
        {
            get
            {
                return _speechRecognitionTool;
            }
            set
            {
                _speechRecognitionTool = value;
            }
        }
        private SpeechToTextTools _speechRecognitionTool;
        #endregion

        #region ListAttributes
        public List<Evenement> ListeEvent
        {
            get { return _listeEvent; }
            set
            {
                _listeEvent = value;
                NotifyPropertyChanged("ListeEvent");
                NotifyPropertyChanged("Event");
            }
        }
        private List<Evenement> _listeEvent;

        public ObservableCollection<Evenement> ListeEventToShow
        {
            get
            {
                return _listeEventToShow;
            }
            set
            {
                _listeEventToShow = value;
                if(ListeEventToShow.Count()>0) { Event = ListeEventToShow[0]; }
                NotifyPropertyChanged("ListeEvent");
                NotifyPropertyChanged("Event");
            }
        }
        private ObservableCollection<Evenement> _listeEventToShow;

        public ObservableCollection<Internaute> ListeSpectator
        {
            get { return _listeSpectator; }
            set
            {
                _listeSpectator = value;
                if (Event == null)
                {
                    return;
                }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("Spectator");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if ((object)Event.Inscrits != null) { if (_listeSpectator != ListToObservableCollectionConverter.Convert(Event.Inscrits)) { Event.Inscrits = _listeSpectator.ToList(); } }
                NotifyPropertyChanged("ListeSpectator");
                NotifyPropertyChanged("Spectator");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<Internaute> _listeSpectator;

        public ObservableCollection<Intervenant> ListeSpeaker
        {
            get { return _listeSpeaker; }
            set
            {
                _listeSpeaker = value;
                NotifyPropertyChanged("ListeSpeaker");
                NotifyPropertyChanged("Speaker");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if (Event == null) { return; }
                if ((object)Event.Intervenants != null) { if (_listeSpeaker != ListToObservableCollectionConverter.Convert(Event.Intervenants)) { Event.Intervenants = _listeSpeaker.ToList(); } }
                NotifyPropertyChanged("ListeSpeaker");
                NotifyPropertyChanged("Speaker");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<Intervenant> _listeSpeaker;

        #endregion

        #region Attributes
        public Internaute Spectator
        {
            get { return _spectator; }
            set
            {
                _spectator = value;
                NotifyPropertyChanged("Spectator");
                NotifyPropertyChanged("Event.Inscrits");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Internaute _spectator;

        public Intervenant Speaker
        {
            get { return _speaker; }
            set
            {
                _speaker = value;
                NotifyPropertyChanged("Speaker");
                NotifyPropertyChanged("Event.Intervenants");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Intervenant _speaker;

        public Evenement Event
        {
            get { return _event; }
            set
            {
                _event = value;
                NotifyPropertyChanged("Event");
                NotifyPropertyChanged("ListeEventToShow");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if ((object)ListeSpeaker == null) { ListeSpeaker=new ObservableCollection<Intervenant>(); }
                if ((object)ListeSpectator == null) { ListeSpectator=new ObservableCollection<Internaute>(); }
                if ((object)_event == null) { return; }
                if (_event.Inscrits != ListeSpectator.ToList()) { ListeSpectator = ListToObservableCollectionConverter.Convert(_event.Inscrits); }
                if (_event.Intervenants != ListeSpeaker.ToList()) { ListeSpeaker = ListToObservableCollectionConverter.Convert(_event.Intervenants); }
                NotifyPropertyChanged("Event");
                NotifyPropertyChanged("ListeEventToShow");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private Evenement _event;

        public string SearchWord
        {
            get
            {
                return _searchWord;
            }
            set
            {
                _searchWord = value;
                if(_searchWord=="" || _searchWord == null) { return; }
                UpdateListeEvent();
                NotifyPropertyChanged("SearchWord");
                MainVocalSearch.RaiseCanExecuteChanged();
            }
        }
        private string _searchWord;

        #endregion

        public EventViewModel()
        {
            SpeechRecognitionTool = new SpeechToTextTools();
            AddCommand = new DelegateCommand(OnAddCommand, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanExecuteEdit);
            DeleteCommand = new DelegateCommand(OnDeleteCommand, CanExecuteDelete);
            MainVocalSearch = new DelegateCommand(OnMainVocalSearch,CanExecuteMainVocalSearch);
            ListeEvent = EventDAO.GetAllEvent();
            ListeEventToShow = ListToObservableCollectionConverter.Convert(ListeEvent);
            ListeSpeaker = ListToObservableCollectionConverter.Convert(new List<Intervenant>());
            ListeSpectator = ListToObservableCollectionConverter.Convert(new List<Internaute>());
        }

        #region Methods
        private void UpdateListeEvent()
        {
            ObservableCollection<Evenement> tmp = new ObservableCollection<Evenement>();
            Regex r = new Regex("^.*" + SearchWord.ToLower() + ".*$");
            for(int i = 0; i < ListeEvent.Count(); i++)
            {
                if (r.IsMatch(ListeEvent[i].Nom.ToLower()))
                {
                    tmp.Add(ListeEvent[i]);
                }
            }
            ListeEventToShow = tmp;
            NotifyPropertyChanged("ListeEventToShow");
        }

        private string FindGoodString(List<string> s)
        {
            Regex r;
            for(int i = 0; i < s.Count(); i++)
            {
                r = new Regex("^.*" + s[i] + ".*$");
                for (int j=0; j < ListeEvent.Count(); j++)
                {
                    if (r.IsMatch(ListeEvent[j].Nom))
                    {
                        return s[i];
                    }
                }
            }
            return "";
        }
        #endregion

        #region CommandAction
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
                    if (Speaker == null) { throw (new ArgumentException("Vous n'avez pas sélectioner d'intervenants. Erreur:\n")); }
                    if(Speaker.Nom==modele.Nom && Speaker.TitreDIntervention == modele.Intervention) { throw (new ArgumentException("Vous n'avez pas modifier l'intervenant. Erreur:\n")); }
                    if (modele.Intervention == " ") { throw (new ArgumentException("Vous n'avez pas rentrez d'intervention. Erreur: \n")); }
                    ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == Speaker.Nom && Intervenant.TitreDIntervention == Speaker.TitreDIntervention))].Nom = modele.Nom;
                    ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == Speaker.Nom && Intervenant.TitreDIntervention == Speaker.TitreDIntervention))].TitreDIntervention = modele.Intervention;
                    Speaker = ListeSpeaker[ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == modele.Nom && Intervenant.TitreDIntervention == modele.Intervention))];
                }
                else
                {
                    if (Spectator == null) { throw (new ArgumentException("Vous n'avez pas sélectionner de spectateurs. Erreur:\n")); }
                    if (Spectator.Nom == modele.Nom) { throw (new ArgumentException("Vous n'avez pas modifier le spectateur. Erreur:\n")); }
                    ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == Spectator.Nom))].Nom = modele.Nom;
                    Spectator = ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == modele.Nom))];
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
                    ListeSpeaker.RemoveAt(ListeSpeaker.ToList().FindIndex(Intervenant => (Intervenant.Nom == Speaker.Nom)));
                    Event.Intervenants = ListeSpeaker.ToList();
                }
                else
                {
                    ListeSpectator.RemoveAt(ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == Spectator.Nom)));
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

        private void OnMainVocalSearch(object o)
        {
            try
            {
                SpeechRecognitionTool.RecognizeText.Clear();
                SpeechRecognitionTool.RecognizeText[0] = "";
                SpeechRecognitionTool.start();
                while (SpeechRecognitionTool.IsMicroUse) { }
                if(SpeechRecognitionTool.RecognizeText==null || ((SpeechRecognitionTool.RecognizeText.Count==1 || SpeechRecognitionTool.Error) && SpeechRecognitionTool.RecognizeText[0] == ""))
                {
                    throw (new Exception("Erreur de reconnaissance vocale:\n"));
                }
                SearchWord = FindGoodString(SpeechRecognitionTool.RecognizeText);
            }
            catch(Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        #endregion

        #region CommandTest
        private bool CanExecuteAdd(object o)
        {
            return Event != null;
        }

        private bool CanExecuteEdit(object o)
        {
            return Speaker != null || Spectator != null;
        }

        private bool CanExecuteDelete(object o)
        {
            return Speaker != null || Spectator != null;
        }

        private bool CanExecuteMainVocalSearch(object o)
        {
            return SpeechRecognitionTool.IsMicroUse;
        }
        #endregion
    }
}
