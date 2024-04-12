using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class KeyPointController
    {
        private KeyPointService _keyPointService;

        public KeyPointController()
        {
            _keyPointService = new KeyPointService();
        }

        public List<KeyPoint> GetAllForTour(int tourId)
        {
            return _keyPointService.GetAllForTour(tourId);
        }

        public void ActivateKeyPoint(int keyPointId)
        {
            _keyPointService.ActivateKeyPoint(keyPointId);
        }
    }
}