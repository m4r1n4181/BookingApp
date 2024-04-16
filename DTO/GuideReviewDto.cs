using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class GuideReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameOfKeyPoint { get; set; }
        public int TourAppeal { get; set; }
        public int Knowledge { get; set; }
        public int Fluency { get; set; }
        public string Comment { get; set; }

        public bool Validity { get; set; }



        public GuideReviewDto(TourReview tourReview)
        {
            Id = tourReview.Id;
            Name = tourReview.TourReservation.Tour.Name;
            NameOfKeyPoint = tourReview.TourReservation.TouristEntry.KeyPoint.Name;
            TourAppeal = tourReview.TourAppeal;
            Knowledge = tourReview.Knowledge;
            Fluency = tourReview.Fluency;
            Comment = tourReview.Comment;
            Validity = tourReview.Validity;
        }

    }

}
