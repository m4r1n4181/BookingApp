using System;
using System.Collections.Generic;
using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.Controller
{
    public class GuestReviewController
    {
        private GuestReviewService _guestReviewService;

        public GuestReviewController()
        {
            _guestReviewService = new GuestReviewService(Injector.CreateInstance<IGuestReviewRepository>(),Injector.CreateInstance<IOwnerReviewRepository>(), Injector.CreateInstance<IAccommodationReservationRepository>());
        }
        public void RateGuest(GuestReview guestReview)
        {
            _guestReviewService.RateGuest(guestReview);
        }
        public List<GuestReview> GetGuestReviews(int id) 
        {
            return _guestReviewService.GetGuestReviews(id);
        }

    }
}
