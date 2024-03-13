using System.Collections.Generic;
using System.Runtime.Serialization;
using BookingApp.Model;
<<<<<<< HEAD
using BookingApp.Serializer;
=======
using BookingApp.Model.Enums;
>>>>>>> e0df37cb42a87101dd7cbaad0780f9461965e4fb

namespace BookingApp.Repository
{
    public class GuestRepository
    {

        private const string FilePath = "D:/LETNJI/simsProjekat/sims-in-2024-group-3-team-b/Resources/Data/accommodation.csv";

        private readonly Serializer<Guest> _serializer;

        public List<Guest> _guests = new List<Guest>();


        public List<Accommodation> GetAll() { return null; }

        public List<Accommodation> GetByName(string name) { return null; }

        public List<Accommodation> GetByLocationCountry(string locationCountry) { return null; }

        public List<Accommodation> GetByLocationCity(string locationCity) { return null; }

        public List<Accommodation> GetByType(AccommodationType type) { return null; }

        public List<Accommodation> GetByMaxGuests(int maxGuests) { return null; }

        public List<Accommodation> GetByDays(int minReservationDays, int cancellationDays) { return null; }






    }
}


