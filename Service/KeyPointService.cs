﻿using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
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
        private IKeyPointRepository _keyPointRepository;
        private ITourRepository _tourRepository;

        public KeyPointService()
        {
            _keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
        }

        public void CreateKeyPoints(List<KeyPoint> keyPoints)
        {
            foreach (KeyPoint keyPoint in keyPoints)
            {
                _keyPointRepository.Save(keyPoint);
            }
        }


        public void ActivateKeyPoint(int keyPointId)
        {

            KeyPoint keyPoint = _keyPointRepository.GetById(keyPointId);
            if (keyPoint == null)
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
                Tour tour = _tourRepository.GetById(tourId);

                tour.TourStatus = Model.Enums.TourStatusType.finished;
                _tourRepository.Update(tour);
            }
        }

        public List<KeyPoint> GetAllForTour(int tourId)
        {
            return _keyPointRepository.GetKeyPointsForTour(tourId);
        }

        /* public KeyPoint GetActiveKeyPointByTour(int tourId)
         {
             List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(tourId);

             foreach (KeyPoint keyPoint in keyPoints)
             {
                 if (keyPoint.IsActive)
                 {
                     return keyPoint;
                 }
             }
             return null;
         }*/

        public List<KeyPoint> GetActiveKeyPointByTour(int tourId)
        {
            return _keyPointRepository.GetActiveKeyPointByTour(tourId);
        }
        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _keyPointRepository.SaveAll(keyPoints);
        }

    }
}

