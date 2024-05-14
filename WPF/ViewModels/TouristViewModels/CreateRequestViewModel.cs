using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.View;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class CreateRequestViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<DateTime> DateTimes { get; set; }

        private LocationController _locationController;
        private TourRequestController _tourRequestController;
        public Location SelectedLocation { get; set; }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));

                    UpdateCountriesBasedOnCity();

                }
            }
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));

                    UpdateCitiesBasedOnCountry();
                }
            }
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged(nameof(Cities));
                }
            }
        }

        private ObservableCollection<string> _countries;
        public ObservableCollection<string> Countries
        {
            get => _countries;
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    OnPropertyChanged(nameof(Countries));

                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _maxTourists;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists)
                {
                    _maxTourists = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateCitiesBasedOnCountry()
        {
            Cities = new ObservableCollection<string>(
            Locations.Where(loc => loc.Country == SelectedCountry).Select(loc => loc.City).Distinct());
        }

        private void UpdateCountriesBasedOnCity()
        {
            Countries = new ObservableCollection<string>(
            Locations.Where(loc => loc.City == SelectedCity).Select(loc => loc.Country).Distinct());
        }
        public NavigationService NavigationService { get; set; }

        public RelayCommand CreateRequestCommand { get; private set; }

        public CreateRequestViewModel(NavigationService navigation) {
            _locationController = new LocationController();
            _tourRequestController = new TourRequestController();
            NavigationService = navigation;
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Cities = new ObservableCollection<string>(Locations.Select(loc => loc.City).Distinct());
            Countries = new ObservableCollection<string>(Locations.Select(loc => loc.Country).Distinct());
            DateTimes = new ObservableCollection<DateTime>();
            CreateRequestCommand = new RelayCommand(CreateRequest);

        }

        public void CreateRequest(object param)
        {

            Location selectedLocation = Locations.FirstOrDefault(loc => loc.City == SelectedCity && loc.Country == SelectedCountry);

            TourRequest newTourRequest = new TourRequest
            {
                Location = selectedLocation,
                Language = Language,
                MaxTourists = MaxTourists,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate
            };

           // _tourRequestController.CreateTourRequest(newTourRequest);


            //Close();
        }
    }
}
