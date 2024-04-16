using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.View.ViewModel.TouristViewModels
{
    public class TourAttendanceViewModel : INotifyPropertyChanged
    {
        public KeyPointController _keyPointController;
        public TourReservation SelectedTour { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public TourAttendanceViewModel(TourReservation selectedTour)
        {
            _keyPointController = new KeyPointController();
            SelectedTour = selectedTour;
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetActiveKeyPointByTour(SelectedTour.Tour.Id));

        }

        private string _currentTourName;
        public string CurrentTourName
        {
            get => _currentTourName;
            set
            {
                if (value != _currentTourName)
                {
                    _currentTourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentKeyPointName;
        public string CurrentKeyPointName
        {
            get => _currentKeyPointName;
            set
            {
                if (value != _currentKeyPointName)
                {
                    _currentKeyPointName = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
