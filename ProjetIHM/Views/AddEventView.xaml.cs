﻿using System;
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

namespace Views
{
    /// <summary>
    /// Logique d'interaction pour AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Window
    {
        public AddEventView()
        {
            InitializeComponent();
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}