using BookingApp.Service;
using BookingApp.Model;
using System.Collections.Generic;
using BookingApp.DTO; // Import the necessary namespace for TourSearchParams

namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }

        public void CreateTour(Tour tour)
        {
            _tourService.CreateTour(tour);
        }

        // Add public access modifier
        public List<Tour> SearchTours(TourSearchParams tourSearchParams)
        {
            return _tourService.SearchTours(tourSearchParams);
        }

        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }

        public List<Tour> GetAllWithLocations()
        {
            return _tourService.GetAllWithLocations();
        }

      
    }


}
