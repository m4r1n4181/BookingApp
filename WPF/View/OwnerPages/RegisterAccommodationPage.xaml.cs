using BookingApp.Controller;
using BookingApp.Model.Enums;
using BookingApp.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.View;
using Microsoft.Win32;
using BookingApp.WPF.ViewModels;
using System.IO;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationPage.xaml
    /// </summary>
    public partial class RegisterAccommodationPage : Page, INotifyPropertyChanged
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
        public ObservableCollection<string> Pictures { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterAccommodationPage()
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
            Pictures = new ObservableCollection<string>();

        }


        private void AddAccommodationButtonClick(object sender, RoutedEventArgs e)
        {

            if (SelectedType == null)
            {
                MessageBoxResult result = MessageBox.Show("Must select type");
                return;
            }
            if (SelectedLocation == null)
            {
                MessageBoxResult result = MessageBox.Show("Must select location");
                return;
            }
            MessageBoxResult check = MessageBox.Show("Are you sure you want to add accommodation?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (check == MessageBoxResult.Yes)
            {

                Accommodation newAccommodation = new Accommodation
                {
                    Owner = SignInForm.LoggedUser,
                    Name = AccommodationName,
                    Type = (AccommodationType)SelectedType,
                    Location = SelectedLocation,
                    MaxGuests = MaxGuests,
                    MinReservationDays = MinReservationDays,
                    CancellationDays = CancellationDays,
                    Pictures = Pictures.ToList(),
                };

                _accommodationController.RegisterAccommondation(newAccommodation);
                MessageBox.Show("The accommodation has been successfully added.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.GoBack();
            }
        }
        private void AddImages_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png"; // Required file extension
            fileDialog.Filter = "Image Files (.png, *.jpg, *.jpeg)|.png;.jpg;.jpeg"; // Optional file extensions
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
    }
}