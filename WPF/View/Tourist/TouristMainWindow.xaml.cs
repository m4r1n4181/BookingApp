using BookingApp.Model;
using BookingApp.WPF.ViewModels.Tourist;
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
using BookingApp.WPF.View.Tourist;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Navigation;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        public TouristMainWindowViewModel ViewModel { get; set; }
        public User User { get; set; }
        public TouristMainWindow(User user)
        {
            InitializeComponent();
            ViewModel = new TouristMainWindowViewModel(this.TouristContentFrame.NavigationService);
            User = user;
            TourOverviewForm tourOverviewForm = new TourOverviewForm(TouristContentFrame.NavigationService);
            TouristContentFrame.Navigate(tourOverviewForm);

            this.DataContext = tourOverviewForm;


        }

    }
}

