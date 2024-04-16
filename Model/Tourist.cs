using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Model;
using BookingApp.Model.Enums;


namespace BookingApp.Model
{
    public class Tourist : BookingApp.Serializer.ISerializable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public int UserId { get; set; }
      

        public Tourist() { }

        public Tourist(int id) { this.Id = id; }
        public Tourist(string firstName, string lastName, int age)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public Tourist(int id, string firstName, string lastName, int age)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public Tourist(int id, string firstName, string lastName, int age, string username, string password, int userId) 
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UserId = userId;
        }


         public string[] ToCSV()
         {
             string[] csvValues = { Id.ToString(), FirstName, LastName, Age.ToString(), UserId.ToString() };
             return csvValues;
         }

         public void FromCSV(string[] values)
         {
             Id = Convert.ToInt32(values[0]);
             FirstName = values[1];
             LastName = values[2];
             Age = Convert.ToInt32(values[3]);
             UserId = Convert.ToInt32(values[4]);
         }


    }

    }



