using BookingApp.Controller;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class DeclineReservationRescheduleRequestCommentViewModel : ViewModelBase
    {
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;

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

       
        public DeclineReservationRescheduleRequestCommentViewModel(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequest = reservationRescheduleRequest;


        }
        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {

            ReservationRescheduleRequest.Status = Model.Enums.RequestStatusType.Declined;
            ReservationRescheduleRequest.Comment = Comment;
            _reservationRescheduleRequestController.Update(ReservationRescheduleRequest);
        }

    }
}
