using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public bool IsCancelled { get; set; }

        public AccommodationRenovation() { }

        public AccommodationRenovation(int id, Accommodation accommodation, DateTime start, DateTime end, string description, bool isCancelled)
        {
            Id = id;
            Accommodation = accommodation;
            Start = start;
            End = end;
            Description = description;
            IsCancelled = isCancelled;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Start.ToString(),
                End.ToString(),
                Description,
                IsCancelled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };

            // Pravimo string koji sadrži očekivani format datuma
          
                Start = Convert.ToDateTime(values[2]);
            
            // Ponavljamo isti postupak za krajnji datum
            
                End = Convert.ToDateTime(values[3]);
            
          

            Description = values[4];
            IsCancelled = bool.Parse(values[5]);
        }


    }
}
