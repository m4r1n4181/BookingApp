using System;
using BookingApp.Model;
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
        
    }

}