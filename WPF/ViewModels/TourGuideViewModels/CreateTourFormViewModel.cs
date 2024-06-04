using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class CreateTourFormViewModel : ValidationBase
    {
        private readonly TourController _tourController;


        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<string> PossibleTimes { get; set; }


        private LocationController _locationController;
        private KeyPointController _keyPointController;
        public Location SelectedLocation { get; set; }

        public ObservableCollection<string> Pictures { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<DateTime> DateTimes { get; set; }

        public string SelectedTime { get; set; }

        public RelayCommand AddKeyPointCommand { get; set; }
        public RelayCommand AddDateTimeCommand { get; set; }

        public RelayCommand AddImagesCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }


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




        public CreateTourFormViewModel()
        {
            _tourController = new TourController();
            _locationController = new LocationController();
            _keyPointController = new KeyPointController();
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Pictures = new ObservableCollection<string>();
            KeyPoints = new ObservableCollection<KeyPoint>();
            DateTimes = new ObservableCollection<DateTime>();
            TourDate = DateTime.Now;


            Cities = new ObservableCollection<string>(Locations.Select(loc => loc.City).Distinct());
            Countries = new ObservableCollection<string>(Locations.Select(loc => loc.Country).Distinct());

            PossibleTimes = new ObservableCollection<string>() { "9:00", "12:00", "15:00", "18:00" };

            AddKeyPointCommand = new RelayCommand(Add_Key_Point_Click, CanExecuteAddKeyPointClick);
            AddDateTimeCommand = new RelayCommand(Add_DateTime_Click, CanExecuteAddDateTimeClick);
            AddImagesCommand = new RelayCommand(AddImages_Click, CanExecuteAddImagesClick);
            CancelCommand = new RelayCommand(Cancel, CanExecuteCancelClick);
            SaveCommand = new RelayCommand(CreateTourForm, CanExecuteCreateClick);


        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.TourName))
            {
                this.ValidationErrors["TourName"] = "TourName is required.";
            }
            if (string.IsNullOrWhiteSpace(this.TourLanguage))
            {
                this.ValidationErrors["TourLanguage"] = "Tour Language is required.";
            }

            if (string.IsNullOrWhiteSpace(this.TourName))
            {
                this.ValidationErrors["TourDate"] = "TourDate is required.";
            }

            if (this.KeyPoints == null || this.KeyPoints.Count < 2)
            {
                this.ValidationErrors["KeyPoints"] = "At least two key points are required.";
            }

            if (string.IsNullOrWhiteSpace(this.TourName))
            {
                this.ValidationErrors["TourDate"] = "TourDate is required.";
            }

        }

        public void CreateTourForm(object param)
        {
            Validate();
            if(IsValid == false)
            {
                return;
            }
            Location selectedLocation = Locations.FirstOrDefault(loc => loc.City == SelectedCity && loc.Country == SelectedCountry);

            Tour newTour = new Tour
            {
                Name = TourName,
                Location = selectedLocation,
                TourGuide = SignInForm.LoggedUser,
                Description = Description,
                Language = TourLanguage,
                MaxTourists = MaxTourists,
                Duration = Duration,
                Pictures = Pictures.ToList(),
            };

            _tourController.CreateTour(newTour, DateTimes.ToList(), KeyPoints.ToList());


            //Close();
        }


        public bool CanExecuteCreateClick(object param)
        {
            return true;
           
        }

        public void Cancel(object param)
        {

            //Close();
        }


        public bool CanExecuteCancelClick(object param)
        {
            return true;
          
        }

        public void AddImages_Click(object param)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png"; // Required file extension
            fileDialog.Filter = "Image Files (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg"; // Optional file extensions
            bool? res = fileDialog.ShowDialog();
            if (res.HasValue && res.Value)
            {
                string fileName = System.IO.Path.GetFileName(fileDialog.FileName);

                string sourceFilePath = fileDialog.FileName;

                string destinationFolder = "../../../Resources/Images/";
                string destinationFilePath = System.IO.Path.Combine(destinationFolder, fileName);
                try
                {
                    File.Copy(sourceFilePath, destinationFilePath, true);
                }
                catch (IOException ex)
                {
                    //MessageBox.Show("Error copying file: " + ex.Message);
                }


                destinationFolder = "../../../Resources/Images/";
                destinationFilePath = System.IO.Path.Combine(destinationFolder, fileName);
                Pictures.Add(destinationFilePath);
            }


        }

        public bool CanExecuteAddImagesClick(object param)
        {
            return true;
       
        }

        public void Add_Key_Point_Click(object param)
        {
            KeyPoint keyPoint = new KeyPoint() { Name = KeyPoint, IsActive = false };
            KeyPoints.Add(keyPoint);
        }


        public bool CanExecuteAddKeyPointClick(object param)
        {
            return true;
           
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
           
        }
    }
}
