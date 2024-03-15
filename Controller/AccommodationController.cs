using System;
using System.Collections.Generic;
using BookingApp.DTO;
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

        public List<Accommodation> GetAll()
        {
            return _accommodationService.GetAll();
        }
        
        public void RegisterAccommondation(Accommodation accommondation)
        {
            _accommodationService.RegisterAccommondation(accommondation);
            
        }

        public List<Accommodation> SearchAccommodations(AccommodationSearchParams searchParams)
        {
            return _accommodationService.SearchAccommodations(searchParams);
        }


    }

}