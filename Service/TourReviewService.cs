using BookingApp.Model;
using BookingApp.Repository;
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
        }

        public TourReview RateTour(TourReview tourReview)
        {
            tourReview = _tourReviewRepository.Save(tourReview);
            return tourReview;
        }
    }
}