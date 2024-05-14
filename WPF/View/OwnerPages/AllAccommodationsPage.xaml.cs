using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using System.Collections.ObjectModel;
using BookingApp.Controller;
using BookingApp.View;
using BookingApp.WPF.ViewModels.OwnerPageViewModels;

namespace BookingApp.WPF.View.OwnerPages
{
    public partial class AllAccommodationsPage : Page
    {
        // private AccommodationController _accommodationController;

        //public ObservableCollection<Accommodation> Accommodations { get; set; }

        public AllAccommodationsPage()
        {
            InitializeComponent();
            // _accommodationController = new AccommodationController();
            //Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
            DataContext = new AllAccommodationsViewModel();
        }
        
        private void ViewAccommodationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Accommodation selectedAccommodation = button.DataContext as Accommodation;
                if (selectedAccommodation != null)
                {
                    AccommodationViewPage accommodationViewPage = new AccommodationViewPage(selectedAccommodation);
                    NavigationService.Navigate(accommodationViewPage);
                }
                else
                {
                    MessageBox.Show("No accommodation selected.");
                }
            }
        }
        /*
        private void DeleteAccommdationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Accommodation selectedAccommodation = button.DataContext as Accommodation;
                if (selectedAccommodation != null)
                {
                    _accommodationController.Delete(selectedAccommodation);
                    MessageBox.Show("Accommodation successfully deleted.");
                }
                else
                {
                    MessageBox.Show("No accommodation selected.");
                }
            }

    }*/
}
}
