using BookingApp.Serializer;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BookingApp.Model
{
    public class TourGuide : User, ISerializable
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsSuperGuide { get; set; }


        public TourGuide()
        {

        }
        public TourGuide(int id)
        {
            this.Id = id;
        }

        public TourGuide(int id, string firstName, string lastName, string email, string phone, bool isSuperGuide) : this(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            IsSuperGuide = isSuperGuide;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), FirstName, LastName, Email, Phone, IsSuperGuide.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FirstName = values[1];
            LastName = values[2];
            Email = values[3];
            Phone = values[4];
            IsSuperGuide = Convert.ToBoolean(values[5]);
        }
    }
}