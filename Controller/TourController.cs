using BookingApp.Service;
using BookingApp.Model;
using System.Collections.Generic;
using BookingApp.DTO;
using System; // Import the necessary namespace for TourSearchParams
using System.Linq;

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

        internal void EndTour(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tour> GetAllByLocations(Location location)
        {
            // Dohvati sve ture iz TourService
            List<Tour> allTours = _tourService.GetAll(); // Prilagodi ovoj liniji prema strukturi tvog koda

            // Filtriraj ture na osnovu lokacije
            List<Tour> toursWithLocation = allTours.Where(t => t.Location == location).ToList();

            return toursWithLocation;
        }
    }


}
