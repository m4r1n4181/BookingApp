using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        List<GuestReview> GetAll();
        GuestReview Save(GuestReview guestReview);
        int NextId();
        void Delete(GuestReview guestReview);
        GuestReview Update(GuestReview guestReview);
        List<GuestReview> GetGuestReviews(int id);
    }
}
