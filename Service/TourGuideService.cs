using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourGuideService
    {
        private TourGuideRepository _tourGuideRepository;
       

        public TourGuideService()
        {
            _tourGuideRepository = new TourGuideRepository();
           
        }

        public List<TourGuide> GetAll()
        {
            return _tourGuideRepository.GetAll();
        }

        public TourGuide GetById(int id)
        {
            return _tourGuideRepository.GetById(id);
        }


    }
}
