using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Model.Enums;
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
        private ITourRepository _tourRepository;
        private ITourGuideRepository _tourGuideRepository;
        public TourReviewService()
        {
            _tourReviewRepository = Injector.CreateInstance<ITourReviewRepository>();
            _touristEntryRepository = Injector.CreateInstance<ITouristEntryRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourGuideRepository = Injector.CreateInstance<ITourGuideRepository>();


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

        public List<Tour> GetFinishedToursForGuideAndLanguageInLastYear(TourGuide guide, string language)
        {
            DateTime lastYear = DateTime.Now.AddYears(-1); // Poslednjih godinu dana 

            // Dobavljanje svih rezervacija tura za određenog vodiča, na određenom jeziku, u poslednjoj godini
            List<TourReservation> tourReservations = _tourReservationRepository.GetByGuideAndLanguage(guide.Id, language, lastYear);

            // Izdvajanje završenih tura koje su se desile u poslednjih godinu dana sa statusom "finished"
            List<Tour> finishedToursInLastYear = tourReservations
                .Where(reservation => reservation.Tour != null && reservation.Tour.StartDate >= lastYear && reservation.Tour.TourStatus == TourStatusType.finished)
                .Select(reservation => reservation.Tour)
                .Distinct() // Da izbegnemo duplikate ako je ista tura rezervisana više puta
                .ToList();

            return finishedToursInLastYear;
        }

        public void UpdateSuperGuideStatus(TourGuide guide, string language)
        {
            List<Tour> finishedTours = GetFinishedToursForGuideAndLanguageInLastYear(guide, language);

            // Provera da li ima bar 20 vođenih tura na tom jeziku u poslednjih godinu dana
            if (finishedTours.Count >= 20)
            {
                // Računanje prosečne ocene tura
                double averageRating = CalculateAverageRating(finishedTours);

                // Provera da li je prosečna ocena iznad 4.0
                if (averageRating > 4.0)
                {
                    guide.IsSuperGuide = true;
                }
                else
                {
                    guide.IsSuperGuide = false;
                }
            }
            else
            {
                guide.IsSuperGuide = false;
            }

            // Ažuriranje statusa super-vodiča u bazi podataka
            _tourGuideRepository.Update(guide);
        }

        private double CalculateAverageRating(List<Tour> tours)
        {
            // Lista za čuvanje svih ocena tura
            List<double> allRatings = new List<double>();

            // Iteracija kroz sve ture
            foreach (var tour in tours)
            {
                // Dobavljanje svih recenzija za datu turu
                List<TourReview> reviewsForTour = _tourReviewRepository.GetByTour(tour.Id);

                // Računanje prosečne ocene za tu turu
                double tourAverageRating = reviewsForTour.Average(review => review.TourAppeal);

                // Dodavanje prosečne ocene u listu svih ocena
                allRatings.Add(tourAverageRating);
            }

            // Računanje prosečne ocene svih tura
            double totalRating = allRatings.Sum();
            double averageRating = totalRating / allRatings.Count;
            return averageRating;
        }

    }
}