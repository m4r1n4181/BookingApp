using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IOwnerReviewRepository
    {
        public List<OwnerReview> GetAll();
        public OwnerReview Save(OwnerReview ownerReview);

        public OwnerReview GetOneByReservation(int reservationId);

    }
}
