using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
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

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for RequestStatisticsOverview.xaml
    /// </summary>
    public partial class RequestStatisticsOverview : Window, INotifyPropertyChanged
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

        public int SelectedYear{ get; set; }


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

        public RequestStatisticsOverview()
        {
            InitializeComponent();
            DataContext = this;

            _tourRequestController = new TourRequestController();
            UniqueLocations = new ObservableCollection<Location>(_tourRequestController.GetUniqueLocationsFromTourRequests());
            UniqueLanguages = new ObservableCollection<string>(_tourRequestController.GetUniqueLanguagesFromTourRequests());
            UniqueYears = new ObservableCollection<int>(_tourRequestController.GetUniqueYearsFromTourRequests());

            CalculateStatistics();

        }

        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateStatistics();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateStatistics();
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateStatistics();
        }

        private void CalculateStatistics()
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

        private void Monthly_View_Click(object sender, RoutedEventArgs e)
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
    }
}

