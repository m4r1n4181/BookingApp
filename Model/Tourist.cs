﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class Tourist : ISerializable
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public Tour Tour { get; set; }

        

        public Tourist() { }

        public Tourist(int id, string firstName, string lastName, int age, Tour tour)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Tour = tour;
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
