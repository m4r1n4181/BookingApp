using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Model
{
    public class OwnerReview : ISerializable
    {

        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int Cleanliness { get; set; }
        public int RuleAdherence { get; set; }
        public string Comment { get; set; }

        public List<string> Pictures { get; set; }


        public OwnerReview() { }

        public OwnerReview(AccommodationReservation accommodationReservation, int cleanliness, int ruleAdherence, string comment,List<string> pictures)
        {

            this.Reservation = accommodationReservation;
            this.Cleanliness = cleanliness;
            this.RuleAdherence = ruleAdherence;
            this.Comment = comment;
            this.Pictures = pictures;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            RuleAdherence = Convert.ToInt32(values[3]);
            Comment = values[4];
            Pictures = values[5].Split(",").ToList();
        }

        public string[] ToCSV()
        {
            string picturesString = string.Join(",", Pictures);
            string[] csvValues = {Id.ToString(),Reservation.Id.ToString(),Cleanliness.ToString(),RuleAdherence.ToString(),Comment, picturesString };
            return csvValues;
        }
    }
}
