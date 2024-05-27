using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
      
        public void Resignation(int guideId)
        {
            _tourGuideService.Resignation(guideId);
        }

        public bool IsSuperGuide(int guideId)
        {
            return _tourGuideService.IsSuperGuide(guideId);
        }
    }
}
