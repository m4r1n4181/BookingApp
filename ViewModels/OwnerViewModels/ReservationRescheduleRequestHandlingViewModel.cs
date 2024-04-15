using BookingApp.Controller;
using BookingApp.Domain.Models;
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
    public class ReservationRescheduleRequestHandlingViewModel : ViewModelBase//, IClose
    {
       // public Action Close { get; set; }
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
        public ICommand AcceptRequestButtonCommand { get; private set; }
        public ICommand DeclineRequestButtonCommand { get; private set; }

        public ReservationRescheduleRequest rescheduleRequest { get; set; }

        public ReservationRescheduleRequestHandlingViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _accommodationReservationController = new AccommodationReservationController();

            //rescheduleRequest = reservationRescheduleRequest;
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


        private void ExecuteAcceptRequestButtonCommand()
        {
            rescheduleRequest.Status = Model.Enums.RequestStatusType.Approved;
            rescheduleRequest.Reservation.Arrival = rescheduleRequest.NewStart;
            rescheduleRequest.Reservation.Departure = rescheduleRequest.NewEnd;
            _accommodationReservationController.Update(rescheduleRequest.Reservation);
            _reservationRescheduleRequestController.Update(rescheduleRequest);
            MessageBox.Show("uspesno pomerena rezervacija");
            return;
        }
        private void ExecuteDeclineRequestButtonCommand() 
        {
            DeclineReservationRescheduleRequestCommentWindow Comment = new DeclineReservationRescheduleRequestCommentWindow(rescheduleRequest);
            Comment.Show();
        }
        private bool CanExecuteAcceptRequestButtonCommand()
        {
            // Add any conditions for when the command can execute, if needed
            return true;
        }
        private bool CanExecuteDeclineRequestButtonCommand()
        {
            // Add any conditions for when the command can execute, if needed
            return true;
        }


    }
}
