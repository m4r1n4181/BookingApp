using BookingApp.Domain.Models.Enums;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
  
        public class RenovatingRequest : ISerializable
        {
            public int Id { get; set; }
            public AccommodationReservation AccommodationReservation { get; set; }
            public string RenovatingSuggestion { get; set; }
            public RenovatingUrgencyLevel Level { get; set; }



            public RenovatingRequest() { }

            public RenovatingRequest(AccommodationReservation accommodationReservation,string renovatingSuggestion,RenovatingUrgencyLevel renovatingUrgencyLevel) 
            {
                AccommodationReservation = accommodationReservation;
                RenovatingSuggestion = renovatingSuggestion;
                Level = renovatingUrgencyLevel;
            }

            public string[] ToCSV()
            {
                
                string[] csvValues = { Id.ToString(), AccommodationReservation.Id.ToString(), RenovatingSuggestion, Level.ToString() };
                return csvValues;
            }

            public void FromCSV(string[] values)
            {
               
                Id = Convert.ToInt32(values[0]);
                AccommodationReservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
                RenovatingSuggestion = values[2];
                Level = (RenovatingUrgencyLevel)Enum.Parse(typeof(RenovatingUrgencyLevel), values[3]);
            }
        }


    }

