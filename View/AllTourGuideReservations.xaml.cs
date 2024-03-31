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
    /// Interaction logic for AllTourGuideReservations.xaml
    /// </summary>
    public partial class AllTourGuideReservations : Window,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TourReservation> _tourReservations;
        public ObservableCollection<TourReservation> TourReservations
        {
            get { return _tourReservations; }
            set
            {
                if (_tourReservations != value)
                {
                    _tourReservations = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourReservationController _tourReservationController;
        public TourReservation SelectedTourReservation { get; set; }

        public AllTourGuideReservations()
        {
            InitializeComponent();
            this.DataContext = this;

            _tourReservationController = new TourReservationController();
           // TourReservations = new ObservableCollection<TourReservation>(_tourReservationController.GetByTour(tourId));
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourReservation == null)
            {
                MessageBox.Show("Please select a tour reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //cancelTour
        }
    }
}
