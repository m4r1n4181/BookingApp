using BookingApp.Model;
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

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _keyPointService.SaveAll();
        }
    }
}
