using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View.OwnerWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestHandlingViewModel : ViewModelBase
    {
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

        public ReservationRescheduleRequest rescheduleRequest { get; set; }

        public ReservationRescheduleRequestHandlingViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();

            rescheduleRequest = reservationRescheduleRequest;
            //Guest = rescheduleRequest.Guest.Username;
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

        
        private void AcceptRequestButton_Click(object sender, RoutedEventArgs e)
        {
            rescheduleRequest.Status = Model.Enums.RequestStatusType.Approved;
            rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
            rescheduleRequest.Reservation.Departure = rescheduleRequest.NewEnd;
            _accommodationReservationController.Update(rescheduleRequest.Reservation);
            _reservationRescheduleRequestController.Update(rescheduleRequest);
        }
        private void DeclineRequestButton_Click(object sender, RoutedEventArgs e)
        {
            DeclineReservationRescheduleRequestCommentWindow Comment = new DeclineReservationRescheduleRequestCommentWindow(rescheduleRequest);
            Comment.Show();
        }



    }
}
