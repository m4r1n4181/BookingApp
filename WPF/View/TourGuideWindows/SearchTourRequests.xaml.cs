using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.View;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using System;
using System.Collections;
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

namespace BookingApp.WPF.View.TourGuideWindows
{
    /// <summary>
    /// Interaction logic for SearchTourRequests.xaml
    /// </summary>
    public partial class SearchTourRequests : Window
    {
       
        public SearchTourRequests()
        {
            InitializeComponent();
            this.DataContext = new SearchTourRequestsViewModel();
        
        }

        private void LiveTour_Click(object sender, RoutedEventArgs e)
        {
            LiveTourView liveTourView = new LiveTourView();
            liveTourView.Show();
        }

        private void AllFutureTours_Click(object sender, RoutedEventArgs e)
        {
            TourReservation tourReservation = new TourReservation();

            FutureTours futureTours = new FutureTours();
            futureTours.Show();
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
            GuideProfile guideProfile = new GuideProfile();
            guideProfile.Show();
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            //otvara prozor za tutorial 
        }


    }
}
