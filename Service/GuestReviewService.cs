using System;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class GuestReviewService
    {
       private GuestReviewRepository _guestReviewRepository;

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
