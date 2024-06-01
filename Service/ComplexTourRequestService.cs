using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class ComplexTourRequestService
    {
        private IComplexTourRequestRepository _complexTourRequestRepository;
        private ITourRequestRepository _simpleTourRequestRepository;
        private ITourGuideRepository _tourGuideRepository;
        private ITourRepository _tourRepository;
        private INotificationRepository _notificationRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = Injector.CreateInstance<IComplexTourRequestRepository>();
            _simpleTourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _tourGuideRepository = Injector.CreateInstance<ITourGuideRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();

        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestRepository.GetAll();
        }

        public ComplexTourRequest Get(int id)
        {
            return _complexTourRequestRepository.Get(id);
        }

        public ComplexTourRequest Save(ComplexTourRequest request)
        {
            return _complexTourRequestRepository.Save(request);
        }

        public ComplexTourRequest Update(ComplexTourRequest request)
        {
            return _complexTourRequestRepository.Update(request);
        }

        public void Delete(ComplexTourRequest request)
        {
            _complexTourRequestRepository.Delete(request);
        }

        public int NextId()
        {
            return _complexTourRequestRepository.NextId();
        }
        public List<ComplexTourRequest> GetAllComplexTourRequestsForUser(int userId)
        {
            List<ComplexTourRequest> myComplexRequests = new List<ComplexTourRequest>();
            var allComplexRequests = _complexTourRequestRepository.GetAll();

            for (int i = 0; i < allComplexRequests.Count(); i++)
            {
                var request = allComplexRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    List<TourRequest> simpleRequests = GetSimpleTourRequestsForComplexRequest(request.Id);
                    request.SimpleTourRequests = simpleRequests;

                    bool allAccepted = simpleRequests.All(simpleRequest => simpleRequest.RequestStatus == RequestStatusType.Approved);

                    bool allDeclined = simpleRequests.All(simpleRequest => simpleRequest.RequestStatus == RequestStatusType.Declined);

                    bool within48Hours = (simpleRequests[0].StartDate - DateTime.Today).TotalHours <= 48;

                    if (allAccepted)
                    {
                        request.Status = RequestStatusType.Approved;
                    }
                    else if (within48Hours && !allDeclined && request.Status == RequestStatusType.Standby)
                    {
                        request.Status = RequestStatusType.Declined;
                    }
                    else if (!allAccepted && !allDeclined)
                    {
                        request.Status = RequestStatusType.Standby;
                    }

                    _complexTourRequestRepository.Update(request);

                    if (!IsTourPartAlreadyAccepted(userId, request))
                    {
                        myComplexRequests.Add(request);
                    }
                }
            }

            return myComplexRequests;
        }


        public List<TourRequest> GetSimpleTourRequestsForComplexRequest(int complexRequestId)
        {
            return _simpleTourRequestRepository.GetAll()
                .Where(request => request.ComplexTourRequestId == complexRequestId)
                .ToList();
        }

        public bool IsGuideAvailable(int guideId, DateTime date)
        {
            var tours = _tourRepository.GetAllByTourGuideId(guideId);
            return !tours.Any(t => t.StartDate.Date == date.Date && t.TourStatus == TourStatusType.not_started);
        }

        public List<DateTime> GetAvailableDatesForTourPart(int guideId, int tourRequestId)
        {
            var tourRequest = _simpleTourRequestRepository.GetById(tourRequestId);
            if (tourRequest == null)
            {
                return new List<DateTime>();
            }

            var startDate = tourRequest.StartDate;
            var endDate = tourRequest.EndDate;
            var availableDates = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsGuideAvailable(guideId, date))
                {
                    availableDates.Add(date);
                }
            }

            var complexRequest = _complexTourRequestRepository.Get(tourRequest.ComplexTourRequestId);
            if (complexRequest != null)
            {
                var acceptedTourParts = complexRequest.SimpleTourRequests
                    .Where(r => r.RequestStatus == RequestStatusType.Approved && r.TourGuide.Id == guideId)
                    .ToList();

                availableDates = availableDates
                    .Where(date => !acceptedTourParts.Any(r => r.StartDate.Date == date.Date))
                    .ToList();
            }

            return availableDates;
        }

        public bool IsTourPartAlreadyAccepted(int guideId, ComplexTourRequest complexRequest)
        {
            var acceptedTourParts = complexRequest.SimpleTourRequests
                .Where(r => r.RequestStatus == RequestStatusType.Approved && r.TourGuide.Id == guideId)
                .ToList();

            return acceptedTourParts.Any();
        }

        public ComplexTourRequest AcceptRequest(int requestId, DateTime selectedDate)
        {
            var complexRequest = _complexTourRequestRepository.Get(requestId);
            if (complexRequest == null)
            {
                return null;
            }

            if (complexRequest.TourGuide == null || !IsGuideAvailable(complexRequest.TourGuide.Id, selectedDate))
            {
                return null;
            }

            complexRequest.Status = RequestStatusType.Approved;
            complexRequest.SelectedDate = selectedDate;
            _complexTourRequestRepository.Update(complexRequest);

            string message = "Vaš zahtev je odobren za datum " + selectedDate.ToShortDateString();
            var notification = new Notification
            {
                Message = message,
                NotificationStatus = NotificationStatus.unread,
                User = complexRequest.Tourist
            };
            _notificationRepository.Save(notification);

            return complexRequest;
        }

        public void DeclineRequest(int requestId)
        {
            var complexRequest = _complexTourRequestRepository.Get(requestId);
            if (complexRequest != null)
            {
                complexRequest.Status = RequestStatusType.Approved;
                _complexTourRequestRepository.Update(complexRequest);
            }
        }
    }
}

