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
    public class Tourist : User, BookingApp.Serializer.ISerializable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
      

        public Tourist() { }
        public Tourist(string firstName, string lastName, int age)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public Tourist(int id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public Tourist( string firstName, string lastName, int age, string username, string password) : base(username, password, UserType.Tourist)
        {
            //Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }


         public string[] ToCSV()
         {
             string[] csvValues = { Id.ToString(), FirstName, LastName, Age.ToString() };
             return csvValues;
         }

         public void FromCSV(string[] values)
         {
            Id = Convert.ToInt32(values[0]);
             FirstName = values[1];
             LastName = values[2];
             Age = Convert.ToInt32(values[3]);
         }


    }

    }



