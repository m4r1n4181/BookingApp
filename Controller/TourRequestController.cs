using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using System;
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
            return _tourRequestService.GetTourRequestsForTourGuide(tourGuideId,  start, end);
        }

    }
}
