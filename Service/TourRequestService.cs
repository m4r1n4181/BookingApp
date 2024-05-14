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
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
        private ILocationRepository _locationRepository;
        private INotificationRepository _notificationRepository;
        private IUserRepository _userRepository;


        public TourRequestService()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
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

        public void CreateTourFromRequest(List<DateTime> dateTimes, List<KeyPoint> keyPoints, string name, Location location, string description, int maxTourists, int duration, List<string> pictures)
        {
            //Location location = GetMostRequestedLocationLastYear();
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
        
        public bool IsTourGuideFreeOnDate(int tourGuideId, DateTime date)
        {
            List<Tour> guideTours = _tourRepository.GetByTourGuideNotStarted(tourGuideId);
            foreach(Tour tour in guideTours)
            {
                if(tour.StartDate.Date == date.Date)
                {
                    return false;
                }
            }
            return true;
        }

        public TourRequest ApproveRequest(int tourRequestId, DateTime selectedDate)
        {
            TourRequest tourRequest = GetById(tourRequestId);
            if (tourRequestId == null)
            {
                return null;
            }
            if (!IsTourGuideFreeOnDate(tourRequest.TourGuide.Id, selectedDate))
            {
                return null;
            }

            tourRequest.RequestStatus = RequestStatusType.Approved;
            tourRequest.SelectedDate = selectedDate;
            _tourRequestRepository.Update(tourRequest);
            TourParticipants tourParticipant = tourRequest.Tourists.FirstOrDefault(tp => tp.UserId != -1);
            if (tourParticipant != null)
            {
                int userId = tourParticipant.UserId;
                User user = _userRepository.GetById(userId);
                if (user != null)
                {
                    string message = "Vaš zahtev je odobren za datum " + selectedDate.ToShortDateString();
                    Notification notification = new Notification()
                    {
                        Message = message,
                        NotificationStatus = NotificationStatus.unread,
                        User = user
                    };
                    _notificationRepository.Save(notification);
                }
            }


            return tourRequest;
        }
        public TourRequest DeclineRequest(int tourRequestId)
        {
            TourRequest tourRequest = GetById(tourRequestId);
            if (tourRequest == null)
            {
                return null;
            }
            tourRequest.RequestStatus = RequestStatusType.Declined;
            _tourRequestRepository.Update(tourRequest);
            TourParticipants tourParticipant = tourRequest.Tourists.FirstOrDefault(tp => tp.UserId != -1);
            if (tourParticipant != null)
            {
                int userId = tourParticipant.UserId;
                User user = _userRepository.GetById(userId);
                if (user != null)
                {
                    string message = "Vaš zahtev je odbijen jer nema dostupnih termina";
                    Notification notification = new Notification()
                    {
                        Message = message,
                        NotificationStatus = NotificationStatus.unread,
                        User = user
                    };
                    _notificationRepository.Save(notification);
                }
            }

            return tourRequest;
        }

        public void CreateTourRequest(Location location, string language, int maxTourists, string description, DateTime startDate, DateTime endDate)
        {
            TourRequest tourRequest = new TourRequest
            {
                Id = _tourRequestRepository.NextId(),
                Location = location,
                Language = language,
                MaxTourists = maxTourists,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Tourists = new List<TourParticipants>(),
                TourGuide = null,
                RequestStatus = RequestStatusType.Standby
            };

            tourRequest = _tourRequestRepository.Save(tourRequest);
        }



    }
}
