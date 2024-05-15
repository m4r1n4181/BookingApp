using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationOwnerReview : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int Cleanliness { get; set; }
        public int Correctness { get; set; }
        public string Comment { get; set; }
        public List<RenovatingRequest> RenovatingRequest { get; set; }

        public AccommodationOwnerReview()
        {
            RenovatingRequest = new List<RenovatingRequest>();
        }

        public AccommodationOwnerReview(int id, AccommodationReservation reservation, int cleanliness, int correctness, string comment)
        {
            Id = id;
            Reservation = reservation;
            Cleanliness = cleanliness;
            Correctness = correctness;
            Comment = comment;
            RenovatingRequest = new List<RenovatingRequest>();  

            
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Reservation.Id.ToString(),
                Cleanliness.ToString(),
                Correctness.ToString(),
                Comment
            };
            return csvValues;

        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            Correctness = Convert.ToInt32(values[3]);
            Comment = values[4];
        }
    }
}
