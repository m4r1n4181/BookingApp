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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourGuideReviews.xaml
    /// </summary>
    public partial class TourGuideReviews : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public TourGuideReviews()
        {
            InitializeComponent();
            DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetTodayTours());
        }


        private void ViewTourReview_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _tourController.StartTour(SelectedTour.Id);
            ReviewDetails reviewDetails = new ReviewDetails(SelectedTour);
            reviewDetails.ShowDialog();


        }
    }
    
}
