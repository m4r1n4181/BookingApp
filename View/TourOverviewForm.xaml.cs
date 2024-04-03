using BookingApp.Controller;
using BookingApp.DTO;
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
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourOverviewForm.xaml
    /// </summary>
    public partial class TourOverviewForm : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }

        public TourController _tourController;

        public TourReservationController _tourReservationController;

        /* public string Country { get; set; }

         public string City { get; set; }

         public string Language { get; set; }

         public string Duration { get; set; }

         public string MaxTourists { get; set; }*/
        public Tour SelectedTour { get; set; }
        public User User {  get; set; }

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

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourOverviewForm(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());
            User = user;
            City = "";
            Country = "";
            Language = "";
            //Duration =;
            //MaxTourists = "";
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            TourSearchParams searchParams = new TourSearchParams();
            searchParams.City = City;
            searchParams.Country = Country;
            searchParams.Language = Language;
            searchParams.Duration = Duration;
            searchParams.MaxTourists = MaxTourists;
            Tours.Clear();
            foreach (Tour tour in _tourController.SearchTours(searchParams))
            {
                Tours.Add(tour);
            }
        }

        private void buttonReserve_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null)
            {
                TourReservationForm viewReservationForm = new TourReservationForm(SelectedTour, User);
                viewReservationForm.Show();
            }
        }



        /* private void buttonSearch_Click(object sender, RoutedEventArgs e)
         {

             List<Tour> searchedTours = _tourController.SearchTours();
             RefreshTours(searchedTours);
         }*/
    }
}