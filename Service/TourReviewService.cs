using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourReviewService
    {
        private TourReviewRepository _tourReviewRepository;
        public TourReservationRepository _tourReservationRepository;

        public TourReviewService()
        {
            _tourReviewRepository = new TourReviewRepository();
            _touristEntryRepository = new TouristEntryRepository();
        }

        public TourReview RateTour(TourReview tourReview)
        {
            tourReview = _tourReviewRepository.Save(tourReview);
            return tourReview;
        }

        public List<TourReview> GetByTour(int tourId)
        {
            List<TourReview> reviews = _tourReviewRepository.GetByTour(tourId);
            foreach(TourReview tourReview in reviews)
            {
                TourReservation reservation = tourReview.TourReservation;
                TouristEntry touristEntry = _touristEntryRepository.GetByTourAndTourist(reservation.Tour.Id, reservation.Tourist.Id);
                reservation.TouristEntry = touristEntry;
                tourReview.TourReservation = reservation;
            }
            return reviews;

        }

        public TourReview Update(TourReview tourReview)
        {
            return _tourReviewRepository.Update(tourReview);
        }

    }
}