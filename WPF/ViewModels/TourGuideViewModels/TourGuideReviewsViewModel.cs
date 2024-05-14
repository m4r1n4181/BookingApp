using BookingApp.Controller;
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
    public class TourGuideReviewsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public RelayCommand ViewCommand { get; set; }


        public TourGuideReviewsViewModel()
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllFinished());
            ViewCommand = new RelayCommand(ViewTourReview_Click, CanExecuteViewClick);

        }


        public void ViewTourReview_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a review.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
  
            ReviewDetails reviewDetails = new ReviewDetails(SelectedTour);
            reviewDetails.ShowDialog();


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
    }
}
