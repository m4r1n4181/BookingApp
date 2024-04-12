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


        public TourReview() { }

        public TourReview(TourReservation tourReservation, int knowledge, int fluency, int tourAppeal, string comment, List<string> pictures)
        {

            this.TourReservation = tourReservation;
            this.Knowledge =  knowledge;
            this.Fluency = fluency;
            this.TourAppeal = tourAppeal;
            this.Comment = comment;
            this.Pictures = pictures;
        }


        public void FromCSV(string[] values)
        {
            // Proverite da li niz values ima dovoljno elemenata pre nego što pristupite indeksima
            if (values.Length >= 7)
            {
                // Postavite vrednosti svojstava objekta koristeći indekse niza values
                Id = Convert.ToInt32(values[0]);
                TourReservation = new TourReservation() { Id = Convert.ToInt32(values[1]) };
                Knowledge = Convert.ToInt32(values[2]);
                Fluency = Convert.ToInt32(values[3]);
                TourAppeal = Convert.ToInt32(values[4]);
                Comment = values[5];

                // Splitujte string na listu koristeći zarez kao separator i postavite svojstvo Pictures
                Pictures = values[6].Split(',').ToList();
            }
            else
            {
                // Ako niz values nema dovoljno elemenata, možete baciti izuzetak ili obraditi grešku na neki drugi način
                throw new ArgumentException("Not enough elements in the values array.");
            }
        }



        public string[] ToCSV()
        {
            string picturesString = string.Join(",", Pictures);
            string[] csvValues = { Id.ToString(), TourReservation.Id.ToString(), Knowledge.ToString(), Fluency.ToString(), TourAppeal.ToString(), Comment };
            return csvValues;
        }
    }
}