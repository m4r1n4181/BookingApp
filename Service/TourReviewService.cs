using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
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
        private ITourReviewRepository _tourReviewRepository;
        public ITourReservationRepository _tourReservationRepository;
        private ITouristEntryRepository _touristEntryRepository;
        public TourReviewService()
        {
            _tourReviewRepository = Injector.CreateInstance<ITourReviewRepository>();
            _touristEntryRepository = Injector.CreateInstance<ITouristEntryRepository>();
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