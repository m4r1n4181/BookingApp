using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;

namespace BookingApp.View
{
    public partial class MyToursView : Window, INotifyPropertyChanged
    {
        private readonly TouristRepository _touristRepository;
        private readonly TouristEntryRepository _entryRepository;
        private readonly UserRepository _userRepository;
        private readonly TourReservationService _tourReservationService;
        private User _user;
        public TourStatusType Status { get; set; }
        public User User
        {
            get => _user;
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourReservation SelectedTour { get; set; }

        private ObservableCollection<TourReservation> _reservations;
        public ObservableCollection<TourReservation> Reservations
        {
            get => _reservations;
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyToursView()
        {
            InitializeComponent();
            _touristRepository = new TouristRepository();
            _entryRepository = new TouristEntryRepository();
            _userRepository = new UserRepository();
            _tourReservationService = new TourReservationService();
            this.DataContext = this;

            if (User != null)
            {
                MessageBox.Show($"User ID: {User.Id}");
            }
        }

        public MyToursView(User user) : this()
        {
            User = user;
            Reservations = new ObservableCollection<TourReservation>(_tourReservationService.GetReservationsByUserId(User.Id));
        }

        // Handler za prikaz prethodnih rezervacija
        private void PreviousTours_Click(object sender, RoutedEventArgs e)
        {
            Reservations.Clear();
            var previousReservations = _tourReservationService.GetReservationsByUserId(User.Id)
                                                 .Where(r => _tourReservationService.GetTourStatus(r) == TourStatusType.finished);
            foreach (var reservation in previousReservations)
            {
                Reservations.Add(reservation);
            }

            // Dodajte ovde kod za proveru broja rezervacija i ispis poruke ako nema rezervacija
            if (Reservations.Count == 0)
            {
                MessageBox.Show("No previous tours found for the user.");
            }
        }


        private void ActiveTours_Click(object sender, RoutedEventArgs e)
        {
            Reservations.Clear();
            var activeReservations = _tourReservationService.GetReservationsByUserId(User.Id)
                                              .Where(r => _tourReservationService.GetTourStatus(r) == TourStatusType.started);
            foreach (var reservation in activeReservations)
            {
                Reservations.Add(reservation);
            }

            // Dodajte ovde kod za proveru broja rezervacija i ispis poruke ako nema rezervacija
            if (Reservations.Count == 0)
            {
                MessageBox.Show("No active tours found for the user.");
            }
        }






        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            // Provera da li je selektovana neka tura
            if (SelectedTour != null)
            {
                // Kreiranje i otvaranje prozora za ocenjivanje ture
                TourReviewForm tourReviewForm = new TourReviewForm(SelectedTour);
                tourReviewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a reservation to rate the tour.");
            }
        }

        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            // Provera da li je selektovana neka tura
            if (SelectedTour != null)
            {
                // Kreiranje i otvaranje prozora za ocenjivanje ture
                TourReviewForm tourReviewForm = new TourReviewForm(SelectedTour);
                tourReviewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a reservation to rate the tour.");
            }
        }

    }
}
