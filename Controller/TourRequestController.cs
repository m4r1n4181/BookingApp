using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourRequestController
    {
        private readonly TourRequestService _tourRequestService;

        public TourRequestController()
        {
            _tourRequestService = new TourRequestService();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestService.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestService.Update(tourRequest);
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequestService.Delete(tourRequest);
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestService.GetById(id);
        }

        public List<TourRequest> GetAllWithLocations()
        {
            return _tourRequestService.GetAllWithLocations();
        }

        public List<TourRequest> SearchTourRequest(TourRequestSearch tourRequestSearch)
        {
            return _tourRequestService.SearchTourRequest(tourRequestSearch);
        }

        public List<TourRequest> GetTourRequestsForTourGuide(int tourGuideId, DateTime start, DateTime end)
        {
            return _tourRequestService.GetTourRequestsForTourGuide(tourGuideId, start, end);
        }

        public void SaveTourFromRequest(TourRequest tourRequest, DateTime selectedDate)
        {
            _tourRequestService.SaveTourFromRequest(tourRequest, selectedDate);
        }

        public List<Location> GetUniqueLocationsFromTourRequests()
        {
            return _tourRequestService.GetUniqueLocationsFromTourRequests();

        }

        public List<string> GetUniqueLanguagesFromTourRequests()
        {
            return _tourRequestService.GetUniqueLanguagesFromTourRequests();
        }

        public List<int> GetUniqueYearsFromTourRequests()
        {
            return _tourRequestService.GetUniqueYearsFromTourRequests();

        }

        public int CountRequestsByLocation(Location location)
        {
            return _tourRequestService.CountRequestsByLocation(location);

        }

        public int CountRequestsByLanguage(string language)
        {
            return _tourRequestService.CountRequestsByLanguage(language);

        }

        public int CountRequestsByYear(int year)
        {
            return _tourRequestService.CountRequestsByYear(year);
        }

        public Dictionary<string, int> CountRequestsByYearAndMonth(int year)
        {
            return _tourRequestService.CountRequestsByYearAndMonth(year);
        }

        public Location GetMostRequestedLocationLastYear()
        {
            return _tourRequestService.GetMostRequestedLocationLastYear();
        }

        public string GetMostRequestedLanguageLastYear()
        {
            return _tourRequestService.GetMostRequestedLanguageLastYear();
        }

        public void CreateTourFromRequest( List<DateTime> dateTimes, List<KeyPoint> keyPoints)
        {
             _tourRequestService.CreateTourFromRequest( dateTimes, keyPoints);
        }
    }
}
