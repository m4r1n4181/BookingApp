using System;
using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.Controller
{
    public class GuestReviewController
    {
        private GuestReviewService _guestReviewService;

        public GuestReviewController()
        {
            _guestReviewService = new GuestReviewService();
        }
        public void RateGuest(GuestReview guestReview)
        {
            _guestReviewService.RateGuest(guestReview);
        }

        public void NotifyToRate(Owner owner)
        {

        }

        public List<GuestReview> GetGuestReviews(int id) 
        {
            return _guestReviewService.GetGuestReviews(id);
        }

    }
}
