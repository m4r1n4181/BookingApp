using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Model.Enums;

namespace BookingApp.Repository
{
    public class GuestRepository
    {
        public List<Guest> Guests = new List<Guest>();


        public List<Accommodation> FindAll() { return null; }

        public List<Accommodation> FindByName(string name) { return null; }

        public List<Accommodation> FindByLocationCountry(string locationCountry) { return null; }

        public List<Accommodation> FindByLocationCity(string locationCity) { return null; }

        public List<Accommodation> FindByType(AccommodationType type) { return null; }

        public List<Accommodation> FindByMaxGuests(int maxGuests) { return null; }

        public List<Accommodation> FindByDays(int minReservationDays, int cancellationDays) { return null; }






    }
}


