using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public User Guest { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public AccommodationOwnerReview AccommodationReview { get; set; }
        public GuestReview GuestReview { get; set; }

        public AccommodationReservationStatus Status { get; set; }

        public AccommodationReservation() { }
        public AccommodationReservation(Accommodation accommodation, User guest, DateTime arrival, DateTime departure)
        {
            Accommodation = accommodation;
            Guest = guest;
            Arrival = arrival;
            Departure = departure;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), Guest.Id.ToString(), Arrival.ToString(), Departure.ToString(), Status.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Guest = new User(Convert.ToInt32(values[2]));
            //Arrival = DateTime.ParseExact(values[3], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //Departure = DateTime.ParseExact(values[4], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
             Arrival = Convert.ToDateTime(values[3]);
             Departure = Convert.ToDateTime(values[4]);
             Enum.TryParse(values[5], out Enums.AccommodationReservationStatus status);
             Status = status;
        }
    }
}