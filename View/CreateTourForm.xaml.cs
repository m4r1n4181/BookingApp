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
    public partial class CreateTourForm : Window
    {
        private readonly TourRepository _tourRepository;

        public ObservableCollection<Location> Locations { get; set; }
        private LocationController _locationController;
        public Location SelectedLocation { get; set; }

        public List<string> Pictures { get; set; }



        private string _name;
        public string Name
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



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreateTourForm()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationController = new LocationController();
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Pictures = new List<string>();
        }

        private void CreateTourFrom(object sender, RoutedEventArgs e)
        {
            Tour newTour = new Tour
            {
                Name = Name,
                Description = Description,
                Language = Language,
                Location = new Location(),
                MaxTourists = MaxTourists,
                KeyPoint = new KeyPoint(),
                StartDates = new List<DateTime> { dpStartDate.SelectedDate ?? DateTime.Now },
                Duration = Duration,
                Pictures = new List<string>(),
            };


            _tourRepository.Save(newTour);
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
            openFileDialog1.Filter = "All Image Files|.png;.jpg;.jpeg;.bmp|PNG Image (.png)|.png|JPEG Image (.jpg;.jpeg)|.jpg;.jpeg";
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                string imagePath = openFileDialog1.FileName;
                string imageFileName = System.IO.Path.GetFileName(imagePath); // Get just the file name
                string imageDestinationPath = "../Resources/Images/" + imageFileName;


                //File.Copy(imagePath, imageDestinationPath, true);

                Pictures.Add(imageDestinationPath);

            }


        }


    }
}



