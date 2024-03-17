using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View
{
    public partial class CreateTourForm : Window, INotifyPropertyChanged
    {
        private readonly TourController _tourController;


        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<string> PossibleTimes { get; set; }
        private LocationController _locationController;
        private KeyPointController _keyPointController;
        public Location SelectedLocation { get; set; }

        public List<string> Pictures { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }

        public string SelectedTime { get; set; }


        private string _addedKeyPoint;
        public string AddedKeyPoint
        {
            get => _addedKeyPoint;
            set
            {
                if (value != _addedKeyPoint)
                {
                    _addedKeyPoint = value;
                    OnPropertyChanged("AddedKeyPoint");
                }
            }
        }

        private string _name;
        public string TourName
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
        public string TourLanguage
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

        private DateTime _tourDate;
        public DateTime TourDate
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
        public CreateTourForm()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            _locationController = new LocationController();
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Pictures = new List<string>();
            KeyPoints = new List<KeyPoint>();
            AddedKeyPoint = "";
            TourDate = DateTime.Now;



            PossibleTimes = new ObservableCollection<string>() { "9:00", "12:00", "15:00", "18:00" };
        }

        private void CreateTourFrom(object sender, RoutedEventArgs e)
        {
            Tour newTour = new Tour
            {
                Name = Name,
                Description = Description,
                Language = TourLanguage,
                MaxTourists = MaxTourists,
                StartDates = new List<DateTime> { dpStartDate.SelectedDate ?? DateTime.Now },
                Duration = Duration,
            };

            TourDate = TourDate.Date;
            TimeSpan timeOfDay = TimeSpan.Parse(SelectedTime);
            TourDate = TourDate.Add(timeOfDay);


            _tourController.CreateTour(newTour);

            KeyPoints.ForEach(kp => kp.Tour = newTour);

            _keyPointController.SaveAll(KeyPoints);

            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void AddImages_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images";
            openFileDialog1.Filter = "All Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                string imagePath = openFileDialog1.FileName;
                string imageFileName = System.IO.Path.GetFileName(imagePath); 
                string imageDestinationPath = "../Resources/Images/" + imageFileName;

                Pictures.Add(imageDestinationPath);

            }


        }

        private void Add_Key_Point_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint keyPoint = new KeyPoint() { Name = KeyPoint, IsActive = false };
            KeyPoints.Add(keyPoint);
            AddedKeyPoint += KeyPoint + ", ";
        }
    }
}



