using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View.OwnerWindows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestHandlingViewModel : ViewModelBase
    {
        
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationController _accommodationReservationController;
        public NotificationController _notificationController;

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
        public ICommand AcceptRequestButtonCommand { get; private set; }
        public ICommand DeclineRequestButtonCommand { get; private set; }

        public ReservationRescheduleRequest rescheduleRequest { get; set; }

        public ReservationRescheduleRequestHandlingViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();
            _notificationController = new NotificationController();

            rescheduleRequest = reservationRescheduleRequest;
            rescheduleRequest = _reservationRescheduleRequestController.GetWithGuest(reservationRescheduleRequest.Reservation.Guest.Id);
            Guest = rescheduleRequest.Reservation.Guest.Username;
            if (!_accommodationReservationController.IsReschedulePossible(rescheduleRequest))
            {
                Available = "Smeštaj je zauzet.";
                MessageBox.Show("Smeštaj je rezervisan u traženim datumima.");

            }
            else
            {
                Available = "Smeštaj je slobodan.";
            }

            AcceptRequestButtonCommand = new RelayCommand(ExecuteAcceptRequestButtonCommand, CanExecuteAcceptRequestButtonCommand);
            DeclineRequestButtonCommand = new RelayCommand(ExecuteDeclineRequestButtonCommand, CanExecuteDeclineRequestButtonCommand);

        }


        private void ExecuteAcceptRequestButtonCommand(object param)
        {
            //rescheduleRequest.Id = rescheduleRequest.Id;

            rescheduleRequest.Status = Model.Enums.RequestStatusType.Approved;
            rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
            rescheduleRequest.Reservation.Departure = rescheduleRequest.NewEnd;
            _accommodationReservationController.Update(rescheduleRequest.Reservation);
            _reservationRescheduleRequestController.Update(rescheduleRequest);

            string message = "Your reservation for accommodation " + rescheduleRequest.Reservation.Accommodation.Name + " has been Approved"; 
            Notification notification = new Notification()
            {
                User = rescheduleRequest.Reservation.Guest,
                Message = message,
                NotificationStatus = Model.Enums.NotificationStatus.unread
            };
            _notificationController.Create(notification);
            MessageBox.Show("uspesno pomerena rezervacija");
            return;
        }
        private void ExecuteDeclineRequestButtonCommand(object param) 
        {
            DeclineReservationRescheduleRequestCommentWindow Comment = new DeclineReservationRescheduleRequestCommentWindow(rescheduleRequest);
            Comment.Show();
        }
        private bool CanExecuteAcceptRequestButtonCommand(object param)
        {
         return true;
        }
        private bool CanExecuteDeclineRequestButtonCommand(object param)
        {
            return true;
        }


    }
}
