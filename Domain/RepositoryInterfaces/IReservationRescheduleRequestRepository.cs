using BookingApp.Domain.Models;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReservationRescheduleRequestRepository
    {
        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest);

        public List<ReservationRescheduleRequest> GetAll();
        public ReservationRescheduleRequest Get(int id);
        public int NextId();
        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest);
        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest);
        public List<ReservationRescheduleRequest> GetAllForGuest(int guestId);
        public List<ReservationRescheduleRequest> GetAllWitReservation();
        public List<ReservationRescheduleRequest> GetAllForOwner(int id);
        public ReservationRescheduleRequest GetWithGuest(int id);
    }
}

