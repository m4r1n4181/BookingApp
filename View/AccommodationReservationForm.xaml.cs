using Booking.App;
using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AccommodationReservationForm.xaml
    /// </summary>
    public partial class AccommodationReservationForm : Window
    {
        private AccommodationController _accommodationController;
        private AccommodationReservationController _accommodationReservationController;


        public Accommodation SelectedAccommodation { get; set; }

        public AccommodationReservation accommodationReservation { get; set; }

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }

        private DateTime _arrival;
        public DateTime Arrival
        {
            get => _arrival;
            set
            {
                if (value != _arrival)
                {
                    _arrival = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _departure;
        public DateTime Departure
        {
            get => _departure;
            set
            {
                if (value != _departure)
                {
                    _departure = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _numberOfDays;
        public int NumberOfDays
        {
            get => _numberOfDays;
            set
            {
                if (value != _numberOfDays)
                {
                    _numberOfDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationReservationForm(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _accommodationReservationController = new AccommodationReservationController();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>();

            SelectedAccommodation = accommodation;

            Arrival = DateTime.Now;
            Departure = DateTime.Now;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation()
            {
                Accommodation = SelectedAccommodation,
                Guest = null,//SignInForm.LoggedUser,
                Arrival = Arrival,
                Departure = Departure,
            };
            _accommodationReservationController.Create(accommodationReservation);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation();
            accommodationReservation.Arrival = Arrival;
            accommodationReservation.Departure = Departure;

            AccommodationReservations.Clear();
            foreach (AccommodationReservation reservation in _accommodationReservationController.GetFreeRangeDays(SelectedAccommodation.Id, Arrival, Departure, NumberOfDays))
            {
                AccommodationReservations.Add(reservation);
            }
        }
    }
}
