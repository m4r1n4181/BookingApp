using BookingApp.Controller;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestsViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationRescheduleRequest> reservationRescheduleRequests;
        public ObservableCollection<ReservationRescheduleRequest> ReservationRescheduleRequests
        {
            get { return reservationRescheduleRequests; }
            set
            {
                reservationRescheduleRequests = value;
                OnPropertyChanged();
            }
        }
        public ReservationRescheduleRequestController _reservationRescheduleRequestsController;

        public ReservationRescheduleRequest SelectedReservationRescheduleRequest { get; set; }


        public ReservationRescheduleRequestsViewModel()
        {
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllRequestsForHandling());
        }

    }
}
