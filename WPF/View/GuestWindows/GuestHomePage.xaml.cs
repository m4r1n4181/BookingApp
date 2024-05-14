using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.View.GuestWindows;
using BookingApp.WPF.View.GuestWindows;
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

namespace BookingApp.WPF.Views.GuestWindows
{
    /// <summary>
    /// Interaction logic for GuestHomePage.xaml
    /// </summary>
    public partial class GuestHomePage : Window
    {
        public AccommodationReservationService _accommodationReservationService;
        public GuestHomePage()
        {
            InitializeComponent();
            _accommodationReservationService = new AccommodationReservationService(
                Injector.CreateInstance<IAccommodationReservationRepository>(),
                Injector.CreateInstance<IAccommodationRepository>(),
                Injector.CreateInstance<INotificationRepository>(),
                Injector.CreateInstance<IOwnerReviewRepository>(),
                Injector.CreateInstance<ISuperGuestRepository>());
            this.DataContext = this;
        }

        private void GuestReviews_Button_Click(object sender, RoutedEventArgs e)
        {
            AllGuestReviews allGuestReviews = new AllGuestReviews();
            allGuestReviews.Show();
        }

        private void Accommodation_Search_Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearch accommodationSearch = new AccommodationSearch();
            accommodationSearch.Show();
        }

        private void Guests_Requests_Button_Click(object sender, RoutedEventArgs e)
        {
            GuestRequests guestsRequests = new GuestRequests();
            guestsRequests.Show();
        }

        private void Owner_Review_Button_Click(object sender, RoutedEventArgs e)
        {
          AccommodationReservationToRateOwnerForm accommodationReservationToRateForm = new AccommodationReservationToRateOwnerForm();
            accommodationReservationToRateForm.Show();
        }

        private void Guests_Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAllReservations accommodationAllReservations = new AccommodationAllReservations();
            accommodationAllReservations.Show();    
        }

        private void Guests_Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            GuestProfile guestProfile = new GuestProfile();
            guestProfile.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _accommodationReservationService.CheckSuperGuestPeriod(SignInForm.LoggedUser.Id);
        }
    }
}
