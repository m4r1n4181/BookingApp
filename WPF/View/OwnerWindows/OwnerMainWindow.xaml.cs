using BookingApp.Controller;
using BookingApp.WPF.View.OwnerWindows;
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

namespace BookingApp.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        private AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public OwnerMainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();

            if (!_accommodationOwnerReviewController.IsSuperOwner(SignInForm.LoggedUser.Id))
            {
                SuperOwnerLabel.Visibility = Visibility.Hidden;
            }
        }

        private void MyAccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsOverviewWindow accommodations = new AccommodationsOverviewWindow();
            accommodations.Show();

        }

        private void GuestsReviewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReservationRescheduleRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequestsWindow reservationRescheduleRequestsWindow = new ReservationRescheduleRequestsWindow();
            reservationRescheduleRequestsWindow.Show();

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}
