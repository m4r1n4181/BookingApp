using System;
using BookingApp.Model;

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
