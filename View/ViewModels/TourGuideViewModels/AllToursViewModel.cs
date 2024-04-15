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
    public class AllToursViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public RelayCommand ViewCommand { get; set; }

        public RelayCommand ActiveCommand { get; set; }

        public AllToursViewModel()
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllWithLocations());
            ViewCommand = new RelayCommand(View_Click, CanExecuteViewClick);
            ActiveCommand = new RelayCommand(ActiveTours_Click, CanExecuteActiveToursClick);

        }

        public void View_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourView tourView = new TourView(SelectedTour);
            tourView.ShowDialog();


        }

        public bool CanExecuteViewClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void ActiveTours_Click(object param)
        {

            ActiveTours activeTours = new ActiveTours();
            activeTours.Show();


        }
        public bool CanExecuteActiveToursClick(object param)
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
