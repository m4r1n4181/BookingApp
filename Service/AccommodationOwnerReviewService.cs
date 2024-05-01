using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class AccommodationOwnerReviewService
    {
        public AccommodationOwnerReviewRepository _accommodationOwnerReviewRepository;

        public AccommodationReservationRepository _accommodationReservationRepository;
        public RenovatingRequestRepository _renovationRequestRepository;

        public AccommodationOwnerReviewService()
        {
            _accommodationOwnerReviewRepository = new AccommodationOwnerReviewRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _renovationRequestRepository = new RenovatingRequestRepository();
        }
        public List<AccommodationOwnerReview> GetAll()
        {
            return _accommodationOwnerReviewRepository.GetAll();
        }

        public AccommodationOwnerReview Get(int id)
        {
            return _accommodationOwnerReviewRepository.Get(id);
        }
       
        public List<AccommodationOwnerReview> GetByReservation(AccommodationOwnerReview accommodationOwnerReview)
        {
            return _accommodationOwnerReviewRepository.GetByReservation(accommodationOwnerReview.Reservation.Id);
        }

        public bool isValidReview(AccommodationReservation reservation)
        {
                return reservation.GuestReview.Id != -1 && reservation.AccommodationReview.Id != -1; 
        }
        public List<AccommodationOwnerReview> GetAllValidReviewsForOwner(int ownerId)
        {
            List<AccommodationOwnerReview> reviews = new List<AccommodationOwnerReview>();
            foreach(AccommodationOwnerReview accommodationOwnerReview in _accommodationOwnerReviewRepository.GetAll())
            {
                AccommodationReservation reservation = _accommodationReservationRepository.GetById(accommodationOwnerReview.Reservation.Id);
                if (reservation.Accommodation.Owner.Id == ownerId)
                {
                    if (isValidReview(reservation))
                    {
                        reviews.Add(accommodationOwnerReview);
                    }
                   
                }
            }
            return reviews;
        }
        public int GetReviewsCountForOwner(int ownerId)
        {
            int count = 0;
            foreach (AccommodationOwnerReview ownerReview in _accommodationOwnerReviewRepository.GetAll())
            {
                if (ownerReview.Reservation.Accommodation.Owner.Id == ownerId)
                {
                    count++;
                }
            }
            return count;
        }

        public double GetReviewsAverageForOwner(int ownerId)
        {
            int count = 0;
            double averageReview = 0;
            foreach (AccommodationOwnerReview ownerReview in _accommodationOwnerReviewRepository.GetAll())
            {
                if (ownerReview.Reservation.Accommodation.Owner.Id == ownerId)
                {
                    count++;
                    averageReview += (ownerReview.Cleanliness + ownerReview.Correctness) / 2;

                }
            }
            return averageReview / count;
        }
        public bool IsSuperOwner(int ownerId)
        {
            int count = GetReviewsCountForOwner(ownerId);
            double average = GetReviewsAverageForOwner(ownerId);
            return count >= 50 && average >= 4.5; 
        }
        public bool IsReservationWithRenovationRecommendations(AccommodationReservation reservation)
        {
            List<AccommodationOwnerReview> reviews = _accommodationOwnerReviewRepository.GetAll();
            foreach (AccommodationOwnerReview review in reviews)
            {
                foreach (RenovatingRequest renovationRequest in _renovationRequestRepository.GetAll())
                {
                    if (renovationRequest.AccommodationReservation.AccommodationReview.Id == review.Id)
                    {
                        review.RenovatingRequest.Add(renovationRequest);
                    }
                }

            }
            foreach (AccommodationOwnerReview review in reviews)
            {
                if (review.Reservation.Id == reservation.Id && review.RenovatingRequest.Any())
                {
                    return true;
                }
            }
            return false;
        }


    }
}
