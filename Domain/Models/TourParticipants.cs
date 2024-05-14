using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model.Enums;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourParticipants : Serializer.ISerializable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int UserId {  get; set; }

        public TourParticipants() { }

        public TourParticipants(int id) { UserId = id; }



        public TourParticipants(int id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), FirstName, LastName, Age.ToString(), UserId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length >= 5 && !string.IsNullOrEmpty(values[4]))
            {
                Id = Convert.ToInt32(values[0]);
                FirstName = values[1];
                LastName = values[2];
                Age = Convert.ToInt32(values[3]);
                UserId = Convert.ToInt32(values[4]);
            }
            else
            {
                Id = Convert.ToInt32(values[0]);
                FirstName = values[1];
                LastName = values[2];
                Age = Convert.ToInt32(values[3]);
                UserId = -1;
            }
        }
    }
}
