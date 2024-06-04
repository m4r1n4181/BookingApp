using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace BookingApp.WPF.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for LocationRecommendation.xaml
    /// </summary>
    public partial class LocationRecommendation : Window, INotifyPropertyChanged
    {
        public AccommodationReservationController AccommodationReservationController;
       
        private List<Location> _topLocations;
        public List<Location> TopLocations
        {
            get { return _topLocations; }
            set
            {
                _topLocations = value;
                OnPropertyChanged(nameof(TopLocations));
            }
        }
        private List<Location> _worstLocations;
        public List<Location> WorstLocations
        {
            get { return _worstLocations; }
            set
            {
                _worstLocations = value;
                OnPropertyChanged(nameof(WorstLocations));
            }
        }
        public LocationRecommendation()
        {
            this.DataContext = this;
            AccommodationReservationController = new AccommodationReservationController();
           // ObservableCollection<Location> locations = new ObservableCollection<Location>(AccommodationReservationController.GetTopThreePopularLocations());
            TopLocations = new List<Location>(AccommodationReservationController.GetTopThreePopularLocations());
            WorstLocations =new  List<Location>(AccommodationReservationController.GetWorstTreePopularLocations());
            InitializeComponent();
        }
        private void LoadTopLocations()
        {
            // Simulating loading top locations, replace with actual method to load locations
            TopLocations = new AccommodationReservationController().GetTopThreePopularLocations();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      
    }
}
