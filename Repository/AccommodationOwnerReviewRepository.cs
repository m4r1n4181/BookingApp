using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationOwnerReviewRepository : IAccommodationOwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationOwnerReviews.csv";
        private readonly Serializer<AccommodationOwnerReview> _serializer;

        private static AccommodationOwnerReviewRepository instance = null;
        public List<AccommodationOwnerReview> _accommodationOwnerReviews;
        public static AccommodationOwnerReviewRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new AccommodationOwnerReviewRepository();
            }
            return instance;
        }
        public AccommodationOwnerReviewRepository()
        {
            _serializer = new Serializer<AccommodationOwnerReview>();
            _accommodationOwnerReviews = _serializer.FromCSV(FilePath);
        }

        public void BindAccommodationReservation() //review sa rezervacijom
        {
            AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();   
            foreach(var accommodationOwnerReview in _accommodationOwnerReviews)
            {
                accommodationOwnerReview.Reservation = accommodationReservationRepository.GetById(accommodationOwnerReview.Reservation.Id);
            }
        }


        public List<AccommodationOwnerReview> GetAll() //mora za ownera ovako ne menjaj!!!!!!!!!!!!!!!!!!!!
        {
            _accommodationOwnerReviews = _serializer.FromCSV(FilePath);        
            BindAccommodationReservation();
            return _accommodationOwnerReviews;
        }

        public AccommodationOwnerReview Get(int id)
        {
            return _accommodationOwnerReviews.Find(aor => aor.Id == id);
        }
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview)
        {
            accommodationOwnerReview.Id = NextId();
            _accommodationOwnerReviews.Add(accommodationOwnerReview);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }
        public int NextId()
        {
            if (_accommodationOwnerReviews.Count < 1)
            {
                return 1;
            }
            return _accommodationOwnerReviews.Max(aor => aor.Id) + 1;
        }


        public List<AccommodationOwnerReview> GetByReservation(int reservationId)
        {
            _accommodationOwnerReviews = _serializer.FromCSV(FilePath);
             BindAccommodationReservation();
            return _accommodationOwnerReviews.FindAll(aor => aor.Reservation.Id == reservationId);
        }
    }

}
