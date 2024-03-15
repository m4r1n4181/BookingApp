using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
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

     

        public List<Accommodation> SearchAccommodations(AccommodationSearchParams searchParams)
        {
            return _accommodationRepository.SearchAccommodation(searchParams);
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }
    }

}
