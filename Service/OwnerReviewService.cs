using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class OwnerReviewService
    {
        private OwnerReviewRepository _ownerReviewRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;

        public OwnerReviewService()
        {
            _ownerReviewRepository = new OwnerReviewRepository();
        }

        public OwnerReview RateOwner(OwnerReview ownerReview)
        {
            ownerReview = _ownerReviewRepository.Save(ownerReview);
            return ownerReview;
        }
    }
}
