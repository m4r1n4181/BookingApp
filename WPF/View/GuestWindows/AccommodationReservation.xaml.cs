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

        private int _numberOfGuests;
        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (value != _numberOfGuests)
                {
                    _numberOfGuests = value;
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
            AccommodationReservation accommodationReservation = new AccommodationReservation(
                SelectedAccommodation,
                SignInForm.LoggedUser,
                SelectedReservation.Arrival,
                SelectedReservation.Departure
            );
            _accommodationReservationController.Create(accommodationReservation,SignInForm.LoggedUser);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation();
            accommodationReservation.Arrival = Arrival;
            accommodationReservation.Departure = Departure;
            List<AccommodationReservation> reservations;
            AccommodationReservations.Clear();

            if(NumberOfGuests > SelectedAccommodation.MaxGuests)
            {
                MessageBox.Show("This number of guests is not allowed.");
                return;
            }

            //dodati i u service
            DateTime startTime = Arrival;
            DateTime endTime = Departure; //pocetna vremena 
            do
            {
                reservations = _accommodationReservationController.GetFreeRangeDays(SelectedAccommodation.Id, startTime, endTime, NumberOfDays);
                startTime = endTime;
                endTime = endTime.AddDays((Departure - Arrival).Days);
                
            } while (reservations.Count==0);



            foreach (AccommodationReservation reservation in  reservations)
            {
                AccommodationReservations.Add(reservation);
            }

        }
    }
}
