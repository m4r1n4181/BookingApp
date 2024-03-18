using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationForm.xaml
    /// </summary>
    public partial class RegisterAccommodationForm : Window, INotifyPropertyChanged
    {
        private readonly AccommodationController _accommodationController;
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<AccommodationType> Types { get; set; }
        public AccommodationType? SelectedType { get; set; }
        public Location SelectedLocation { get; set; }
        private LocationController _locationController;

        private string _name;
        public string AccommodationName
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

        private AccommodationType _accommodationType;
        public AccommodationType Type
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _minReservationDays;
        public int MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cancellationDays;
        public int CancellationDays
        {
            get => _cancellationDays;
            set
            {
                if (value != _cancellationDays)
                {
                    _cancellationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> Pictures { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterAccommodationForm()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _locationController = new LocationController();
            Locations = new ObservableCollection<Location>(_locationController.GetAll());
            Types = new ObservableCollection<AccommodationType>();
            Types.Add(AccommodationType.apartment);
            Types.Add(AccommodationType.hut);
            Types.Add(AccommodationType.house);
            SelectedType = null;
            Pictures = new List<string>();
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
                string imageDestinationPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Images", imageFileName);

                // Now you have the correct absolute path to your image
                Pictures.Add(imageDestinationPath);
            }


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedType == null)
            {
                MessageBoxResult result = MessageBox.Show("Must select type"); 
                return;
            }
            if (SelectedLocation == null)
            {
                MessageBoxResult result = MessageBox.Show("Must select location");
                return;
            }
            Accommodation newAccommodation = new Accommodation
            {
                Owner = SignInForm.LoggedUser,
                Name = AccommodationName,
                Type = (AccommodationType)SelectedType,
                Location = SelectedLocation,
                MaxGuests = MaxGuests,
                MinReservationDays = MinReservationDays,
                CancellationDays = CancellationDays,
                Pictures = Pictures,
            };

            _accommodationController.RegisterAccommondation(newAccommodation);
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
