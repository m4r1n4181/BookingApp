using BookingApp.Service;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;

namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService tourService;

        public TourController()
        {
            tourService = new TourService();
        }

        public void EndTour(int id)
        {
            tourService.EndTour(id);
        }

        public void CreateTour(Tour tour)
        {
            tourService.CreateTour(tour);

        }

        public List<Tour> GetTodayTours()
        {
            return tourService.GetTodayTours();
        }

        public void StartTour(int id)
        {
            tourService.StartTour(id);
        }

    }


}
