using BookingApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourAttendanceView.xaml
    /// </summary>
    public partial class TourAttendanceView : Window
    {
        public KeyPointController _keyPointController;
        public TourReservation SelectedTour {  get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }

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

       /* public TourAttendanceView()
        {
            InitializeComponent();
            this.DataContext = this;
            _keyPointController = new KeyPointController();
            CurrentTourName = "";
            CurrentKeyPointName = "";
        }*/
        public TourAttendanceView(TourReservation selectedTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _keyPointController = new KeyPointController();
            SelectedTour = selectedTour;
            KeyPoints = new ObservableCollection<KeyPoint>( _keyPointController.GetActiveKeyPointByTour(SelectedTour.Tour.Id));
            
        }
    }
}
