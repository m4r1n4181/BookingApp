using System;
using System.Collections.Generic;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class GuestReviewService
    {
        private IGuestReviewRepository _guestReviewRepository;
        private IOwnerReviewRepository _ownerReviewRepository;
        public IAccommodationReservationRepository _accommodationReservationRepository;

        public GuestReviewService(IGuestReviewRepository guestReviewRepository,IOwnerReviewRepository ownerReviewRepository,IAccommodationReservationRepository accommodationReservationRepository)
        {
            _guestReviewRepository = guestReviewRepository;
            _ownerReviewRepository = ownerReviewRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
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
