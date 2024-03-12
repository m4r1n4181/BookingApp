using BookingApp.Service;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService tourService;

        public TourController()
        {
            tourService = new TourService();
        }

        public void CreateTour(Tour tour)
        {
            tourService.CreateTour(tour);

        }
    }

   
}
