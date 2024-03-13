using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class AccommodationService
    {
        private AccommodationRepository _accommodationRepository;

        public AccommodationService() { 
            _accommodationRepository = new AccommodationRepository();
        }

        public Accommodation RegisterAccommondation(Accommodation accommodation)
        {
            accommodation = _accommodationRepository.Save(accommodation);

            return accommodation;
        }

    }

}
