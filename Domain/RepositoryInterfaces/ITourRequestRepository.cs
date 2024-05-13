using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest Save(TourRequest tourRequest);
        int NextId();
        void Delete(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);
        TourRequest GetById(int id);
        void BindTourRequestUser();
        List<TourRequest> GetAllWithUser();
        void BindTourRequestLocation();
        List<TourRequest> GetAllWithLocations();
        List<TourRequest> GetByTourist(int touristId);
        List<TourRequest> GetByTourGuide(int tourGuideId);
        List<TourRequest> SearchTourRequest(TourRequestSearch tourRequestSearch);
        List<string> GetUniqueLanguagesFromTourRequests();
        List<Location> GetUniqueLocationsFromTourRequests();
        List<int> GetUniqueYearsFromTourRequests();
        int CountRequestsByLocation(Location location);
        int CountRequestsByLanguage(string language);
        int CountRequestsByYear(int year);
        Dictionary<string, int> CountRequestsByYearAndMonth(int year);
        Location GetMostRequestedLocationLastYear();
        string GetMostRequestedLanguageLastYear();
    }
}
