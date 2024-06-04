using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.ViewModels.TourGuideViewModels;
using BookingApp.ViewModels;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for CreateTourFromRequestStats.xaml
    /// </summary>
    public partial class CreateTourFromRequestStats : Window
    {
      
        public CreateTourFromRequestStats()
        {
            InitializeComponent();
            this.DataContext = new CreateTourFromRequestStatsViewModel();
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
