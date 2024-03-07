using System;

namespace BookingApp.Model
{
    public class Location
    {
        private string City { get; set; }
        private string Country { get; set; }

        public Location() { }

        public Location(string city, string country)
        {
            this.City = city;   
            this.Country = country; 
        }

}