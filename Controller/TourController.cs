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
        public List<Tour> SearchTours(string country, string city, string language, string numberOfPeople, string duration)
        {
            return _tourService.SearchTours(country, city, language, numberOfPeople, duration);
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
