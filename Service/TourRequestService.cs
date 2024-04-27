using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourRequestService 
    {
        private TourRequestRepository _tourRequestRepository;

        public TourRequestService()
        {
            _tourRequestRepository = new TourRequestRepository();
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


    }
}
