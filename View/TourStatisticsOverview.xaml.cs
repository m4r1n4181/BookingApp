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
    /// Interaction logic for TourStatisticsOverview.xaml
    /// </summary>
    public partial class TourStatisticsOverview : Window, INotifyPropertyChanged
    {

        public ObservableCollection<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public TourReservationController _tourReservationController;
        public TourController _tourController;
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }


        private Tour _bestTour;
        public Tour BestTour
        {
            get => _bestTour;
            set
            {
                if (value != _bestTour)
                {
                    _bestTour = value;
                    OnPropertyChanged("BestTour");
                }
            }
        }

        private TourAgeGroupStatistic _tourAge;
        public TourAgeGroupStatistic TourAge
        {
            get => _tourAge;
            set
            {
                if (value != _tourAge)
                {
                    _tourAge = value;
                    OnPropertyChanged("TourAge");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourStatisticsOverview()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            _tourReservationController = new TourReservationController();
            Years = new ObservableCollection<int>(_tourController.YearForTour(SignInForm.LoggedUser.Id));
            BestTour = _tourController.MostVisitedTour();
            SelectedYear = -1;
            Tours = new ObservableCollection<Tour>(_tourController.GetAllTour(SignInForm.LoggedUser.Id));

        }

        private void ViewMostVisitedTourInGeneral_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedYear == -1)
            {
                return;
            }
            BestTour = _tourController.MostVisitedTour(SelectedYear);
        }

        private void ViewAgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                return;
            }
            TourAge = _tourReservationController.GetAgeStatisticsForTour(SelectedTour.Id);

        }

    }

}

