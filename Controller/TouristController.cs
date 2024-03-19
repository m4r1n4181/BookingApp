using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TouristController
    {
        private TouristService _touristService;

        public TouristController()
        {
            _touristService = new TouristService();
        }

        public List<Tourist> GetAll()
        {
            return _touristService.GetAll();
        }

        public List<Tourist> GetAllNotOnTour(int tourId)
        {
            return _touristService.GetAllNotOnTour(tourId);
        }
    }
}