using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace BookingApp.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestHandle.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestHandle : Window, INotifyPropertyChanged

    {
        public ReservationRescheduleRequest rescheduleRequest { get; set; }
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationController _accommodationReservationController;


        #region NotifyProperties
        private string _guest;
        public string Guest
        {
            get => _guest;
            set
            {
                if (value != _guest)
                {
                    _guest = value;
                    OnPropertyChanged("Guest");
                }
            }
        }

        private string _available;
        public string Available
        {
            get => _available;
            set
            {
                if (value != _available)
                {
                    _available = value;
                    OnPropertyChanged("Available");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
        public ReservationRescheduleRequestHandle(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();

            rescheduleRequest = reservationRescheduleRequest;
            Guest = rescheduleRequest.Guest.Username;
            if (!_accommodationReservationController.IsReschedulePossible(rescheduleRequest))
            {
                Available = "Smeštaj je zauzet.";
                MessageBox.Show("Smeštaj je rezervisan u traženim datumima.");

            }
            else
            {
                Available = "Smeštaj je slobodan.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void AcceptRequestButton_Click(object sender, RoutedEventArgs e)
        {
            rescheduleRequest.Status = Model.Enums.RequestStatusType.Approved;
            rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
            rescheduleRequest.Reservation.Departure = rescheduleRequest.NewEnd;
            _accommodationReservationController.Update(rescheduleRequest.Reservation);
            _reservationRescheduleRequestController.Update(rescheduleRequest);
            Close();
        }

        private void DeclineRequestButton_Click(object sender, RoutedEventArgs e)
        {
           DeclineRescheduleRequestComment CommentForm = new DeclineRescheduleRequestComment(rescheduleRequest);
            CommentForm.Show();
            Close();
           

        }
    }
}
