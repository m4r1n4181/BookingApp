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
    public class TourViewModel : INotifyPropertyChanged
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

        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }
        public ObservableCollection<string> Pictures { get; set; }

        public Tour SelectedTour { get; set; }
        private KeyPointController _keyPointController;

        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }

        private TourController _tourController;
        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (value != _imagePath)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }
        private int pictureIndex;
        public TourViewModel(Tour tour)
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

            NextImageCommand = new RelayCommand(NextImage_Click, CanExecuteNextImageClick);
            PreviousImageCommand = new RelayCommand(PreviousImage_Click, CanExecutePreviousImageClick);


            Pictures = new ObservableCollection<string>(tour.Pictures);
            if(tour.Pictures.Count != 0)
            {
                ImagePath = tour.Pictures[0];
            }
            pictureIndex = 0;

        }

        public void NextImage_Click(object param)
        {
            if (pictureIndex == SelectedTour.Pictures.Count - 1)
            {
                pictureIndex = 0;
            }
            else
            {
                pictureIndex++;
            }
            ImagePath = SelectedTour.Pictures[pictureIndex];
        }

        public bool CanExecuteNextImageClick(object param)
        {
            return true;

        }

        public void PreviousImage_Click(object param)
        {
            if (pictureIndex == 0)
            {
                pictureIndex = SelectedTour.Pictures.Count - 1;
            }
            else
            {
                pictureIndex--;
            }
            ImagePath = SelectedTour.Pictures[pictureIndex];
        }

        public bool CanExecutePreviousImageClick(object param)
        {
            return true;

        }
    }
}
