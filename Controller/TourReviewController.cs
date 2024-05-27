using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourReviewController
    {
        private TourReviewService _tourReviewService;

        public TourReviewController()
        {
            _tourReviewService = new TourReviewService();
        }

        public void RateTour(TourReview tourReview)
        {
            _tourReviewService.RateTour(tourReview);
        }

        public List<TourReview> GetByTour(int tourId)
        {
            return _tourReviewService.GetByTour(tourId);

        }

        public TourReview Update(TourReview tourReview)
        {
            return _tourReviewService.Update(tourReview);
        }
        public TourReviewController(TourReviewService tourReviewService)
        {
            _tourReviewService = tourReviewService;
        }

        public void UpdateSuperGuideStatus(TourGuide guide, string language)
        {
            _tourReviewService.UpdateSuperGuideStatus(guide, language);
        }
    }
}