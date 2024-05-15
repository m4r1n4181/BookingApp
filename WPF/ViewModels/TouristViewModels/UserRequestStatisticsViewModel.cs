using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class UserRequestStatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<int> UniqueYears { get; set; }
        public ObservableCollection<Location> UniqueLocations { get; set; }
        public ObservableCollection<string> UniqueLanguages { get; set; }
        public Location SelectedLocation { get; set; }
        public string SelectedLanguage { get; set; }
        public NavigationService Navigation { get; set; }
        public TourRequestController _tourRequestController { get; set; }

        private TourRequestPercentageDto _tourRequestPercentage;
        public TourRequestPercentageDto TourRequestPercentageDto
        {
            get => _tourRequestPercentage;
            set
            {
                if (value != _tourRequestPercentage)
                {
                    _tourRequestPercentage = value;
                    OnPropertyChanged("TourRequestPercentageDto");
                }
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged("SelectedYear");
                    CalculateStatistics();
                }
            }
        }
        private int _selectedLocationRequestCount;
        public int SelectedLocationRequestCount
        {
            get => _selectedLocationRequestCount;
            set
            {
                if (value != _selectedLocationRequestCount)
                {
                    _selectedLocationRequestCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _selectedLanguageRequestCount;
        public int SelectedLanguageRequestCount
        {
            get => _selectedLanguageRequestCount;
            set
            {
                if (value != _selectedLanguageRequestCount)
                {
                    _selectedLanguageRequestCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _totalRequests;
        public int TotalRequests
        {
            get => _selectedLanguageRequestCount;
            set
            {
                if (value != _totalRequests)
                {
                    _totalRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand InGeneralCommand { get; }
        public RelayCommand ViewCommand { get; }
        public RelayCommand LocationComboBox_SelectionChangedCommand { get; }
        public RelayCommand LanguageComboBox_SelectionChangedCommand { get; }

        public UserRequestStatisticsViewModel(NavigationService navigation)
        {
            Navigation = navigation;
            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetAllWithLocations());

            UniqueLocations = new ObservableCollection<Location>(_tourRequestController.GetUniqueLocationsFromTourRequests());
            UniqueLanguages = new ObservableCollection<string>(_tourRequestController.GetUniqueLanguagesFromTourRequests());
            UniqueYears = new ObservableCollection<int>(_tourRequestController.YearsOfTourRequests(SignInForm.LoggedUser.Id));

            CalculateStatistics();

            ViewCommand = new RelayCommand(Executed_ViewCommand);
            InGeneralCommand = new RelayCommand(Executed_InGeneralCommand);
            LocationComboBox_SelectionChangedCommand = new RelayCommand(LocationComboBox_SelectionChanged);
            LanguageComboBox_SelectionChangedCommand = new RelayCommand(LanguageComboBox_SelectionChanged);
        }

        private void CalculateStatistics()
        {
            TotalRequests = _tourRequestController.CountRequestForTourist(SignInForm.LoggedUser.Id);
            if (_tourRequestController != null)
            {
                if (SelectedYear != 0)
                {
                    TourRequestPercentageDto = _tourRequestController.GetPercentageOfTourRequestForYear(SignInForm.LoggedUser.Id, SelectedYear);
                }

                if (SelectedLocation != null && !string.IsNullOrEmpty(SelectedLanguage))
                {
                    SelectedLocationRequestCount = _tourRequestController.CountRequestsByLocationForTourist(SelectedLocation, SignInForm.LoggedUser.Id);
                    SelectedLanguageRequestCount = _tourRequestController.CountRequestsByLanguageForTourist(SelectedLanguage, SignInForm.LoggedUser.Id);
                }
                else if (SelectedLocation != null)
                {
                    SelectedLocationRequestCount = _tourRequestController.CountRequestsByLocationForTourist(SelectedLocation, SignInForm.LoggedUser.Id);
                    SelectedLanguageRequestCount = 0;
                }
                else if (!string.IsNullOrEmpty(SelectedLanguage))
                {
                    SelectedLanguageRequestCount = _tourRequestController.CountRequestsByLanguageForTourist(SelectedLanguage, SignInForm.LoggedUser.Id);
                    SelectedLocationRequestCount = 0;
                }
                else
                {
                    SelectedLocationRequestCount = 0;
                    SelectedLanguageRequestCount = 0;
                }
            }
        }

        public void Executed_ViewCommand(object obj)
        {
            TourRequestPercentageDto = _tourRequestController.GetPercentageOfTourRequestForYear(SignInForm.LoggedUser.Id, SelectedYear);
        }

        public void Executed_InGeneralCommand(object obj)
        {
            TourRequestPercentageDto = _tourRequestController.GetPercentageOfTourRequest(SignInForm.LoggedUser.Id);
        }

        public void LanguageComboBox_SelectionChanged(object param)
        {
            CalculateStatistics();
        }

        public void LocationComboBox_SelectionChanged(object param)
        {
            CalculateStatistics();
        }


    }
}
