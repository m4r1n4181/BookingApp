using System.Windows;
using System.Windows.Controls;
using BookingApp.Model; // Assuming your Accommodation model is located here
using System.Collections.ObjectModel;
using BookingApp.Controller;
using BookingApp.View;

namespace BookingApp.WPF.View.OwnerPages
{
    public partial class AllAccommodationsPage : Page
    {
        // Assuming AccommodationController can fetch accommodations
        private AccommodationController _accommodationController;

        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public AllAccommodationsPage()
        {
            InitializeComponent();
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id)); // Assuming you have a method to fetch accommodations by owner
            this.DataContext = this; // Set the DataContext to the current instance for binding
        }

        private void ViewAccommodationButton_Click(object sender, RoutedEventArgs e)
        {
            // Sender is the button that was clicked
            var button = sender as Button;
            if (button != null)
            {
                // Retrieve the DataContext from the button, which should be an Accommodation object
                Accommodation selectedAccommodation = button.DataContext as Accommodation;
                if (selectedAccommodation != null)
                {
                    // Navigate to the AccommodationViewPage, passing the selected accommodation
                    AccommodationViewPage accommodationViewPage = new AccommodationViewPage(selectedAccommodation);
                    NavigationService.Navigate(accommodationViewPage);
                }
                else
                {
                    MessageBox.Show("No accommodation selected.");
                }
            }
        }
    }
}
