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

        public List<TourReview> GetAllTourGuideReviews(int tourGuideId, int touristId)

        {

            List<TourReview> tourGuideReviews = new List<TourReview>();

            foreach (TourReview tourReview in _tourReviewRepository.GetAll())
            {
                if (tourReview.TourReservation.TouristEntry.Id == touristId && tourReview.TourReservation.Tour.TourGuide.Id == tourGuideId)
                {
                    tourGuideReviews.Add(tourReview);
                }
            }
            return tourGuideReviews;

        }
    }
}
