using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
   public interface IAccommodationOwnerReviewRepository
    {
        public List<AccommodationOwnerReview> GetAll();
        public AccommodationOwnerReview Get(int id);
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview);    
        public int NextId();
        public List<AccommodationOwnerReview> GetByReservation(int reservationId);
     //   public AccommodationOwnerReview SaveImages(AccommodationOwnerReview accommodationOwnerReview);
    }
}
