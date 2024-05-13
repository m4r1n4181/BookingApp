using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.ViewModels;
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
    public class TourStatisticsOverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public TourReservationController _tourReservationController;
        public TourController _tourController;
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        public RelayCommand ViewAgeCommand { get; set; }


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


        public TourStatisticsOverviewViewModel()
        {
            _tourController = new TourController();
            _tourReservationController = new TourReservationController();
            Years = new ObservableCollection<int>(_tourController.YearForTour(SignInForm.LoggedUser.Id));
            BestTour = _tourController.MostVisitedTour();
            SelectedYear = -1;
            Tours = new ObservableCollection<Tour>(_tourController.GetAllFinished());

            ViewAgeCommand = new RelayCommand(ViewAgeButton_Click, CanExecuteViewAgeClick);


        }

        public void ViewMostVisitedTourInGeneral_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedYear == -1)
            {
                return;
            }
            BestTour = _tourController.MostVisitedTour(SelectedYear);
        }

        public void ViewAgeButton_Click(object param)
        {
            if (SelectedTour == null)
            {
                return;
            }
            TourAge = _tourController.GetAgeStatisticsForTour(SelectedTour.Id);

        }

        public bool CanExecuteViewAgeClick(object param)
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
