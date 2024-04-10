using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    public partial class CancelTourView : Window, INotifyPropertyChanged
    {
        private TourReservation _tourReservation;
        private TourReservationController _tourReservationController;
        private VoucherController _voucherController;

        public string TourName { get; set; }
        public ObservableCollection<TourParticipants> Participants { get; set; }

        public ObservableCollection<VoucherType> Types { get; set; }
        public VoucherType? SelectedType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CancelTourView(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = this;

            _tourReservationController = new TourReservationController();
            _voucherController = new VoucherController();

            _tourReservation = tourReservation;

            TourName = tourReservation.Tour.Name;

            // Participants = new ObservableCollection<TourParticipants>(_tourReservationController.GetAllParticipants(tourReservation.Id));

            Types = new ObservableCollection<VoucherType>();
            Types.Add(VoucherType.resignation);
            Types.Add(VoucherType.cancellation);
            Types.Add(VoucherType.winner);
            SelectedType = VoucherType.cancellation;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CancelTour_Click(object sender, RoutedEventArgs e)
        {
           // _tourReservationController.CancelAllTourReservationsForTour(tourId);
        }


    }
}

