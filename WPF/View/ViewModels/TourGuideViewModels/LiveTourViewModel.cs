using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class LiveTourViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public RelayCommand ActivateCommand { get; set; }

        public LiveTourViewModel()
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetTodayTours());
            ActivateCommand = new RelayCommand(Activate_Click, CanExecuteActivateClick);
        }


        public void Activate_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _tourController.StartTour(SelectedTour.Id);
            TourDetails tourDetails = new TourDetails(SelectedTour);
            tourDetails.ShowDialog();
        }

        public bool CanExecuteActivateClick(object param)
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
