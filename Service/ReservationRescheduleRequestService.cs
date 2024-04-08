using BookingApp.Domain.Models;
using BookingApp.Model.Enums;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View;


namespace BookingApp.Service
{
   public class ReservationRescheduleRequestService
    {
        private IReservationRescheduleRequestRepository _reservationRescheduleRequestRepository;
        public ReservationRescheduleRequestService()
        {
            _reservationRescheduleRequestRepository = Injector.Injector.CreateInstance<IReservationRescheduleRequestRepository>();

        }
        public List<ReservationRescheduleRequest> GetAll()
        {
            return _reservationRescheduleRequestRepository.GetAll();
        }

        public ReservationRescheduleRequest Get(int id)
        {
            return _reservationRescheduleRequestRepository.Get(id);
        }
        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest)
        {

            return _reservationRescheduleRequestRepository.Save(reservationRescheduleRequest);
        }

        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest)
        {

            _reservationRescheduleRequestRepository.Delete(reservationRescheduleRequest);

        }

        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return _reservationRescheduleRequestRepository.Update(reservationRescheduleRequest);
        }


        private bool IsRequestOnStandby(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return reservationRescheduleRequest.Status == Model.Enums.RequestStatusType.Standby;
        }

        public List<ReservationRescheduleRequest> GetAllRequestsForHandling()
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (IsRequestOnStandby(reservationRescheduleRequest) && SignInForm.LoggedUser.Id == reservationRescheduleRequest.Reservation.Accommodation.Owner.Id)
                {
                    reservationRescheduleRequests.Add(reservationRescheduleRequest);
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetStandBy(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (reservationRescheduleRequest.Guest.Id == guest)
                {
                    if (reservationRescheduleRequest.Status == RequestStatusType.Standby)
                    {
                        reservationRescheduleRequests.Add(reservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetApproved(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (reservationRescheduleRequest.Guest.Id == guest)
                {
                    if (reservationRescheduleRequest.Status == RequestStatusType.Approved)
                    {
                        reservationRescheduleRequests.Add(reservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }

        public List<ReservationRescheduleRequest> GetDeclined(int guest)
        {
            List<ReservationRescheduleRequest> reservationRescheduleRequests = new List<ReservationRescheduleRequest>();
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequestRepository.GetAll())
            {
                if (reservationRescheduleRequest.Guest.Id == guest)
                {
                    if (reservationRescheduleRequest.Status == RequestStatusType.Declined)
                    {
                        reservationRescheduleRequests.Add(reservationRescheduleRequest);
                    }
                }
            }

            return reservationRescheduleRequests;
        }
    }

}

