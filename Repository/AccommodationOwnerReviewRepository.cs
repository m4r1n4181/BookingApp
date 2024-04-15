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

        // private ImageRepository _imageRepository;
       private static AccommodationOwnerReviewRepository instance = null;

        private List<AccommodationOwnerReview> _accommodationOwnerReviews;
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
          //  _imageRepository = ImageRepository.GetInstance();
            _accommodationOwnerReviews = _serializer.FromCSV(FilePath);
        }
        public void BindAccommodationOwnerReviewWithAccommodationReservation()
        {
            foreach (AccommodationOwnerReview accommodationOwnerReview in _accommodationOwnerReviews)
            {
                int accommodationReservationId = accommodationOwnerReview.Reservation.Id;
                AccommodationReservation reservation = AccommodationReservationRepository.GetInstance().Get(accommodationReservationId);
                if (reservation != null)
                {
                    accommodationOwnerReview.Reservation = reservation;
                   // reservation.AccommodationReview = accommodationOwnerReview;
                }
                else
                {
                    Console.WriteLine("Error in accommodationOwnerReviewAccommodationReservation binding");
                }
            }
        }


        public List<AccommodationOwnerReview> GetAll()
        {
            BindAccommodationOwnerReviewWithAccommodationReservation();
            return _serializer.FromCSV(FilePath);
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
        public void Delete(AccommodationOwnerReview accommodationOwnerReview)
        {
            AccommodationOwnerReview founded = _accommodationOwnerReviews.Find(aor => aor.Id == accommodationOwnerReview.Id);
            _accommodationOwnerReviews.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
        }

        public AccommodationOwnerReview Update(AccommodationOwnerReview accommodationOwnerReview)
        {
            AccommodationOwnerReview current = _accommodationOwnerReviews.Find(aor => aor.Id == accommodationOwnerReview.Id);
            int index = _accommodationOwnerReviews.IndexOf(current);
            _accommodationOwnerReviews.Remove(current);
            _accommodationOwnerReviews.Insert(index, accommodationOwnerReview);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }

        public List<AccommodationOwnerReview> GetByReservation(int reservationId)
        {
            return _accommodationOwnerReviews.FindAll(aor => aor.Reservation.Id == reservationId);
        }
    }

      /*  public AccommodationOwnerReview SaveImages(AccommodationOwnerReview accommodationOwnerReview)
        {
            accommodationOwnerReview.Id = NextId();

            foreach (Image image in accommodationOwnerReview.Images)
            {
                image.ResourceId = accommodationOwnerReview.Id;
                _imageRepository.Save(image);
            }
      
            _accommodationOwnerReviews.Add(accommodationOwnerReview);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }
    }*/
}
