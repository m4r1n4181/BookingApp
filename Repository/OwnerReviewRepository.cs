using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class OwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/owner-review.csv";
        private readonly Serializer<OwnerReview> _serializer;
        public List<OwnerReview> OwnerReviews;



        public OwnerReviewRepository()
        {
            _serializer = new Serializer<OwnerReview>();
            OwnerReviews = _serializer.FromCSV(FilePath);
        }


        public List<OwnerReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            OwnerReviews = _serializer.FromCSV(FilePath);
            if (OwnerReviews.Count < 1)
            {
                return 1;
            }
            return OwnerReviews.Max(a => a.Id) + 1;
        }



        public OwnerReview Save(OwnerReview ownerReview)
        {
            ownerReview.Id = NextId();
            OwnerReviews = _serializer.FromCSV(FilePath);
            OwnerReviews.Add(ownerReview);
            _serializer.ToCSV(FilePath, OwnerReviews);
            return ownerReview;
        }




    }
}
