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
        private readonly AccommodationOwnerReviewService _accommodationOwnerReviewService;

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
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview)
        {
            return _accommodationOwnerReviewService.Save(accommodationOwnerReview);
        }
        public void Delete(AccommodationOwnerReview accommodationOwnerReview)
        {
            _accommodationOwnerReviewService.Delete(accommodationOwnerReview);
        }
        public void Update(AccommodationOwnerReview accommodationOwnerReview)
        {
            _accommodationOwnerReviewService.Update(accommodationOwnerReview);
        }
        /*
        public AccommodationOwnerReview SaveImages(AccommodationOwnerReview accommodationOwnerReview)
        {
            return _accommodationOwnerReviewService.SaveImages(accommodationOwnerReview);
        }
        */
        public List<AccommodationOwnerReview> GetAllValidReviews(Accommodation accommodation)
        {
            return _accommodationOwnerReviewService.GetAllValidReviews(accommodation);
        }
        public List<AccommodationOwnerReview> GetAllReviewsTest(int id)
        {
            return _accommodationOwnerReviewService.GetAllReviewsTest(id);
        }
        public List<AccommodationOwnerReview> GetAllValidReviewsForOwner(User owner)
        {
            return _accommodationOwnerReviewService.GetAllValidReviewsForOwner(owner);
        }
        public bool IsSuperOwner(int ownerId)
        {
            return _accommodationOwnerReviewService.IsSuperOwner(ownerId);
        }

    }
}
