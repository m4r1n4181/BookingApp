using BookingApp.Model;
using BookingApp.Service;
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

        public TourReviewController(TourReviewService tourReviewService)
        {
            _tourReviewService = tourReviewService;
        }

        public void RateTour(TourReview tourReview)
        {
            _tourReviewService.RateTour(tourReview);
        }
    }
}