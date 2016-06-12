using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetIHM
{
    /// <summary>
    /// Logique d'interaction pour AddView.xaml
    /// </summary>
    public partial class AddView : Window
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
