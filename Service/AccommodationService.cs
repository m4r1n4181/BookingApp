using System;
using System.Collections.Generic;
using System.Xml.Linq;
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


        public List<Accommodation> GetAccommodationsByName(string name)
        {
            return _accommodationRepository.GetByName(name);
        }


        public List<Accommodation> GetAccommodationsByLocationCountry(string locationCountry)
        {
            return _accommodationRepository.GetByLocationCountry(locationCountry);
        }

        public List<Accommodation> GetAccommodationsByLocationCity(string locationCity)
        {
            return _accommodationRepository.GetByLocationCity(locationCity);
        }

        public List<Accommodation> GetAccommodationsByType(AccommodationType type)
        {
            return _accommodationRepository.GetByType(type);
        }




    }

}
