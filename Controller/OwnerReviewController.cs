using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class OwnerReviewController
    {
        private OwnerReviewService _ownerReviewService;

        public OwnerReviewController(OwnerReviewService ownerReviewService)
        {
            _ownerReviewService = ownerReviewService;
        }

        public void RateOwner(OwnerReview ownerReview)
        {
            _ownerReviewService.RateOwner(ownerReview);
        }
    }
}
