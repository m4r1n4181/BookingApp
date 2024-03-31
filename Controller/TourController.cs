using BookingApp.Service;
using BookingApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.DTO;
using System;
namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }

        public void EndTour(int id)
        {
            _tourService.EndTour(id);
        }

        public void CreateTour(Tour tour)
        {
            _tourService.CreateTour(tour);
        }
        public List<Tour> GetAllWithDateTime()
        {
            return _tourService.GetAllWithDateTime();
        }
        
        public List<Tour> GetAllWithLocations()
        {
            return _tourService.GetAllWithLocations();
        }

        public List<Tour> GetTodayTours()
        {
            return _tourService.GetTodayTours();
        }

        public void StartTour(int id)
        {
            _tourService.StartTour(id);
        }

        public List<Tour>SearchTours(TourSearchParams searchParams)
        {
            return _tourService.SearchTours(searchParams);
        }

        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }

    }


}
