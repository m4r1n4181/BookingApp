using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
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
    /// mozda dodati polje za datetime cisto da imam uvid u to kad treba tura da se desi al svakako radi 48sati pre...
    public partial class AllTourGuideReservations : Window,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private TourReservationController _tourReservationController;

        private ObservableCollection<TourReservation> TourReservation;
   

        private TourController _tourController;

        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged();
                }
            }
        }

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



        public AllTourGuideReservations()
        {
            InitializeComponent();
            this.DataContext = this;

            _tourController = new TourController();

            Tours = new ObservableCollection<Tour>(_tourController.GetTourInFuture());
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                /*if (_tourReservationController == null)
                {
                    _tourReservationController = new TourReservationController(); // Inicijalizacija ako nije već inicijalizovan
                }

                _tourReservationController.CancelAllTourReservationsForTour(SelectedTour.Id);*/
               // CancelTourView cancelTourView = new CancelTourView();
                //cancelTourView.Show();
            }
        }


    }
}
