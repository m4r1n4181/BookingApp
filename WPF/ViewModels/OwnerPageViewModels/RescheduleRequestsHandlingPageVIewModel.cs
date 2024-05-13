using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View.OwnerWindows;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class RescheduleRequestsHandlingPageVIewModel : ViewModelBase//, IClose
    {
        // Action Close { get; set; }
       
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationController _accommodationReservationController;
        public NotificationController _notificationController;

        #region NotifyProperties
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

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        #endregion
        public ICommand AcceptRequestCommand { get; private set; }
        public ICommand DeclineRequestCommand { get; private set; }

        public ReservationRescheduleRequest rescheduleRequest { get; set; }

        public RescheduleRequestsHandlingPageVIewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();
            _notificationController = new NotificationController();
            Comment = reservationRescheduleRequest.Comment;
            rescheduleRequest = reservationRescheduleRequest;
            rescheduleRequest = _reservationRescheduleRequestController.GetWithGuest(reservationRescheduleRequest.Reservation.Guest.Id);

            if (!_accommodationReservationController.IsReschedulePossible(rescheduleRequest))
            {
                Available = "! NOT AVAILABLE";         
            }
            else
            {
                Available = "! AVAILABLE";
            }

            AcceptRequestCommand = new RelayCommand(ExecuteAcceptRequestCommand, CanExecuteAcceptRequestCommand);
            DeclineRequestCommand = new RelayCommand(ExecuteDeclineRequestCommand, CanExecuteDeclineRequestCommand);
        }


        private void ExecuteAcceptRequestCommand(object param)
        {  
            var result = MessageBox.Show("Are you sure you want to accept the request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                rescheduleRequest.Status = Model.Enums.RequestStatusType.Approved;
                rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
                rescheduleRequest.Reservation.Departure = rescheduleRequest.NewEnd;
                //rescheduleRequest.Comment = Comment;
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

                MessageBox.Show("The reservation has been successfully rescheduled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); //ok
            }
            else
            {
                MessageBox.Show("Reservation rescheduling canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); //ok
            }
        }


        private void ExecuteDeclineRequestCommand(object param)
        {
            var result = MessageBox.Show("Are you sure you want to decline the request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                rescheduleRequest.Status = Model.Enums.RequestStatusType.Declined;
                rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
              //  rescheduleRequest.Comment = Comment;
                _accommodationReservationController.Update(rescheduleRequest.Reservation);
                _reservationRescheduleRequestController.Update(rescheduleRequest);

                string message = "Your reservation for accommodation " + rescheduleRequest.Reservation.Accommodation.Name + " has been DECLINED";
                Notification notification = new Notification()
                {
                    User = rescheduleRequest.Reservation.Guest,
                    Message = message,
                    NotificationStatus = Model.Enums.NotificationStatus.unread
                };
                _notificationController.Create(notification);

                MessageBox.Show("The reservation has been successfully declined.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); //ok;
            }
            else
            {
                MessageBox.Show("Reservation rescheduling canceled.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); //ok
            }
        }

        private bool CanExecuteAcceptRequestCommand(object param)
        {
            return true; 
        }

        private bool CanExecuteDeclineRequestCommand(object param)
        {
            return true; 

        }

    }
}
