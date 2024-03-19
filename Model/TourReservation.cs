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
        public Tour Tour { get; set; } // Tura koja je rezervisana
        public List<Tourist> Tourists { get; set; }
        public int GuestsNumber { get; set; }
        public DateTime ReservationDate { get; set; } // Datum kada je rezervacija napravljena
        public TourReservation() { }
        public TourReservation(int id, Tour tour, List<Tourist> tourists, int guestsNumber, DateTime reservationDate)
        {
            Id = id;
            Tour = tour;
            Tourists = tourists;
            GuestsNumber = guestsNumber;
            ReservationDate = reservationDate;


        }

        public string[] ToCSV()
        { 
            string[] csvValues = { Id.ToString(), GuestsNumber.ToString(), ReservationDate.ToString()};

            return csvValues;
        }

       
        public void FromCSV(string[] values)
        { 
            Id = Convert.ToInt32(values[0]);
            GuestsNumber = Convert.ToInt32(values[1]);
            ReservationDate = DateTime.ParseExact(values[2], "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
