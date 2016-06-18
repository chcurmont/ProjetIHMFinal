using DAO;
using Models;
using System.Windows;

namespace ProjetIHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EventViewModel main;
        public MainWindow()
        {
            main = new EventViewModel();
            InitializeComponent();
            DataContext = main;
        }

        private void Fermeture(object sender, System.EventArgs e)
        {
            EventDAO.SetAllEvent(main.ListeEvent);
        }
    }
}
