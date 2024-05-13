using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
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
    public class DeclineReservationRescheduleRequestCommentViewModel : ViewModelBase//, IClose
    {
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;
        private NotificationController _notificationController;

        //public Action Close { get; set; }
        #region NotifyProperties
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        public ReservationRescheduleRequest ReservationRescheduleRequest { get; set; }

        public ICommand AddCommentButtonCommand { get; private set; }

        public DeclineReservationRescheduleRequestCommentViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            _notificationController = new NotificationController();
            ReservationRescheduleRequest = reservationRescheduleRequest;
            AddCommentButtonCommand = new RelayCommand(ExecuteAddCommentButtonCommand, CanExecuteAddCommentButtonCommand);


        }

        private void ExecuteAddCommentButtonCommand(object param)
        {
            ReservationRescheduleRequest.Status = Model.Enums.RequestStatusType.Declined;
            ReservationRescheduleRequest.Comment = Comment;
            _reservationRescheduleRequestController.Update(ReservationRescheduleRequest);

            string message = "Your reservation for accommodation " + ReservationRescheduleRequest.Reservation.Accommodation.Name + " has been Declined";
            Notification notification = new Notification()
            {
                User = ReservationRescheduleRequest.Reservation.Guest,
                Message = message,
                NotificationStatus = Model.Enums.NotificationStatus.unread
            };
            _notificationController.Create(notification);
        
            MessageBox.Show("Uspješno odbijen zahtjev", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);

            
            CloseWindow();
        }

       
        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
        private bool CanExecuteAddCommentButtonCommand(object param)
        {
            // Add any conditions for when the command can execute, if needed
            return true;
        }

    }
}
