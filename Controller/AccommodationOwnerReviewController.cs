using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class AccommodationOwnerReviewController
    {
        private AccommodationOwnerReviewService _accommodationOwnerReviewService;

        public AccommodationOwnerReviewController()
        {
            _accommodationOwnerReviewService = new AccommodationOwnerReviewService();
        }

        public List<AccommodationOwnerReview> GetAll()
        {
            return _accommodationOwnerReviewService.GetAll();
        }

        public AccommodationOwnerReview Get(int id)
        {
            return _accommodationOwnerReviewService.Get(id);
        }    
        public List<AccommodationOwnerReview> GetAllValidReviewsForOwner(int id)
        {
            return _accommodationOwnerReviewService.GetAllValidReviewsForOwner(id);
        }
   
        public bool IsSuperOwner(int ownerId)
        {
            return _accommodationOwnerReviewService.IsSuperOwner(ownerId);
        }

    }
}
