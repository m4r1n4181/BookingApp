using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Model;
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

        public Tour SelectedTour { get; set; }

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
            DataContext = this;

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
            var previousReservations = _tourReservationService.GetPreviousReservationsByUserId(User.Id);
            foreach (var reservation in previousReservations)
            {
                Reservations.Add(reservation);
            }
        }

        private void ActiveTours_Click(object sender, RoutedEventArgs e)
        {
            Reservations.Clear();
            var activeReservations = _tourReservationService.GetActiveReservationsByUserId(User.Id);
            foreach (var reservation in activeReservations)
            {
                Reservations.Add(reservation);
            }
        }

    }
}
