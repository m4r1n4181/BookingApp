using System;
using BookingApp.Model;
using BookingApp.Model.Accommodation;

namespace BookingApp.Repository
{
    public class AccommodationRepository
    {
        public List<Accommodation> Accommodations { get; set; } = new List<Accommodation>();

        public void AddAccommodation(Accommodation accommodation)
        {
            // Accommodations.RegisterAccommodation(accommodation);
        }
    }
}
