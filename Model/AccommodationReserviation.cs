using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Booking.App
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public Guest Guest {  get; set; }
        public DateTime Arrival {  get; set; }
        public DateTime Departure { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), Guest.Id.ToString(), Arrival.ToString(), Departure.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Guest = new Guest(Convert.ToInt32(values[2]));
            Arrival = Convert.ToDateTime(values[3]);
            Departure = Convert.ToDateTime(values[4]);
        }
    }
}