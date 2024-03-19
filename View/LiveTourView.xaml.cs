using System;
using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;


namespace BookingApp.View
{
    public partial class LiveTourView : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public LiveTourView()
        {
            InitializeComponent();
            DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetTodayTours());
        }


        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _tourController.StartTour(SelectedTour.Id);
            TourDetails tourDetails = new TourDetails(SelectedTour);
            tourDetails.ShowDialog();


        }
    }
}
