using BookingApp.Serializer;
using System;
using System.Collections.Generic;

namespace BookingApp.Model
{
    public class TourGuide
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Tour> Tours { get; set; }

   
        public TourGuide()
        {
            Tours = new List<Tour>();
        }

    
        public TourGuide(int id, string username, string password, string firstName, string lastName, string email, string phone)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Tours = new List<Tour>();
        }
    }
}
