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
using BookingApp.WPF.ViewModels;
using BookingApp.WPF.View.Tourist;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using BookingApp.WPF.ViewModels;
using BookingApp.WPF.View;
using BookingApp.ViewModels;
using BookingApp.View;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public enum MyToursViewState
    {
        WatchingAllTours, WatcingActiveTours, WatchingPreviousTours
    }
    public class MyToursViewModel : ViewModelBase
    {
        private readonly TouristRepository _touristRepository;
        private readonly TouristEntryRepository _entryRepository;
        private readonly UserRepository _userRepository;
        private readonly TourReservationService _tourReservationService;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly NotificationController _notificationController;
        private User _user;
        private MyToursViewState _windowState;
        public NavigationService NavigationService { get; set; }
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


        public RelayCommand PreviousToursCommand { get; private set; }
        public RelayCommand ActiveToursCommand { get; private set; }
        public RelayCommand RateTourCommand { get; private set; }
        public RelayCommand TrackTourCommand { get; private set; }
      //  public RelayCommand NotificationButtonCommand { get; private set; }
        public MyToursViewModel(NavigationService navigation)
        {
            _touristRepository = new TouristRepository();
            _entryRepository = new TouristEntryRepository();
            _userRepository = new UserRepository();
            _tourReservationService = new TourReservationService();
            _tourReservationRepository = new TourReservationRepository();
            _notificationController = new NotificationController();
            NavigationService = navigation;
            User = SignInForm.LoggedUser;
            Reservations = new ObservableCollection<TourReservation>(_tourReservationService.GetByUserId(User.Id));

            PreviousToursCommand = new RelayCommand(PreviousTour);
            ActiveToursCommand = new RelayCommand(ActiveTours_Click);
            RateTourCommand = new RelayCommand(RateTour_Click);
            TrackTourCommand = new RelayCommand(TrackTour_Click);
            //NotificationButtonCommand = new RelayCommand(NotificationButton_Click);

            if (User != null)
            {
                MessageBox.Show($"User ID: {User.Id}");
            }
            _windowState = MyToursViewState.WatchingAllTours;
           // Notifications = new ObservableCollection<Notification>();
        }

        private void PreviousTour(object obj)
        {
            _windowState = MyToursViewState.WatchingPreviousTours;
            Reservations.Clear();
            var previousReservations = _tourReservationService.GetByUserId(User.Id)
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


        private void ActiveTours_Click(object obj)
        {
            _windowState = MyToursViewState.WatcingActiveTours;
            
                Reservations.Clear();
            
            var activeReservations = _tourReservationService.GetByUserId(User.Id)
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



        private void RateTour_Click(object obj)
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
            TourReviewFormViewModel tourReviewForm = new TourReviewFormViewModel(SelectedTour);
            NavigationService.Navigate(tourReviewForm);

        }

        private void TrackTour_Click(object obj)
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
            TourAttendanceView tourAttendanceView = new TourAttendanceView(SelectedTour, NavigationService);
            NavigationService.Navigate(tourAttendanceView);
        }

        /*private void NotificationButton_Click(object obj)
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
        */
        public bool CanExecuteViewClick(object param)
        {
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}