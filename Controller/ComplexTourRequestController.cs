using BookingApp.Domain.Models;
using BookingApp.Model.Enums;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class ComplexTourRequestController
    {
        private readonly ComplexTourRequestService _tourRequestService;

        public ComplexTourRequestController()
        {
            _tourRequestService = new ComplexTourRequestService();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }

        public ComplexTourRequest Get(int id)
        {
            return _tourRequestService.Get(id);
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return _tourRequestService.Save(complexTourRequest);
        }

        public void Delete(ComplexTourRequest complexTourRequest)
        {
            _tourRequestService.Delete(complexTourRequest);
        }

        public int NextId()
        {
            return _tourRequestService.NextId();
        }


        public ComplexTourRequest Update(ComplexTourRequest complexTourRequest)
        {
            return _tourRequestService.Update(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAllComplexTourRequestsForUser(int userId)
        {
            return _tourRequestService.GetAllComplexTourRequestsForUser(userId);
        }

        public List<TourRequest> GetSimpleTourRequestsForComplexRequest(int complexRequestId)
        {
            return _tourRequestService.GetSimpleTourRequestsForComplexRequest(complexRequestId);
        }
        public bool IsGuideAvailable(int guideId, DateTime date)
        {
            return _tourRequestService.IsGuideAvailable(guideId, date);
        }

        public List<DateTime> GetAvailableDatesForTourPart(int guideId, int tourRequestId)
        {
            return _tourRequestService.GetAvailableDatesForTourPart(guideId, tourRequestId);

        }

        public bool IsTourPartAlreadyAccepted(int guideId, ComplexTourRequest complexRequest)
        {
            return _tourRequestService.IsTourPartAlreadyAccepted(guideId, complexRequest);
        }

        public ComplexTourRequest AcceptRequest(int requestId, DateTime selectedDate)
        {
            return _tourRequestService.AcceptRequest(requestId, selectedDate);
        }

        public void DeclineRequest(int requestId)
        {
             _tourRequestService.DeclineRequest(requestId);
        }
    }
}