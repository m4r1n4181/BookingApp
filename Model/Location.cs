using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {
<<<<<<< HEAD
=======
        public int Id { get; set; }
>>>>>>> e0df37cb42a87101dd7cbaad0780f9461965e4fb
        public string City { get; set; }
        public string Country { get; set; }

        public Location() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), City, Country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}