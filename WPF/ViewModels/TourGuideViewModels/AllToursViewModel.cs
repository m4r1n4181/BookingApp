using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class AllToursViewModel : INotifyPropertyChanged
    {
        private bool _isAllToursSelected;
        public bool IsAllToursSelected
        {
            get => _isAllToursSelected;
            set
            {
                if (value != _isAllToursSelected)
                {
                    _isAllToursSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isFutureToursSelected;
        public bool IsFutureToursSelected
        {
            get => _isFutureToursSelected;
            set
            {
                if (value != _isFutureToursSelected)
                {
                    _isFutureToursSelected = value;
                    OnPropertyChanged();

                    IsCancelTourButtonVisible = value; 

                }
            }
        }

        private bool _isActiveToursSelected;
        public bool IsActiveToursSelected
        {
            get => _isActiveToursSelected;
            set
            {
                if (value != _isActiveToursSelected)
                {
                    _isActiveToursSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCancelTourButtonVisible;
        public bool IsCancelTourButtonVisible
        {
            get => _isCancelTourButtonVisible;
            set
            {
                if (_isCancelTourButtonVisible != value)
                {
                    _isCancelTourButtonVisible = value;
                    OnPropertyChanged(nameof(IsCancelTourButtonVisible));
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private TourReservationController _tourReservationController;

        private ObservableCollection<TourReservation> TourReservation;

        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public RelayCommand ViewCommand { get; set; }

        public RelayCommand ActiveCommand { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand TutorialCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public AllToursViewModel()
        {
            _tourReservationController = new TourReservationController();

            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllWithLocations());

            IsAllToursSelected = true;
            IsFutureToursSelected = false;
            IsActiveToursSelected = false;

            StartDate = DateTime.Now;

            ViewCommand = new RelayCommand(View_Click, CanExecuteViewClick);
            AddCommand = new RelayCommand(AddTours_Click, CanExecuteAddToursClick);
            TutorialCommand = new RelayCommand(Tutorial_Click, CanExecuteTutorialClick);
            SearchCommand = new RelayCommand(Search_Click, CanExecuteSearchClick);
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecuteCancelClick);


            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(IsAllToursSelected) && IsAllToursSelected)
                {
                    IsFutureToursSelected = false;
                    IsActiveToursSelected = false;
                    Tours.Clear();
                    foreach (var tour in _tourController.GetAllWithLocations())
                    {
                        Tours.Add(tour);
                    }
                }
                else if (e.PropertyName == nameof(IsFutureToursSelected) && IsFutureToursSelected)
                {
                    IsAllToursSelected = false;
                    IsActiveToursSelected = false;
                    Tours.Clear();
                    foreach (var tour in _tourController.GetFutureTours())
                    {
                        Tours.Add(tour);
                    }
                }
                else if (e.PropertyName == nameof(IsActiveToursSelected) && IsActiveToursSelected)
                {
                    IsAllToursSelected = false;
                    IsFutureToursSelected = false;
                    Tours.Clear();
                    foreach (var tour in _tourController.GetAllActiveTours())
                    {
                        Tours.Add(tour);
                    }
                }
            };

        }

        public void Search_Click(object param)
        {
            TourGuideSearch searchParams = new TourGuideSearch()
            {
                City = City,
                Country = Country,
                MaxTourists = MaxTourists,
                Language = Language,
                StartDate = StartDate,

            };

            Tours.Clear();
            if (IsAllToursSelected)
            {
                foreach (Tour tour in _tourController.SearchTourForTourGuide(searchParams))
                {
                    Tours.Add(tour);
                }
            }
        }


            public bool CanExecuteSearchClick(object param)
            {
            return true;

            }

        public void View_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourView tourView = new TourView(SelectedTour);
            tourView.ShowDialog();


        }

        public bool CanExecuteViewClick(object param)
        {
            return true;
         
        }

        public void AddTours_Click(object param)
        {

            CreateTourForm createTourForm = new CreateTourForm();
            createTourForm.ShowDialog();


        }
        public bool CanExecuteAddToursClick(object param)
        {
            return true;
          
        }

        public void Tutorial_Click(object param)
        {
           
         //prozor za tutorial 

        }
        public bool CanExecuteTutorialClick(object param)
        {
            return true;
          
        }

        public void CancelButton_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            _tourReservationController.CancelAllTourReservationsForTour(SelectedTour.Id);
            Refresh();

        }

        private void Refresh()
        {
            Tours.Clear();
            _tourController.GetTourInFuture().ForEach(t => Tours.Add(t));
        }

        public bool CanExecuteCancelClick(object param)
        {
            return true;

        }

    }
}
