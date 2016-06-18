using Library;
using Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Factories;
using DAO;
using Models;
using ProjetIHM;
using Views;

namespace Models
{
    public class EventViewModel:NotifyPropertyChangedBase
    {
        #region commands
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand MainVocalSearch { get; set; }
        public DelegateCommand AddEventCommand { get; set; }
        public DelegateCommand EditEventCommand { get; set; }
        public DelegateCommand DeleteEventCommand { get; set; }
        public DelegateCommand WaitingLineCommand { get; set; }
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
                if ((object) this!=null) { UpdateListeEvent(); }
                NotifyPropertyChanged("ListeEventToShow");
                NotifyPropertyChanged("Event");
                AddEventCommand.RaiseCanExecuteChanged();
                EditEventCommand.RaiseCanExecuteChanged();
                DeleteEventCommand.RaiseCanExecuteChanged();
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
                if(ListeEventToShow.Count()!=0 && HasSearch) { Event = ListeEventToShow[0]; }
                NotifyPropertyChanged("ListeEvent");
                NotifyPropertyChanged("Event");
                AddEventCommand.RaiseCanExecuteChanged();
                EditEventCommand.RaiseCanExecuteChanged();
                DeleteEventCommand.RaiseCanExecuteChanged();
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
                if (ListeSpectator.Count() != 0) { Spectator = ListeSpectator[0]; }
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
                if (Event == null) { return; }
                if (ListeSpectator.Count() != 0) { Spectator = ListeSpectator[0]; }
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

        public string Localisation
        {
            get
            {
                return _localisation;
            }
            set
            {
                _localisation = value;
                NotifyPropertyChanged("Localisation");
            }
        }
        private string _localisation;

