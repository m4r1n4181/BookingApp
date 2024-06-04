using BookingApp.Service;
using BookingApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.DTO;
using System;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model.Enums;
namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }

        public void EndTour(int id)
        {
            _tourService.EndTour(id);
        }

        public void CreateTour(Tour tour, List<DateTime> dateTimes, List<KeyPoint> keyPoints)
        {
            _tourService.CreateTour(tour, dateTimes, keyPoints);
        }
        public List<Tour> GetAllWithDateTime()
        {
            return _tourService.GetAllWithDateTime();
        }
        
        public List<Tour> GetAllWithLocations()
        {
            return _tourService.GetAllWithLocations();
        }

        public List<Tour> GetTodayTours()
        {
            return _tourService.GetTodayTours();
        }
        public List<Tour> GetThisWeeksMondayTours()
        {
            return _tourService.GetThisWeeksMondayTours();
        }

        public void StartTour(int id)
        {
            _tourService.StartTour(id);
        }

        public List<Tour> SearchTours(TourSearchParams searchParams)
        {
            return _tourService.SearchTours(searchParams);
        }

        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }

        public List<Tour> GetTourInFuture()
        {
            return _tourService.GetTourInFuture();
        }

        public List<Tour> GetTourForNow()
        {
            return _tourService.GetTourForNow();
        }

        public List<Tour> GetAllToursForTourGuide(int tourId)
        {
            return _tourService.GetAllToursForTourGuide(tourId);
        }

        public List<int> YearForTour(int tourGuideId)
        {
            return _tourService.YearForTour(tourGuideId);
        }

        public Tour MostVisitedTour(int year = -1)
        {
            return _tourService.MostVisitedTour(year);
        }

        public List<Tour> GetAllTour(int tourGuideId)
        {
            return _tourService.GetAllTour(tourGuideId);
        }

        public List<Tour> GetAllActiveTours()
        {
            return _tourService.GetAllActiveTours();
        }

        public TourAgeGroupStatistic GetAgeStatisticsForTour(int tourId)
        {
            return _tourService.GetAgeStatisticsForTour(tourId);
        }

        public List<Tour> GetAllFinished()
        {
            return _tourService.GetAllFinished();
        }

        public List<Tour> GetAlternativeTours(int id)
        {
            return _tourService.GetAlternativeTours(id);
        }

        public List<Tour> GetFutureTours()
        {
            return _tourService.GetFutureTours();
        }

        public List<Tour> SearchTourForTourGuide(TourGuideSearch tourGuideSearch)
        {
            return _tourService.SearchTourForTourGuide(tourGuideSearch);

        }

        public List<Tour> GetFutureToursByGuideId(int guideId)
        {
            return _tourService.GetFutureToursByGuideId(guideId);

        }

        public List<string> GetUniqueLanguagesFromFinishedToursInLastYear(TourGuide guide)
        {
            return _tourService.GetUniqueLanguagesFromFinishedToursInLastYear(guide);
        }


    }


}