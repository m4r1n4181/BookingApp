using BookingApp.Controller;
using BookingApp.Model;
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
using System.Collections.ObjectModel;
using BookingApp.View.ViewModels.TourGuideViewModels;
using BookingApp.WPF.View.TourGuide;
using BookingApp.WPF.View.TourGuideWindows;


namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AllTours.xaml
    /// </summary>
    public partial class AllTours : Window
    {
       
        public AllTours()
        {
            InitializeComponent();
            this.DataContext = new AllToursViewModel();

        }

        private void LiveTour_Click(object sender, RoutedEventArgs e)
        {
            LiveTourView liveTourView = new LiveTourView();
            liveTourView.Show();
        }

        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {

            TourStatisticsOverview tourStatisticsOverview = new TourStatisticsOverview();
            tourStatisticsOverview.Show();
        }

        private void TourGuideReviews_Click(object sender, RoutedEventArgs e)
        {

            TourGuideReviews tourGuideReviews = new TourGuideReviews();
            tourGuideReviews.Show();
        }
        private void AllTours_Click(object sender, RoutedEventArgs e)
        {

            AllTours allTours = new AllTours();
            allTours.Show();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SearchTourRequests searchTourRequests = new SearchTourRequests();
            searchTourRequests.Show();

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            //za sad me samo baca na signin, posle neka logika 
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            //otvara prozor za profil tourGuidea
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            //otvara prozor za tutorial 
        }

    }
}
