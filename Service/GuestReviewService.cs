using System;
using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class GuestReviewService
    {
        private GuestReviewRepository _guestReviewRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;

        public GuestReviewService()
        {
            _guestReviewRepository = new GuestReviewRepository();
        }
        public GuestReview RateGuest(GuestReview guestReview)
        {
            guestReview = _guestReviewRepository.Save(guestReview);
            return guestReview;
        }

        public void NotifyToRate(Owner owner)
        {

        }


    }
}
