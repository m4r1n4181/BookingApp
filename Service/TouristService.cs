using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TouristService
    {
        private TouristRepository _touristRepository;
        private KeyPointRepository _keyPointRepository;
        private TouristEntryRepository _touristEntryRepository;

        public TouristService()
        {
            _touristRepository = new TouristRepository();
            _keyPointRepository = new KeyPointRepository();
            _touristEntryRepository = new TouristEntryRepository();
        }

        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }


      /*  private bool IsTouristOnKeyPoint(Tourist tourist, KeyPoint keyPoint)
        {
            List<TouristEntry> touristEntries = _touristEntryRepository.GetAllByKeyPoint(keyPoint.Id);
            return touristEntries.Any(te => te.Tourist.Id == tourist.Id);
        }*/


        /*public List<Tourist> GetAllNotOnTour(int tourId)
        {
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(tourId);
            List<Tourist> tourists = new List<Tourist>();
            foreach(Tourist tourist in _touristRepository.GetAll())
            {

                if(!keyPoints.Any(kp => IsTouristOnKeyPoint(tourist, kp))){
                    tourists.Add(tourist);
                }
            }

            return tourists;
        }*/
    }
}
