using BookingApp.Controller;
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
    public class TourDetailsViewModel
    {
        public string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _description;
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

        public string _language;
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
        private string _keyPoint;
        public string KeyPoint
        {
            get => _keyPoint;
            set
            {
                if (value != _keyPoint)
                {
                    _keyPoint = value;
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

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourDate;
        public string TourDate
        {
            get => _tourDate;
            set
            {
                if (value != _tourDate)
                {
                    _tourDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tour SelectedTour { get; set; }
        private KeyPointController _keyPointController;

        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }

        private TourController _tourController;

        public RelayCommand ActivateKeyPointCommand { get; set; }
        public RelayCommand MarkTouristCommand { get; set; }
        public RelayCommand EndTourCommand { get; set; }


        public TourDetailsViewModel(Tour tour)
        {
            SelectedTour = tour;

            TourName = tour.Name;
            Location = $"{tour.Location.City}, {tour.Location.Country}";
            Description = tour.Description;
            Languages = tour.Language;
            MaxTourists = tour.MaxTourists;
            Duration = tour.Duration;
            TourDate = string.Join(", ", tour.StartDate);


            _keyPointController = new KeyPointController();
            _tourController = new TourController();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetAllForTour(tour.Id));

            ActivateKeyPointCommand = new RelayCommand(ActivateKeyPoint_Click, CanExecuteActivateKeyPointClick);
            MarkTouristCommand = new RelayCommand(MarkTourist_Click, CanExecuteMarkTouristClick);
            EndTourCommand = new RelayCommand(EndTour_Click, CanExecuteEndTourClick);

        }

        public void ActivateKeyPoint_Click(object param)
        {
            if (SelectedKeyPoint == null)
            {
                MessageBox.Show("Please select a keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            _keyPointController.ActivateKeyPoint(SelectedKeyPoint.Id);
            KeyPoints.Clear();
            foreach (KeyPoint keyPoint in _keyPointController.GetAllForTour(SelectedTour.Id))
            {
                KeyPoints.Add(keyPoint);
            }
        }

        public bool CanExecuteActivateKeyPointClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void MarkTourist_Click(object param)
        {
            if (SelectedKeyPoint == null)
            {
                MessageBox.Show("Please select a keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedKeyPoint.IsActive == false)
            {
                MessageBox.Show("Please select active keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TouristSelectionForm touristSelectionForm = new TouristSelectionForm(SelectedKeyPoint);
            touristSelectionForm.ShowDialog();

        }

        public bool CanExecuteMarkTouristClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void EndTour_Click(object param)
        {
            //tourId
            _tourController.EndTour(SelectedTour.Id);
           // Close();

        }
        public bool CanExecuteEndTourClick(object param)
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
