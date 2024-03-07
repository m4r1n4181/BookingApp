using System;

namespace BookingApp.Model
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }

        public Location() { }

        public Location(string city, string country)
        {
            this.City = city;
            this.Country = country;
        }
    }
}