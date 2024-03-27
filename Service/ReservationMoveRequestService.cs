using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class ReservationMoveRequestService
    {
        private ReservationMoveRequestRepository _reservationMoveRequestRepository;

        public ReservationMoveRequestService() { 
            _reservationMoveRequestRepository = new ReservationMoveRequestRepository();
        }
        public bool IsOnStandby(ReservationMoveRequest reservationMoveRequest)
        {
            return reservationMoveRequest.Status == Model.Enums.RequestStatusType.standby;
        }


        public List<ReservationMoveRequest> GetStandBy(int guest)
        {
            List<ReservationMoveRequest> reservationMoveRequests = new List<ReservationMoveRequest>();
            foreach (ReservationMoveRequest reservationMoveRequest in _reservationMoveRequestRepository.GetAll())
            {
                if (reservationMoveRequest.Guest.Id == guest)
                {
                    if (reservationMoveRequest.Status == RequestStatusType.standby)
                    {
                        reservationMoveRequests.Add(reservationMoveRequest);
                    }
                }
            }

            return reservationMoveRequests;
        }

    }
}
