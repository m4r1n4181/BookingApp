using System;
using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;


namespace BookingApp.View
{
    public partial class LiveTourView : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private readonly TourRepository _tourRepository;
        private readonly TourService _tourService;

        public LiveTourView()
        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _tourService = new TourService();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetTodayTours());
        }

        private void ActivateTour(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to activate this tour?", "Activate Tour",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _tourService.StartTour(SelectedTour.Id);


                    TourDetails tourDetails = new TourDetails();
                    tourDetails.Show();
                }
            }
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour == null)
            {
                //messageboc
                return;
            }
            TourDetails tourDetails = new TourDetails(SelectedTour);
            tourDetails.ShowDialog();


        }
    }
}
