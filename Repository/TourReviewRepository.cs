using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-review.csv";
        private readonly Serializer<TourReview> _serializer;
        public List<TourReview> TourReviews;



        public TourReviewRepository()
        {
            _serializer = new Serializer<TourReview>();
            TourReviews = _serializer.FromCSV(FilePath);
        }


        public List<TourReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            TourReviews = _serializer.FromCSV(FilePath);
            if (TourReviews.Count < 1)
            {
                return 1;
            }
            return TourReviews.Max(a => a.Id) + 1;
        }



        public TourReview Save(TourReview tourReview)
        {
            tourReview.Id = NextId();
            TourReviews = _serializer.FromCSV(FilePath);
            TourReviews.Add(tourReview);
            _serializer.ToCSV(FilePath, TourReviews);
            return tourReview;
        }

       
        public List<TourReview> GetByTour(int tourId)
        {
            TourReviews = _serializer.FromCSV(FilePath);
            BindReservation();
            return TourReviews.FindAll(tr => tr.TourReservation.Tour.Id == tourId);
        }

        public void BindReservation()//reservation sa review
        {
            TourReservationRepository tourReservationRepository = new TourReservationRepository();
            TourReviews.ForEach(tr => tr.TourReservation = tourReservationRepository.GetById(tr.TourReservation.Id));
        }
    }
}