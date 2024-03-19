// TourReservation.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; } // Id rezervacije
        public int GuestsNumber { get; set; }

        public Tour Tour { get; set; }  
        public User Tourist { get; set; }
        public TourReservation() { }

        public TourReservation(int id, int guestsNumber, Tour tour, User tourist)
        {
            Id = id;
            GuestsNumber = guestsNumber;
            this.Tour = tour;
            Tourist = tourist;
        }

        public string[] ToCSV()
        { 
            string[] csvValues = { Id.ToString(), GuestsNumber.ToString(), Tour.Id.ToString(),Tourist.Id.ToString()};
            return csvValues;
        }

       
        public void FromCSV(string[] values)
        { 
            Id = Convert.ToInt32(values[0]);
            GuestsNumber = Convert.ToInt32(values[1]);
            Tour = new Tour() { Id = Convert.ToInt32(values[2]) };
            Tourist = new User() {  Id = Convert.ToInt32(values[3]) };
        }
    }
}
