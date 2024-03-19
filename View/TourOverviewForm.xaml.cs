using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourOverviewForm.xaml
    /// </summary>
    public partial class TourOverviewForm : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }

        public TourController _tourController;

        public string Country { get; set; }

        public string City { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }

        public string MaxTourists { get; set; }
        public Tour SelectedTour { get; set; }
        
        public TourOverviewForm()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllWithLocations());
            City = "";
            Country = "";
            Language = "";
            Duration = "";
            MaxTourists = "";
        }
        private void RefreshTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (Tour tour in tours)
            {
                Tours.Add(tour);
            }
        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {

            List<Tour> searchedTours = _tourController.SearchTours(Country, City, Language, MaxTourists, Duration);
            RefreshTours(searchedTours);
        }
    }
}
