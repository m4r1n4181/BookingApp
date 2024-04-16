using BookingApp.Serializer;
using System;
using System.Collections.Generic;

namespace BookingApp.Model
{
    public class TourGuide : User
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public TourGuide()
        {

        }
        public TourGuide(int id)
        {
            this.Id = id;
        }

        public TourGuide(int Id, string firstName, string lastName, string email, string phone)
        {
            Id = Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
    }
}