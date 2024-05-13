using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourGuideController
    {
        private TourGuideService _tourGuideService;

        public TourGuideController()
        {
            _tourGuideService = new TourGuideService();
        }

        public List<TourGuide> GetAll()
        {
            return _tourGuideService.GetAll();
        }

        public TourGuide GetById(int id)
        {
            return _tourGuideService.GetById(id);
        }
    }
}
