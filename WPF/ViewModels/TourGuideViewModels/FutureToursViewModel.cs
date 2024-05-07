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
    public class FutureToursViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private TourReservationController _tourReservationController;

        private ObservableCollection<TourReservation> TourReservation;


        private TourController _tourController;

        public RelayCommand CancelCommand { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }



        public FutureToursViewModel()
        { 

            _tourController = new TourController();
            _tourReservationController = new TourReservationController();

            Tours = new ObservableCollection<Tour>(_tourController.GetTourInFuture());
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecuteCancelClick);

        }


        public void CancelButton_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            _tourReservationController.CancelAllTourReservationsForTour(SelectedTour.Id);
            Refresh();

        }

        private void Refresh()
        {
            Tours.Clear();
            _tourController.GetTourInFuture().ForEach(t => Tours.Add(t));
        }

        public bool CanExecuteCancelClick(object param)
        {
            return true;
   
        }


    }
}
