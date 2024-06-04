using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAll();
        public List<AccommodationReservation> GetAllWithAccommodations();

        public AccommodationReservation Get(int id);


        public List<Location> GetWorstTreePopularLocations();
        public List<AccommodationReservation> GetAllWithGuests();

        public List<AccommodationReservation> GetAllWithGuestsAndAccommodations();
        public AccommodationReservation GetById(int id);

        public AccommodationReservation Save(AccommodationReservation accommodationReservation);


        public void Delete(AccommodationReservation accommodationreservation);
        public AccommodationReservation Update(AccommodationReservation accommodationReservation);

        public List<AccommodationReservation> GetByAccommodation(int accommodationId);
        public List<AccommodationReservation> GetByAccommodationId(int id);

        public List<AccommodationReservation> GetByOwnerId(int id);
        public List<Location> GetTopThreePopularLocations();

    }
}
