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
    public class TourReview : ISerializable
    {

        public int Id { get; set; }
        public TourReservation TourReservation { get; set; }
        public int Knowledge { get; set; }
        public int Fluency { get; set; }
        public int TourAppeal { get; set; }
        public string Comment { get; set; }

        public List<string> Pictures { get; set; }

        public bool Validity { get; set; }


        public TourReview() { }

        public TourReview(TourReservation tourReservation, int knowledge, int fluency, int tourAppeal, string comment, List<string> pictures, bool validity)
        {

            this.TourReservation = tourReservation;
            this.Knowledge = knowledge;
            this.Fluency = fluency;
            this.TourAppeal = tourAppeal;
            this.Comment = comment;
            this.Pictures = pictures;
            this.Validity = validity;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourReservation = new TourReservation() { Id = Convert.ToInt32(values[1]) };
            Knowledge = Convert.ToInt32(values[2]);
            Fluency = Convert.ToInt32(values[3]);
            TourAppeal = Convert.ToInt32(values[4]);
            Comment = values[5];
            Pictures = values[6].Split(",").ToList();
            Validity = bool.Parse(values[7]);
        }

        public string[] ToCSV()
        {
            string picturesString = string.Join(",", Pictures);
            string[] csvValues = { Id.ToString(), TourReservation.Id.ToString(), Knowledge.ToString(), Fluency.ToString(), TourAppeal.ToString(), Comment, Validity.ToString(), };
            return csvValues;
        }
    }
}