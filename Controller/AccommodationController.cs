using System;
using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Service;

namespace BookingApp.Controller
{
    public class AccommodationController
    {
        private AccommodationService _accommodationService;

        public AccommodationController()
        {
            _accommodationService = new AccommodationService();
        }
        
        public void RegisterAccommondation(Accommodation accommondation)
        {
            _accommodationService.RegisterAccommondation(accommondation);
            
        }

        public List<Accommodation> GetAccommodationsByName(string name)
        {
            return _accommodationService.GetAccommodationsByName(name);
        }

        public List<Accommodation> GetAccommodationsByLocationCountry(string locationCountry)
        {
            return _accommodationService.GetAccommodationsByLocationCountry(locationCountry);
        }


        public List<Accommodation> GetAccommodationsByLocationCity(string locationCity)
        {
            return _accommodationService.GetAccommodationsByLocationCity(locationCity);
        }


        public List<Accommodation> GetAccommodationsByType(AccommodationType type)
        {
            return _accommodationService.GetAccommodationsByType(type);
        }



    }

}