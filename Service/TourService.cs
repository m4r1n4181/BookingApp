using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace BookingApp.Service
{
    public class TourService
    {
        private TourRepository _tourRepository;
        private KeyPointRepository _keyPointRepository;
        private TouristRepository _touristRepository;
        private TouristEntryRepository _entryRepository;

        public TourService()
        {
            _tourRepository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            _touristRepository = new TouristRepository(); // Initialize tourist repository
            _entryRepository = new TouristEntryRepository(); // Initialize tourist entry repository
        }


        public Tour CreateTour(Tour tour)
        {
            tour = _tourRepository.Save(tour);


            return tour;

        }

        public List<Tour> GetTodayTours()
        {
            return _tourRepository.GetTodayTours();
        }

        public void StartTour(int id)
        {
            Tour tour = _tourRepository.GetById(id);
            if (tour == null || tour.IsStarted)
            {
                return;
            }

            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(id);
            if (keyPoints.Count == 0)
            {
                return;
            }

            tour.IsStarted = true;
            _tourRepository.Update(tour);

            foreach(KeyPoint keyPoint in keyPoints)
            {
                keyPoint.IsActive = false;
                _keyPointRepository.Update(keyPoint);
            }

            KeyPoint firstPoint = keyPoints.ElementAt(0);
            firstPoint.IsActive = true;
            _keyPointRepository.Update(firstPoint);
        }


        public void TouristArrival(int touristId, int keyPointId)
        {
            Tourist tourist = _touristRepository.GetById(touristId);
            if (tourist == null)
            {
                return;
            }
            KeyPoint keyPoint = _keyPointRepository.GetById(keyPointId);
            if (keyPoint == null)
            {
                return;
            }

            Tour tour = keyPoint.Tour;
            TouristEntry entry = new TouristEntry() { Tourist = tourist, KeyPoint = keyPoint, Tour = tour };
            _entryRepository.Save(entry);
        }

        public void EndTour(int id)
        {
            Tour tour = _tourRepository.GetById(id);
            if (tour == null || !tour.IsStarted)
            {
                return;
            }
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(id);

            foreach (KeyPoint keyPoint in keyPoints)

            {
                keyPoint.IsActive = false;
                _keyPointRepository.Update(keyPoint);
            }
            tour.IsStarted = false;
            _tourRepository.Update(tour);
        }


        // Move this method inside the class TourService
        public List<Tour> SearchTours(TourSearchParams tourSearchParams)
        {
            return _tourRepository.SearchTours(tourSearchParams);
        }

      
        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public List<Tour> GetAllWithLocations()
        {
            return _tourRepository.GetAllWithLocations();
        }
    }
}
