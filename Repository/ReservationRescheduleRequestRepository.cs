using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Serializer;
using BookingApp.Model;

namespace BookingApp.Repository
{
    public class ReservationRescheduleRequestRepository : IReservationRescheduleRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationRescheduleRequests.csv";

        private readonly Serializer<ReservationRescheduleRequest> _serializer;

        private List<ReservationRescheduleRequest> _reservationRescheduleRequests;
        
        public ReservationRescheduleRequestRepository()
        {
            _serializer = new Serializer<ReservationRescheduleRequest>();
            _reservationRescheduleRequests = _serializer.FromCSV(FilePath);
        }

        public void BindReservationRescheduleRequestWithUser()
        {
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequests)
            {
                int guestId = reservationRescheduleRequest.Reservation.Guest.Id;
                User guest = UserRepository.GetInstance().GetById(guestId);
                if (guest != null)
                {
                    reservationRescheduleRequest.Reservation.Guest = guest;
                }
                else
                {
                    Console.WriteLine("Error in accommodationLocation binding");
                }
            }
        }
        public void BindRequestwithGuest()
        {
            UserRepository userRepository = new UserRepository();
            foreach (var request in _reservationRescheduleRequests)
            {
                request.Reservation.Guest = userRepository.GetById(request.Reservation.Guest.Id);
            }
        }
        public void BindReservationRescheduleRequestWithAccommodationReservation()
        {
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequests)
            {
                int accommodationReservationId = reservationRescheduleRequest.Reservation.Id;
                AccommodationReservation accommodationReservation = AccommodationReservationRepository.GetInstance().Get(accommodationReservationId);
                if (accommodationReservation != null)
                {
                    reservationRescheduleRequest.Reservation = accommodationReservation;
                }
                else
                {
                    Console.WriteLine("Error in accommodationLocation binding");
                }
            }
        }


        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            reservationRescheduleRequest.Id = NextId();
            _reservationRescheduleRequests.Add(reservationRescheduleRequest);
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
            return reservationRescheduleRequest;
        }

        public List<ReservationRescheduleRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public ReservationRescheduleRequest Get(int id)
        {
            return _reservationRescheduleRequests.Find(rrr => rrr.Id == id);
        }

        public int NextId()
        {
            if (_reservationRescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _reservationRescheduleRequests.Max(rrr => rrr.Id) + 1;
        }
        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            ReservationRescheduleRequest founded = _reservationRescheduleRequests.Find(rrr => rrr.Id == reservationRescheduleRequest.Id);
            _reservationRescheduleRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
        }

        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            ReservationRescheduleRequest current = _reservationRescheduleRequests.Find(rrr => rrr.Id == reservationRescheduleRequest.Id);
            int index = _reservationRescheduleRequests.IndexOf(current);
            _reservationRescheduleRequests.Remove(current);
            _reservationRescheduleRequests.Insert(index, reservationRescheduleRequest);
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
            return reservationRescheduleRequest;
        }

        public List<ReservationRescheduleRequest> GetAllForGuest(int guestId)
        {
            _reservationRescheduleRequests = _serializer.FromCSV(FilePath);
            BindReservationRescheduleRequestWithAccommodationReservation();
            return _reservationRescheduleRequests.FindAll(rr => rr.Guest.Id == guestId);
        }
        public List<ReservationRescheduleRequest> GetAllWitReservation()
        {
            _reservationRescheduleRequests = _serializer.FromCSV(FilePath);
            BindReservationRescheduleRequestWithAccommodationReservation();
            return _reservationRescheduleRequests;
        }
        public List<ReservationRescheduleRequest> GetAllForOwner(int id)
        {
            _reservationRescheduleRequests = _serializer.FromCSV(FilePath);
            BindReservationRescheduleRequestWithAccommodationReservation();
            return _reservationRescheduleRequests.FindAll(rr => rr.Reservation.Accommodation.Owner.Id == id);
        }
        public ReservationRescheduleRequest GetWithGuest(int id)
        {
            _reservationRescheduleRequests = GetAllWitReservation();
            BindRequestwithGuest();
            return _reservationRescheduleRequests.Find(rr => rr.Reservation.Guest.Id == id);
        }
    }
}