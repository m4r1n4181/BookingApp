using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;

namespace BookingApp.View
{
    public enum MyToursViewState
    {
        WatchingAllTours, WatcingActiveTours, WatchingPreviousTours
    }
    public partial class MyToursView : Window, INotifyPropertyChanged
    {
        private readonly TouristRepository _touristRepository;
        private readonly TouristEntryRepository _entryRepository;
        private readonly UserRepository _userRepository;
        private readonly TourReservationService _tourReservationService;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly NotificationController _notificationController;
        private User _user;
        private MyToursViewState _windowState;

        public ObservableCollection<Notification> Notifications { get; set; }
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
            _tourReservationRepository = new TourReservationRepository();
            _notificationController = new NotificationController();
            this.DataContext = this;

            if (User != null)
            {
                MessageBox.Show($"User ID: {User.Id}");
            }
            _windowState = MyToursViewState.WatchingAllTours;
            Notifications = new ObservableCollection<Notification>();
        }

        public MyToursView(User user) : this()
        {
            User = user;
            Reservations = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByUser(User.Id));
        }

        // Handler za prikaz prethodnih rezervacija
        private void PreviousTours_Click(object sender, RoutedEventArgs e)
        {
            _windowState = MyToursViewState.WatchingPreviousTours;
            Reservations.Clear();
            var previousReservations = _tourReservationRepository.GetByUser(User.Id)
                                                 .Where(r => _tourReservationRepository.GetTourStatus(r) == TourStatusType.finished);
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
            _windowState = MyToursViewState.WatcingActiveTours;

            Reservations.Clear();
            var activeReservations = _tourReservationRepository.GetByUser(User.Id)
                                              .Where(r => _tourReservationRepository.GetTourStatus(r) == TourStatusType.started);
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
            if (_windowState == MyToursViewState.WatcingActiveTours)
            {
                MessageBox.Show("Cant rate active tours.");
                return;
            }
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a reservation to rate the tour.");
                return;
            }

            // Kreiranje i otvaranje prozora za ocenjivanje ture
            TourReviewForm tourReviewForm = new TourReviewForm(SelectedTour);
            tourReviewForm.ShowDialog();

        }

        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            if (_windowState == MyToursViewState.WatchingPreviousTours)
            {
                MessageBox.Show("Cant track previous tours.");
                return;
            }
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a reservation to rate the tour.");
            }
            TourAttendanceView tourAttendanceView = new TourAttendanceView(SelectedTour);
            tourAttendanceView.ShowDialog();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            List<Notification> notifications = _notificationController.GetByUserId(User.Id);
            if (notifications.Count == 0)
            {
                MessageBox.Show("There are no new notifications.", "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Notifications.Clear();
            notifications.ForEach(n => Notifications.Add(n));

            _notificationController.ReadAllUserNotifications(User.Id);
        }
    }
}
