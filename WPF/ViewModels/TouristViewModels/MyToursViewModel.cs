using BookingApp.Controller;
using BookingApp.Model.Enums;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace BookingApp.View.ViewModel.TouristViewModels
{
    public enum MyToursViewState
    {
        WatchingAllTours, WatcingActiveTours, WatchingPreviousTours
    }
    public class MyToursViewModel : INotifyPropertyChanged
    {
        private readonly TouristRepository _touristRepository;
        private readonly TouristEntryRepository _entryRepository;
        private readonly UserRepository _userRepository;
        private readonly TourReservationService _tourReservationService;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly NotificationController _notificationController;
       // private User _user;
        private MyToursViewState _windowState;

        public ObservableCollection<Notification> Notifications { get; set; }
        public TourStatusType Status { get; set; }
        

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


        public ICommand PreviousToursCommand { get; private set; }
        public ICommand ActiveToursCommand { get; private set; }
        public ICommand RateTourCommand { get; private set; }
        public ICommand TrackTourCommand { get; private set; }
       // public ICommand NotificationButtonCommand { get; private set; }
      
        public MyToursViewModel()
        {
            _touristRepository = new TouristRepository();
            _entryRepository = new TouristEntryRepository();
            _userRepository = new UserRepository();
            _tourReservationService = new TourReservationService();
            _tourReservationRepository = new TourReservationRepository();
            _notificationController = new NotificationController();

            Reservations = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByUser(SignInForm.LoggedUser.Id));

            PreviousToursCommand = new RelayCommand(PreviousTours_Click);
            ActiveToursCommand = new RelayCommand(ActiveTours_Click);
            RateTourCommand = new RelayCommand(RateTour_Click);
            TrackTourCommand = new RelayCommand(TrackTour_Click);
           // NotificationButtonCommand = new GalaSoft.MvvmLight.Command.RelayCommand(NotificationButton_Click);

            /*if (User != null)
            {
                MessageBox.Show($"User ID: {User.Id}");
            }*/
            _windowState = MyToursViewState.WatchingAllTours;
            Notifications = new ObservableCollection<Notification>();
        }


        // Handler za prikaz prethodnih rezervacija
        private void PreviousTours_Click()
        {
            _windowState = MyToursViewState.WatchingPreviousTours;
            Reservations.Clear();
            var previousReservations = _tourReservationRepository.GetByUser(SignInForm.LoggedUser.Id)
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


        private void ActiveTours_Click()
        {
            _windowState = MyToursViewState.WatcingActiveTours;

            Reservations.Clear();
            var activeReservations = _tourReservationRepository.GetByUser(SignInForm.LoggedUser.Id)
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



        private void RateTour_Click()
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

        private void TrackTour_Click()
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

       /* private void NotificationButton_Click()
        {
            List<Notification> notifications = _notificationController.GetByUserId(SignInForm.LoggedUser.Id);
            if (notifications.Count == 0)
            {
                MessageBox.Show("There are no new notifications.", "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Notifications.Clear();
            notifications.ForEach(n => Notifications.Add(n));

            _notificationController.ReadAllUserNotifications(SignInForm.LoggedUser.Id);
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}