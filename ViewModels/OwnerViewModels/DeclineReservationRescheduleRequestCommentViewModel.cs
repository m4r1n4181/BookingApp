using BookingApp.Controller;
using BookingApp.Domain.Models;
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

            ReservationRescheduleRequest = reservationRescheduleRequest;
            AddCommentButtonCommand = new RelayCommand(ExecuteAddCommentButtonCommand, CanExecuteAddCommentButtonCommand);


        }
        private void ExecuteAddCommentButtonCommand()
        {

            ReservationRescheduleRequest.Status = Model.Enums.RequestStatusType.Declined;
            ReservationRescheduleRequest.Comment = Comment;
            _reservationRescheduleRequestController.Update(ReservationRescheduleRequest);
            MessageBox.Show("uspesno odbijeno pomeranje rezervacije");
            return;
        }
        private bool CanExecuteAddCommentButtonCommand()
        {    
            return true;
        }

    }
}
