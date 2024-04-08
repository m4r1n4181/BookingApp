using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationOverviewWindow.xaml
    /// </summary>
    public partial class AccommodationOverviewWindow : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationController _accommodationController;

        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationOverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationController = new AccommodationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                return;
            }
            AccommodationReviewsWindow AccommodationReviews = new AccommodationReviewsWindow(SelectedAccommodation);
            AccommodationReviews.Show();
        }

    }
}
