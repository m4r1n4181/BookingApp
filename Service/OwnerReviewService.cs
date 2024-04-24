using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
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
        private IOwnerReviewRepository _ownerReviewRepository;
        public IAccommodationReservationRepository _accommodationReservationRepository;

        public OwnerReviewService()
        {
            _ownerReviewRepository = Injector.CreateInstance<IOwnerReviewRepository>();
            //treba li u konstruktor ovo?
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public OwnerReview RateOwner(OwnerReview ownerReview)
        {
            ownerReview = _ownerReviewRepository.Save(ownerReview);
            return ownerReview;
        }
    }
}
