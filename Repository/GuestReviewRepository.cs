using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/guest-review.csv";
        private readonly Serializer<GuestReview> _serializer;
        public List<GuestReview> GuestReviews; //{ get; set; } = new List<Accommodation>();


        public GuestReviewRepository()
        {
            _serializer = new Serializer<GuestReview>();
            GuestReviews = _serializer.FromCSV(FilePath);
        }


        public List<GuestReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            guestReview.Id = NextId();
            GuestReviews = _serializer.FromCSV(FilePath);
            GuestReviews.Add(guestReview);
            _serializer.ToCSV(FilePath, GuestReviews);
            return guestReview;
        }

        public int NextId()
        {
            GuestReviews = _serializer.FromCSV(FilePath);
            if (GuestReviews.Count < 1)
            {
                return 1;
            }
            return GuestReviews.Max(a => a.Id) + 1;
        }

        public void Delete(GuestReview guestReview)
        {
            GuestReviews = _serializer.FromCSV(FilePath);
            GuestReview founded = GuestReviews.Find(c => c.Id == guestReview.Id);
            GuestReviews.Remove(founded);
            _serializer.ToCSV(FilePath, GuestReviews);
        }

        public GuestReview Update(GuestReview guestReview)
        {
            GuestReviews = _serializer.FromCSV(FilePath);
            GuestReview current = GuestReviews.Find(a => a.Id == guestReview.Id);
            int index = GuestReviews.IndexOf(current);
            GuestReviews.Remove(current);
            GuestReviews.Insert(index, guestReview);
            _serializer.ToCSV(FilePath, GuestReviews);
            return guestReview;
        }

    }
}
