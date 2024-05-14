using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.WPF.View.TourGuide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.ViewModels;

namespace BookingApp.WPF.ViewModels.TourGuideViewModels
{
    public class RequestStatisticsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TourRequest> TourRequests { get; set; }

        private TourRequestController _tourRequestController;
        public ObservableCollection<Location> UniqueLocations { get; set; }

        private LocationController _locationController;
        public Location SelectedLocation { get; set; }

        public string SelectedLanguage { get; set; }

        public ObservableCollection<string> UniqueLanguages { get; set; }
        public ObservableCollection<int> UniqueYears { get; set; }

        public int SelectedYear { get; set; }

        public RelayCommand MonthlyViewCommand { get; set; }
        public RelayCommand CreateFromStatsCommand { get; set; }

        public RelayCommand LocationComboBox_SelectionChangedCommand { get; }
        public RelayCommand LanguageComboBox_SelectionChangedCommand { get; }
        public RelayCommand YearComboBox_SelectionChangedCommand { get; }




        private string _language;
        public string Languages
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

        private int _selectedYearRequestCount;
        public int SelectedYearRequestCount
        {
            get => _selectedYearRequestCount;
            set
            {
                if (value != _selectedYearRequestCount)
                {
                    _selectedYearRequestCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public RequestStatisticsViewModel()
        {
            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetAllWithLocations());

            UniqueLocations = new ObservableCollection<Location>(_tourRequestController.GetUniqueLocationsFromTourRequests());
            UniqueLanguages = new ObservableCollection<string>(_tourRequestController.GetUniqueLanguagesFromTourRequests());
            UniqueYears = new ObservableCollection<int>(_tourRequestController.GetUniqueYearsFromTourRequests());

            CalculateStatistics();

            MonthlyViewCommand = new RelayCommand(Monthly_View_Click, CanExecuteMonthlyViewClick);
            CreateFromStatsCommand = new RelayCommand(CreateFromRequestStats_Click, CanExecuteCreateFromRequestStatsClick);
            LocationComboBox_SelectionChangedCommand = new RelayCommand(LocationComboBox_SelectionChanged, CanExecuteLocationComboBox);
            LanguageComboBox_SelectionChangedCommand = new RelayCommand(LanguageComboBox_SelectionChanged, CanExecuteLanguageComboBox);
            YearComboBox_SelectionChangedCommand = new RelayCommand(YearComboBox_SelectionChanged, CanExecuteYearComboBox);
        }

        public void LocationComboBox_SelectionChanged(object param)
        {
            CalculateStatistics();
        }

        public bool CanExecuteLocationComboBox(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void LanguageComboBox_SelectionChanged(object param)
        {
            CalculateStatistics();
        }

        public bool CanExecuteLanguageComboBox(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void YearComboBox_SelectionChanged(object param)
        {
            CalculateStatistics();
        }

        public bool CanExecuteYearComboBox(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        private void CalculateStatistics()
        {
            if (_tourRequestController != null) 
            {
                if (SelectedYear != 0)
                {
                    SelectedYearRequestCount = _tourRequestController.CountRequestsByYear(SelectedYear);
                }

                if (SelectedLocation != null && !string.IsNullOrEmpty(SelectedLanguage))
                {
                    SelectedLocationRequestCount = _tourRequestController.CountRequestsByLocation(SelectedLocation);
                    SelectedLanguageRequestCount = _tourRequestController.CountRequestsByLanguage(SelectedLanguage);
                }
                else if (SelectedLocation != null)
                {
                    SelectedLocationRequestCount = _tourRequestController.CountRequestsByLocation(SelectedLocation);
                    SelectedLanguageRequestCount = 0;
                }
                else if (!string.IsNullOrEmpty(SelectedLanguage))
                {
                    SelectedLanguageRequestCount = _tourRequestController.CountRequestsByLanguage(SelectedLanguage);
                    SelectedLocationRequestCount = 0;
                }
                else
                {
                    SelectedLocationRequestCount = 0;
                    SelectedLanguageRequestCount = 0;
                }
            }
        }

        public void Monthly_View_Click(object param)
        {
            if (SelectedYear != null)
            {
                MonthlyStats monthlyStats = new MonthlyStats(SelectedYear);
                monthlyStats.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a year first.");
            }
        }

        public bool CanExecuteMonthlyViewClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void CreateFromRequestStats_Click(object param)
        {
            CreateTourFromRequestStats createTourFromRequestStats = new CreateTourFromRequestStats();
            createTourFromRequestStats.Show();
        }

        public bool CanExecuteCreateFromRequestStatsClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

    }
}
