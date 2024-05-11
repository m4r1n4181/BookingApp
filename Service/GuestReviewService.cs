using System;
using System.Collections.Generic;
using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class GuestReviewService
    {
        private IGuestReviewRepository _guestReviewRepository;
        private IOwnerReviewRepository _ownerReviewRepository;

        public GuestReviewService()
        {
            _guestReviewRepository = Injector.CreateInstance<IGuestReviewRepository>();
            _ownerReviewRepository = Injector.CreateInstance<IOwnerReviewRepository>();
        }
        public GuestReview RateGuest(GuestReview guestReview)
        {
            guestReview = _guestReviewRepository.Save(guestReview);
            return guestReview;
        }

        public void NotifyToRate(Owner owner)
        {

        }

        public List<GuestReview> GetGuestReviews(int id)
        { 
            //za svaku od guestReview proveri da li postoji OwnerReview za tu rezervaciju
            List<GuestReview> guestReviews = _guestReviewRepository.GetGuestReviews(id);
            List<GuestReview> reviewsWithOwnerReview =  new List<GuestReview>();

            foreach (GuestReview guestReview in guestReviews)
            {
                bool ownerReviewExists = _ownerReviewRepository.CheckOwnerReviewExistence(guestReview.AccommodationReservation.Id);
                if (ownerReviewExists)
                {
                    reviewsWithOwnerReview.Add(guestReview);
                }
            }
           return reviewsWithOwnerReview;
        }


    }
}
