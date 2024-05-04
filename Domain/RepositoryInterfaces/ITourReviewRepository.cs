using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        List<TourReview> GetAll();
        int NextId();
        TourReview Save(TourReview tourReview);
        List<TourReview> GetByTour(int tourId);
        TourReview Update(TourReview tourReview);
    }
}
