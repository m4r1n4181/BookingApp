using BookingApp.Controller;
using BookingApp.Model;
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

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class CreateTourFormViewModel : INotifyPropertyChanged
    {
        private readonly TourController _tourController;


        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<string> PossibleTimes { get; set; }


        private LocationController _locationController;
        private KeyPointController _keyPointController;
        public Location SelectedLocation { get; set; }

        public List<string> Pictures { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<DateTime> DateTimes { get; set; }

        public string SelectedTime { get; set; }

        public RelayCommand AddKeyPointCommand { get; set; }
        public RelayCommand AddDateTimeCommand { get; set; }

        public RelayCommand AddImagesCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }

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
        public CreateTourFormViewModel()
        {
            _tourController = new TourController();
            _locationController = new LocationController();
            _keyPointController = new KeyPointController();
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Pictures = new List<string>();
            KeyPoints = new List<KeyPoint>();
            DateTimes = new List<DateTime>();
            AddedKeyPoint = "";
            TourDate = DateTime.Now;



            PossibleTimes = new ObservableCollection<string>() { "9:00", "12:00", "15:00", "18:00" };

            AddKeyPointCommand = new RelayCommand(Add_Key_Point_Click, CanExecuteAddKeyPointClick);
            AddDateTimeCommand = new RelayCommand(Add_DateTime_Click, CanExecuteAddDateTimeClick);
            AddImagesCommand = new RelayCommand(AddImages_Click, CanExecuteAddImagesClick);
            CancelCommand = new RelayCommand(Cancel, CanExecuteCancelClick);
            SaveCommand = new RelayCommand(CreateTourForm, CanExecuteCreateClick);


        }

        public void CreateTourForm(object param)
        {

            Tour newTour = new Tour
            {
                Name = TourName,
                Location = SelectedLocation,
                TourGuide = SignInForm.LoggedUser,
                Description = Description,
                Language = TourLanguage,
                MaxTourists = MaxTourists,
                Duration = Duration,
                Pictures = Pictures,
            };

            _tourController.CreateTour(newTour, DateTimes, KeyPoints);


            //Close();
        }


        public bool CanExecuteCreateClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void Cancel(object param)
        {

            //Close();
        }


        public bool CanExecuteCancelClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void AddImages_Click(object param)
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

        public bool CanExecuteAddImagesClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void Add_Key_Point_Click(object param)
        {
            KeyPoint keyPoint = new KeyPoint() { Name = KeyPoint, IsActive = false };
            KeyPoints.Add(keyPoint);
            AddedKeyPoint += KeyPoint + ", ";
        }


        public bool CanExecuteAddKeyPointClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void Add_DateTime_Click(object param)
        {

            TourDate = TourDate.Date;
            TimeSpan timeOfDay = TimeSpan.Parse(SelectedTime);
            TourDate = TourDate.Add(timeOfDay);

            DateTimes.Add(TourDate);
        }


        public bool CanExecuteAddDateTimeClick(object param)
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
