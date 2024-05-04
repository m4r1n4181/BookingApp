using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using Microsoft.Win32;
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
    /// Interaction logic for CreateTourFromRequestStats.xaml
    /// </summary>
    public partial class CreateTourFromRequestStats : Window, INotifyPropertyChanged
    {
        private readonly TourRequestController _tourRequestController;
        private readonly TourController _tourController;
        private readonly LocationController _locationController;
        private readonly KeyPointController _keyPointController;


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

        private Location _tourlocation;
        public Location TourLocation
        {
            get => _tourlocation;
            set
            {
                if (value != _tourlocation)
                {
                    _tourlocation = value;
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

        public List<string> Pictures { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<DateTime> DateTimes { get; set; }
        public ObservableCollection<string> PossibleTimes { get; set; }

        public string SelectedTime { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreateTourFromRequestStats()
        {
            InitializeComponent();
            this.DataContext = this;

            _tourRequestController = new TourRequestController();
            _tourController = new TourController();
            _locationController = new LocationController();
            _keyPointController = new KeyPointController();

            Pictures = new List<string>();
            KeyPoints = new List<KeyPoint>();
            DateTimes = new List<DateTime>();
            AddedKeyPoint = "";
            TourDate = DateTime.Now;

            PossibleTimes = new ObservableCollection<string>() { "9:00", "12:00", "15:00", "18:00" };

            string mostRequestedLanguage = _tourRequestController.GetMostRequestedLanguageLastYear();
            TourLanguage = mostRequestedLanguage;

            Location mostRequestedLocation = _tourRequestController.GetMostRequestedLocationLastYear();
            TourLocation = mostRequestedLocation;



        }

        public void CreateFromRequestStats_Click(object sender, RoutedEventArgs e)
        {
            Location mostRequestedLocation = _tourRequestController.GetMostRequestedLocationLastYear();
            string mostRequestedLanguage = _tourRequestController.GetMostRequestedLanguageLastYear();

            Tour newTour = new Tour
            {
                Name = TourName,
                TourGuide = SignInForm.LoggedUser,
                Description = Description,
                Location = mostRequestedLocation,
                MaxTourists = MaxTourists,
                AvailableSeats = MaxTourists,
                Duration = Duration,
                Language = mostRequestedLanguage, 
                Pictures = Pictures,
            };

            List<DateTime> dateTimes = new List<DateTime> { TourDate };
            List<KeyPoint> keyPoints = KeyPoints.Select(kp => new KeyPoint { Name = kp.Name, IsActive = kp.IsActive, Tour = newTour }).ToList();

            _tourRequestController.CreateTourFromRequest(dateTimes, keyPoints, newTour.Name, newTour.Description, newTour.MaxTourists, newTour.Duration, newTour.Pictures);
        }

        private void AddImages_Click(object sender, RoutedEventArgs e)
        {
            if (Pictures == null)
            {
                Pictures = new List<string>();
            }

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

        private void Add_DateTime_Click(object sender, RoutedEventArgs e)
        {
            TourDate = TourDate.Date;
            TimeSpan timeOfDay = TimeSpan.Parse(SelectedTime);
            TourDate = TourDate.Add(timeOfDay);

            DateTimes.Add(TourDate);
        }

    }
}
