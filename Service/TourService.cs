﻿using BookingApp.Controller;
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
        private TourReservationService _tourReservationService;

        private TourReservationRepository _tourReservationRepository;
       
        public TourService()
        {
            _tourRepository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            _touristRepository = new TouristRepository(); // Initialize tourist repository
            _entryRepository = new TouristEntryRepository(); // Initialize tourist entry repository
            _tourReservationService = new TourReservationService();
            _tourReservationRepository = new TourReservationRepository();
        }


        public void CreateTour(Tour tour, List<DateTime> dateTimes, List<KeyPoint> keyPoints)
        {
            foreach(DateTime dateTime in dateTimes)
            {
                tour.StartDate = dateTime;
                tour = _tourRepository.Save(tour);

                keyPoints.ForEach(kp => kp.Tour = tour);

                _keyPointRepository.SaveAll(keyPoints);
            }

        }

        public List<Tour> GetTodayTours()
        {
            return _tourRepository.GetTodayTours();
        }

        public void StartTour(int id)
        {
            Tour tour = _tourRepository.GetById(id);
            if (tour == null || tour.TourStatus == Model.Enums.TourStatusType.started)
            {
                return;
            }

            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(id);
            if (keyPoints.Count == 0)
            {
                return;
            }

            //tour.IsStarted = true;
             tour.TourStatus = Model.Enums.TourStatusType.started;
            _tourRepository.Update(tour);

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
            if (tour == null || tour.TourStatus == Model.Enums.TourStatusType.not_started)
            {
                return;
            }
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(id);

            foreach (KeyPoint keyPoint in keyPoints)

            {
                keyPoint.IsActive = false;
                _keyPointRepository.Update(keyPoint);
            }
            tour.TourStatus = Model.Enums.TourStatusType.not_started;
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

        public List<Tour> GetAllWithDateTime()
        {
            return _tourRepository.GetAllWithDateTime();
        }
        public List<Tour> GetTourForNow()
        {
            List<Tour> _tourForNow = new List<Tour>();
            List<Tour> allTours = _tourRepository.GetAllWithLocations();
            _tourRepository.BindLocations();

            foreach (Tour tour in allTours)
            {
                if (tour.StartDate == DateTime.Today)
                {
                    _tourForNow.Add(tour);
                }
            }
            return _tourForNow;
        }

        public List<Tour> GetTourInFuture()
        {
            List<Tour> _tourInFuture = new List<Tour>();
            List<Tour> allTours = _tourRepository.GetAllWithLocations();
            
            foreach (Tour tour in allTours)
            {
                if (tour.StartDate > DateTime.Today.AddDays(2) && tour.TourStatus == Model.Enums.TourStatusType.not_started) // 48 hours before the tour starts
                {
                    _tourInFuture.Add(tour);
                }
            }
            return _tourInFuture;
        }

        public List<Tour> GetAllToursForTourGuide(int tourId)
        {

            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                if (tour.TourGuide.Id == tourId)
                {
                    tours.Add(tour);
                }
            }
            return tours;

        }

        public List<TourParticipants> GetAllParticipantsForTour(int tourId)
        {
            List<TourParticipants> tourParticipants = new List<TourParticipants>();

            List<TourReservation> reservations = new List<TourReservation>();
            List<TouristEntry> touristEntries = new List<TouristEntry>();
            List<KeyPoint> keyPoints = _keyPointRepository.GetKeyPointsForTour(tourId);
            keyPoints.ForEach(kp => touristEntries.AddRange(_entryRepository.GetAllByKeyPoint(kp.Id)));

            touristEntries.ForEach(te => reservations.Add(_tourReservationRepository.GetByTourAndTourist(tourId, te.Tourist.Id)));

            reservations.ForEach(r => tourParticipants.AddRange(r.Tourists));
            return tourParticipants;
        }

        public Tour MostVisitedTour(int year = -1)
        {
            Tour mostVisitedTour = null;
            int maxPeopleCame = -1;

            foreach (Tour tour in _tourRepository.GetAllFinished())
            {
                if (year == -1 || tour.StartDate.Year == year) // Provera da li je godina postavljena i da li datum odgovara godini
                {
                    int peopleCame = GetAllParticipantsForTour(tour.Id).Count();
                    if (peopleCame > maxPeopleCame)
                    {
                        mostVisitedTour = tour;
                        maxPeopleCame = peopleCame;
                    }
                }
            }
            return mostVisitedTour;
        }

        public TourAgeGroupStatistic GetAgeStatisticsForTour(int tourId)
        {
            TourAgeGroupStatistic tourAgeGroupStatistic = new TourAgeGroupStatistic(0, 0, 0);
           
            foreach (TourParticipants tourist in GetAllParticipantsForTour(tourId))
            {
                if (tourist.Age <= 18)
                {
                    tourAgeGroupStatistic.To18 += 1;
                }
                else if (tourist.Age > 18 && tourist.Age <= 50)
                {
                    tourAgeGroupStatistic.From18To50 += 1;
                }
                else
                {
                    tourAgeGroupStatistic.From50 += 1;
                }
            }
            
            return tourAgeGroupStatistic;
        }

        public List<int> YearForTour(int tourGuideId)
        {
            List<int> years = new List<int>();
            foreach (Tour tour in _tourRepository.GetAll())
            {
                if (tour.TourGuide.Id == tourGuideId)
                {
                    years.Add(tour.StartDate.Year);
                }
            }
            return years.Distinct().ToList();
        }



        public List<Tour> GetAllTour(int tourGuideId)
        {
            List<Tour> _allTour = new List<Tour>();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                if (tour.TourGuide.Id == tourGuideId)
                {
                    _allTour.Add(tour);
                }
            }
            return _allTour;
        }

        public List<Tour> GetAllActiveTours()
        {
            return _tourRepository.GetAllActiveTours();
        }

        public List<Tour> GetAlternativeTours(int locationId)
        {
            return _tourRepository.GetAlternativeTours(locationId);
        }

        public List<Tour> GetAllFinished()
        {
            return _tourRepository.GetAllFinished();
        }


    }
}