using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class KeyPoint : ISerializable 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Tour Tour { get; set; }

        public KeyPoint() 
        { 
        }

        public KeyPoint(int id, string name, bool isActive, Tour tour)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            Tour = tour;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),  Name,  IsActive.ToString(), Tour.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            IsActive = Convert.ToBoolean(values[2]);
            Tour = new Tour(Convert.ToInt32(values[3]));
        }

        
    }

}