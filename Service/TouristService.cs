using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
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
        private ITouristRepository _touristRepository;
        private IKeyPointRepository _keyPointRepository;
        private ITouristEntryRepository _touristEntryRepository;
        private ITourReservationRepository _tourReservationRepository;

        public TouristService()
        {
            _touristRepository = Injector.CreateInstance<ITouristRepository>();
            _keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
            _touristEntryRepository = Injector.CreateInstance<ITouristEntryRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
        }

        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }


        private bool IsTouristOnKeyPoint(Tourist tourist, KeyPoint keyPoint)
        {
            List<TouristEntry> touristEntries = _touristEntryRepository.GetAllByKeyPoint(keyPoint.Id);
            return touristEntries.Any(te => te.Tourist.Id == tourist.Id);
        }

        public List<Tourist> GetTouristsForTour(int tourId)
        {
            List<TourReservation> reservations = _tourReservationRepository.GetByTour(tourId);
            List<Tourist> tourists = new List<Tourist>();
            reservations.ForEach(r => tourists.Add(r.Tourist));
            return tourists;
        }

        public List<Tourist> GetAllNotOnTour(int tourId)
        {
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(tourId);
            List<Tourist> tourists = new List<Tourist>();
            foreach(Tourist tourist in GetTouristsForTour(tourId))
            {

                if(!keyPoints.Any(kp => IsTouristOnKeyPoint(tourist, kp))){
                    tourists.Add(tourist);
                }
            }

            return tourists;
        }
    }
}
