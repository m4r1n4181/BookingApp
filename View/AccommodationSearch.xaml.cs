using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model.Enums;
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
    /// Interaction logic for AccommodationSearch.xaml
    /// </summary>
    public partial class AccommodationSearch : Window
    {
        private AccommodationController _accommodationController;
        public static ObservableCollection<Accommodation> Accommodations { get; set; }


        public Accommodation SelectedAccommodation { get; set; }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationType _accommodationType;
        public AccommodationType Type
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }



        private int _minReservationDays;
        public int MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public AccommodationSearch()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in _accommodationController.GetAll())
            {
                Accommodations.Add(accommodation);
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchParams searchParams = new AccommodationSearchParams();
            searchParams.Name = Name;
            searchParams.City = City;
            searchParams.Country = Country;
            searchParams.Type = Type;
            searchParams.MaxGests = MaxGuests;
            searchParams.MinReservationDays = MinReservationDays;
            Accommodations.Clear();
            foreach (Accommodation accommodation in _accommodationController.SearchAccommodations(searchParams))
            {
                Accommodations.Add(accommodation);
            }
        }
    }
}
