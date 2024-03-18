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
    public class KeyPointService
    { 
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TourRepository tourRepository;

        public KeyPointService()
        {
            _keyPointRepository = new KeyPointRepository();
        }

        public void CreateKeyPoints(List<KeyPoint> keyPoints)
        {
            foreach(KeyPoint keyPoint in keyPoints)
            {
                _keyPointRepository.Save(keyPoint);
            }
        }


        public void ActivateKeyPoint(int keyPointId)
        {
            
            KeyPoint keyPoint = _keyPointRepository.GetById(keyPointId);
            if(keyPoint == null)
            {
                return;
            }
            
            keyPoint.IsActive = true; 
            _keyPointRepository.Update(keyPoint); 

            int tourId = keyPoint.Tour.Id;
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(tourId);

            //ako je zadnji postaviti da je tour Active=false
            if (keyPoints.All(kp => kp.IsActive))
            {
                Tour tour = keyPoint.Tour;
                keyPoint.IsActive = false;
                tourRepository.Update(tour);
            }
        }

        public List<KeyPoint> GetAllForTour(int tourId)
        {
            return _keyPointRepository.GetKeyPointsForTour(tourId);
        }
        public void SaveAll(List<KeyPoint> keyPoints)
        {
            foreach (KeyPoint keyPoint in keyPoints)
            {
                _keyPointRepository.Save(keyPoint);
            }
        }
    }
}