        public bool HasSearch
        {
            get
            {
                return _hasSearch;
            }
            set
            {
                _hasSearch = value;
            }
        }
        private bool _hasSearch;

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
                Localisation = TimeZone.CurrentTimeZone.StandardName;
                _event = value;
                if ((object)ListeSpeaker == null) { ListeSpeaker=new ObservableCollection<Intervenant>(); }
                if ((object)ListeSpectator == null) { ListeSpectator=new ObservableCollection<Internaute>(); }
                if ((object)_event == null) { _event = _listeEventToShow[0]; }
                SearchWordSpeaker = "";
                SearchWordSpectator = "";
                UpdateListeSpeaker();
                UpdateListeSpectator();
                NotifyPropertyChanged("Event");
                NotifyPropertyChanged("ListeEventToShow");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                AddEventCommand.RaiseCanExecuteChanged();
                EditEventCommand.RaiseCanExecuteChanged();
                DeleteEventCommand.RaiseCanExecuteChanged();
                WaitingLineCommand.RaiseCanExecuteChanged();
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
                HasSearch = true;
                _searchWord = value;
                UpdateListeEvent();
                NotifyPropertyChanged("SearchWord");
                MainVocalSearch.RaiseCanExecuteChanged();
                AddEventCommand.RaiseCanExecuteChanged();
                EditEventCommand.RaiseCanExecuteChanged();
                DeleteEventCommand.RaiseCanExecuteChanged();
            }
        }
        private string _searchWord;

        public string SearchWordSpeaker
        {
            get
            {
                return _searchWordSpeaker;
            }
            set
            {
                _searchWordSpeaker = value;
                UpdateListeSpeaker();
                NotifyPropertyChanged("SearchWordSpeaker");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private string _searchWordSpeaker;

        public string SearchWordSpectator
        {
            get
            {
                return _searchWordSpectator;
            }
            set
            {
                _searchWordSpectator = value;
                UpdateListeSpectator();
                NotifyPropertyChanged("SearchWordSpectator");
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private string _searchWordSpectator;

        #endregion
        
        public EventViewModel()
        {
            Localisation = TimeZone.CurrentTimeZone.StandardName;
            HasSearch = false;
            SpeechRecognitionTool = new SpeechToTextTools();
            AddCommand = new DelegateCommand(OnAddCommand, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanExecuteEdit);
            DeleteCommand = new DelegateCommand(OnDeleteCommand, CanExecuteDelete);
            MainVocalSearch = new DelegateCommand(OnMainVocalSearch,CanExecuteMainVocalSearch);
            AddEventCommand = new DelegateCommand(OnAddEventCommand, CanExecuteAddEvent);
            EditEventCommand = new DelegateCommand(OnEditEventCommand, CanExecuteEditEvent);
            DeleteEventCommand = new DelegateCommand(OnDeleteEventCommand, CanExecuteDeleteEvent);
            WaitingLineCommand = new DelegateCommand(OnWaitingLine, CanExecuteWaitingLine);
            ListeEvent = EventDAO.GetAllEvent();
            ListeEventToShow = ListToObservableCollectionFactory.Convert(ListeEvent);
            ListeSpeaker = ListToObservableCollectionFactory.Convert(new List<Intervenant>());
            ListeSpectator = ListToObservableCollectionFactory.Convert(new List<Internaute>());
        }

        #region Methods
        private void UpdateListeEvent()
        {
            if (SearchWord == "" || SearchWord == null) { ListeEventToShow=ListToObservableCollectionFactory.Convert(ListeEvent); return; }
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

        private void UpdateListeSpeaker()
        {
            if(SearchWordSpeaker=="" || SearchWordSpeaker == null) { ListeSpeaker = ListToObservableCollectionFactory.Convert(Event.Intervenants); return; }
            ObservableCollection<Intervenant> tmp = new ObservableCollection<Intervenant>();
            Regex r = new Regex("^.*" + SearchWordSpeaker.ToLower() + ".*$");
            for(int i = 0; i < Event.Intervenants.Count(); i++)
            {
                if (r.IsMatch(Event.Intervenants[i].Nom.ToLower()))
                {
                    tmp.Add(Event.Intervenants[i]);
                }
            }
            ListeSpeaker = tmp;
            NotifyPropertyChanged("ListeSpeaker");
        }

        private void UpdateListeSpectator()
        {
            if(SearchWordSpectator=="" || SearchWordSpectator == null) { ListeSpectator = ListToObservableCollectionFactory.Convert(Event.Inscrits); return; }
            ObservableCollection<Internaute> tmp = new ObservableCollection<Internaute>();
            Regex r = new Regex("^.*" + SearchWordSpectator.ToLower() + ".*$");
            for(int i = 0; i < Event.Inscrits.Count(); i++)
            {
                if (r.IsMatch(Event.Inscrits[i].Nom.ToLower()))
                {
                    tmp.Add(Event.Inscrits[i]);
                }
            }
            ListeSpectator = tmp;
            NotifyPropertyChanged("ListeSpectator");
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
        private void OnAddEventCommand(object o)
        {
            try
            {
                AddEventViewModel m = new AddEventViewModel();
                AddEventView v = new AddEventView();
                v.DataContext = m;
                v.ShowDialog();
                if (!m.NormalEnd)
                {
                    return;
                }
                if(m.Name=="" || m.Name == null) { throw (new ArgumentException("Vous n'avez pas rentrer de nom. Erreur:\n")); }
                if(m.Place=="" || m.Place == null) { throw (new ArgumentException("Vous n'avez pas rentrer de lieu. Erreur:\n")); }
                if(m.Date=="" || m.Date == null) { throw (new ArgumentException("Vous n'avez pas rentrer de date. Erreur:\n")); }
                Evenement tmp = new Evenement(m.Name, new MaDate(m.Date),m.NbPlace, new Lieu(m.Place));
                ListeEvent.Add(tmp);
                UpdateListeEvent();
                NotifyPropertyChanged("ListeEventToShow");
            }
            catch(Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        private void OnEditEventCommand(object o)
        {
            try
            {
                AddEventViewModel m = new AddEventViewModel();
                m.Name = Event.Nom;
                m.NbPlace = Event.NbPlaces;
                m.Place = Event.Lieu.Nom;
                m.Date = MaDate.DateToString(Event.Date);
                m.AddOrEdit = "Edit";
                AddEventView v = new AddEventView();
                v.DataContext = m;
                v.ShowDialog();
                if (!m.NormalEnd)
                {
                    return;
                }
                if (m.Name == "" || m.Name == null) { throw (new ArgumentException("Vous n'avez pas rentrer de nom. Erreur:\n")); }
                if (m.Place == "" || m.Place == null) { throw (new ArgumentException("Vous n'avez pas rentrer de lieu. Erreur:\n")); }
                if (m.Date == "" || m.Date == null) { throw (new ArgumentException("Vous n'avez pas rentrer de date. Erreur:\n")); }
                Evenement tmp = new Evenement(m.Name, new MaDate(m.Date), m.NbPlace, new Lieu(m.Place));
                ListeEvent[ListeEvent.FindIndex(ev => ev.Nom == Event.Nom && ev.Date==Event.Date && ev.Lieu.Nom==Event.Lieu.Nom && ev.NbPlaces==Event.NbPlaces)].Nom = m.Name;
                ListeEvent[ListeEvent.FindIndex(ev => ev.Nom == Event.Nom && ev.Date == Event.Date && ev.Lieu.Nom == Event.Lieu.Nom && ev.NbPlaces == Event.NbPlaces)].Lieu = new Lieu(m.Place);
                ListeEvent[ListeEvent.FindIndex(ev => ev.Nom == Event.Nom && ev.Date == Event.Date && ev.Lieu.Nom == Event.Lieu.Nom && ev.NbPlaces == Event.NbPlaces)].NbPlaces = m.NbPlace;
                ListeEvent[ListeEvent.FindIndex(ev => ev.Nom == Event.Nom && ev.Date == Event.Date && ev.Lieu.Nom == Event.Lieu.Nom && ev.NbPlaces == Event.NbPlaces)].Date = new MaDate(m.Date);
                UpdateListeEvent();
                NotifyPropertyChanged("ListeEventToShow");
            }
            catch (Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        private void OnDeleteEventCommand(object o)
        {
            try
            {
                if (Event == null) { throw (new Exception("Il n y a pas d'évènements à supprimer. Erreur:\n")); }
                ListeEvent.Remove(Event);
                ListeEventToShow = ListToObservableCollectionFactory.Convert(ListeEvent);
                Event = ListeEventToShow[0];
                NotifyPropertyChanged("ListeEventToShow");
                NotifyPropertyChanged("Event");
            }
            catch(Exception E)
            {
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
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
                    UpdateListeSpeaker();
                }
                else
                {
                    List<Internaute> tmp = Event.Inscrits;
                    tmp.Add(new Internaute(modele.Nom));
                    Event.Inscrits = tmp;
                    UpdateListeSpectator();
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
                    UpdateListeSpeaker();
                }
                else
                {
                    if (Spectator == null) { throw (new ArgumentException("Vous n'avez pas sélectionner de spectateurs. Erreur:\n")); }
                    if (Spectator.Nom == modele.Nom) { throw (new ArgumentException("Vous n'avez pas modifier le spectateur. Erreur:\n")); }
                    ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == Spectator.Nom))].Nom = modele.Nom;
                    Spectator = ListeSpectator[ListeSpectator.ToList().FindIndex(Internaute => (Internaute.Nom == modele.Nom))];
                    UpdateListeSpectator();
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
                    Event.Intervenants.RemoveAt(Event.Intervenants.ToList().FindIndex(Intervenant => (Intervenant.Nom == Speaker.Nom)));
                    UpdateListeSpeaker();
                }
                else
                {
                    Event.Inscrits.RemoveAt(Event.Inscrits.ToList().FindIndex(Internaute => (Internaute.Nom == Spectator.Nom)));
                    UpdateListeSpectator();
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
                if (SpeechRecognitionTool.RecognizeText.Count() != 0) { SpeechRecognitionTool.RecognizeText.Clear(); }
                SpeechRecognitionTool.RecognizeText.Add("");
                SpeechRecognitionTool.start();
                MainVocalSearch.RaiseCanExecuteChanged();
                while (SpeechRecognitionTool.IsMicroUse) { }
                if(SpeechRecognitionTool.RecognizeText==null || ((SpeechRecognitionTool.RecognizeText.Count==0 || SpeechRecognitionTool.Error) && SpeechRecognitionTool.RecognizeText[0] == ""))
                {
                    throw (new Exception("Erreur de reconnaissance vocale:\n"));
                }
                SearchWord = FindGoodString(SpeechRecognitionTool.RecognizeText);
                SpeechRecognitionTool.MicClient.EndMicAndRecognition();
            }
            catch(Exception E)
            {
                SpeechRecognitionTool.MicClient.EndMicAndRecognition();
                ExceptionViewModel v = new ExceptionViewModel(E);
                ExceptionView ExceptionWindow = new ExceptionView();
                ExceptionWindow.DataContext = v;
                ExceptionWindow.ShowDialog();
            }
        }

        private void OnWaitingLine(object o)
        {
            try
            {
                WaitingLineViewModel m = new WaitingLineViewModel();
                WaitingLineView v = new WaitingLineView();
                m.MainList = ListToObservableCollectionFactory.Convert(Event.ListeAttente.ToList());
                v.ShowDialog();
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
        private bool CanExecuteWaitingLine(object o)
        {
            return Event != null;
        }

        private bool CanExecuteAddEvent(object o)
        {
            return ListeEvent != null;
        }

        private bool CanExecuteEditEvent(object o)
        {
            return Event != null && ListeEvent.Count() != 0;
        }

        private bool CanExecuteDeleteEvent(object o)
        {
            return Event != null && ListeEvent.Count() != 0;
        }

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
            //return !SpeechRecognitionTool.IsMicroUse;
            return false;
        }
        #endregion
    }
}
