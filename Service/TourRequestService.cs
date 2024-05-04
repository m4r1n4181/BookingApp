using BookingApp.Controller;
using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Service
{
    public class TourRequestService 
    {
        private ITourRequestRepository _tourRequestRepository;
        private ITourRepository _tourRepository;
        private IKeyPointRepository _keyPointRepository;



        public TourRequestService()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequestRepository.Delete(tourRequest);
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public List<TourRequest> GetAllWithLocations()
        {
            return _tourRequestRepository.GetAllWithLocations();
        }

        public List<TourRequest> SearchTourRequest(TourRequestSearch tourRequestSearch)
        {
            return _tourRequestRepository.SearchTourRequest(tourRequestSearch);
        }

        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }

         public List<TourRequest> GetTourRequestsForTourGuide(int tourGuideId, DateTime start, DateTime end)
          {
              List<TourRequest> tourRequest = _tourRequestRepository.GetByTourGuide(tourGuideId);

              return tourRequest.FindAll(tR => DatesIntertwine(tR.StartDate, tR.EndDate, start, end));
          }

        public void SaveTourFromRequest(TourRequest tourRequest, DateTime selectedDate)
        {
            Tour tour = new Tour
            {
                Location = tourRequest.Location,
                Language = tourRequest.Language,
                MaxTourists = tourRequest.MaxTourists,
                Description = tourRequest.Description,
                StartDate = selectedDate,
                TourGuide = tourRequest.TourGuide,
                Name = tourRequest.Description, 
                Duration = 2, 
                AvailableSeats = tourRequest.MaxTourists, 
                Pictures = null, 
                TourStatus = TourStatusType.not_started 
            };

            _tourRepository.Save(tour);
        }

        public List<string> GetUniqueLanguagesFromTourRequests()
        {
            return _tourRequestRepository.GetUniqueLanguagesFromTourRequests();
        }

        public List<Location> GetUniqueLocationsFromTourRequests()
        {
            return _tourRequestRepository.GetUniqueLocationsFromTourRequests();
        }

        public List<int> GetUniqueYearsFromTourRequests()
        {
            return _tourRequestRepository.GetUniqueYearsFromTourRequests();
        }

        public int CountRequestsByLocation(Location location)
        {
            return _tourRequestRepository.CountRequestsByLocation(location);
        }

        public int CountRequestsByLanguage(string language)
        {
            return _tourRequestRepository.CountRequestsByLanguage(language);
        }

        public int CountRequestsByYear(int year)
        {
            return _tourRequestRepository.CountRequestsByYear(year);
        }

        public Dictionary<string, int> CountRequestsByYearAndMonth(int year)
        {
            return _tourRequestRepository.CountRequestsByYearAndMonth(year);
        }

        public Location GetMostRequestedLocationLastYear()
        {
            return _tourRequestRepository.GetMostRequestedLocationLastYear();
        }

        public string GetMostRequestedLanguageLastYear()
        {
            return _tourRequestRepository.GetMostRequestedLanguageLastYear();
        }

        public void CreateTourFromRequest(List<DateTime> dateTimes, List<KeyPoint> keyPoints, string name, string description, int maxTourists, int duration, List<string> pictures)
        {
            Location location = GetMostRequestedLocationLastYear();
            string language = GetMostRequestedLanguageLastYear();

            foreach (DateTime dateTime in dateTimes)
            {
                Tour tour = new Tour
                {
                    Name = name,
                    TourGuide = SignInForm.LoggedUser,
                    Description = description,
                    Location = location,
                    Language = language, 
                    MaxTourists = maxTourists,
                    AvailableSeats = maxTourists,
                    Duration = duration,
                    StartDate = dateTime,
                    Pictures = pictures,
                };

                tour = _tourRepository.Save(tour);

                keyPoints.ForEach(kp => kp.Tour = tour);

                _keyPointRepository.SaveAll(keyPoints);
            }
        }
    }
}
