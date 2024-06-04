using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.ViewModels;
using BookingApp.WPF.View.Tourist;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using NavigationService = System.Windows.Navigation.NavigationService;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class CreateRequestViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<DateTime> DateTimes { get; set; }

        private LocationController _locationController;
        private TourRequestController _tourRequestController;
        private TourParticipantService _tourParticipantService;
        private TouristRepository _touristRepository;

        public ObservableCollection<TourParticipants> Participants { get; private set; }
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

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                if (value != _age)
                {
                    _age = value;
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

        public RelayCommand AddParticipantsCommand { get; private set; }
        public RelayCommand CreateRequestCommand {  get; private set; }
        public RelayCommand UseMyInfoCommand {  get; private set; }
        
        public CreateRequestViewModel(NavigationService navigation) {
            _locationController = new LocationController();
            _tourRequestController = new TourRequestController();
            _tourParticipantService = new TourParticipantService();
            _touristRepository = new TouristRepository();
            Participants = new ObservableCollection<TourParticipants>();
            NavigationService = navigation;
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Cities = new ObservableCollection<string>(Locations.Select(loc => loc.City).Distinct());
            Countries = new ObservableCollection<string>(Locations.Select(loc => loc.Country).Distinct());
            DateTimes = new ObservableCollection<DateTime>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            AddParticipantsCommand = new RelayCommand(AddParticipants);
            CreateRequestCommand = new RelayCommand(CreateRequest);
            UseMyInfoCommand = new RelayCommand(UseMyInfo);

        }

        public void AddParticipants(object param)
        {

            TourParticipants participant = new TourParticipants
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Convert.ToInt32(Age)
            };

            Participants.Add(participant);
            _tourParticipantService.CreateParticipant(participant);

        }

        private void UseMyInfo(object sender)
        {
            Model.Tourist tourist1 = _touristRepository.GetByUserId(SignInForm.LoggedUser.Id);
            TourParticipants tourist = new TourParticipants
            {
                FirstName = tourist1.FirstName,
                LastName = tourist1.LastName,
                Age = Convert.ToInt32(tourist1.Age),
                UserId = SignInForm.LoggedUser.Id,
            };

            Participants.Add(tourist);
            _tourParticipantService.CreateParticipant(tourist);
        }

        public void CreateRequest(object param)
        {

            Location selectedLocation = Locations.FirstOrDefault(loc => loc.City == SelectedCity && loc.Country == SelectedCountry);
            _tourRequestController.CreateTourRequest(selectedLocation, Language, MaxTourists, Description, StartDate, EndDate, Participants.ToList(), SignInForm.LoggedUser);
            MyRequestsView myRequests = new MyRequestsView(NavigationService);
            //NavigationService.Navigate(myRequests);

            //Close();
        }
    }
}
