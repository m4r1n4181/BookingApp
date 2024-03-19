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
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), Guest.Id.ToString(), Arrival.ToString(), Departure.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Guest = new User(Convert.ToInt32(values[2]));
            Arrival = DateTime.ParseExact(values[3], "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
            Departure = DateTime.ParseExact(values[4], "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}