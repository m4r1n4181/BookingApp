using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Model
{
    public class TouristEntry : ISerializable
    {
        public int Id { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Tourist Tourist { get; set; }

        public Tour Tour { get; set; }



        public TouristEntry() { }  
        public TouristEntry(int id, KeyPoint keyPoint, Tourist tourist, Tour tour)
        {
            Id = id;
            KeyPoint = keyPoint;
            Tourist = tourist;
            Tour = tour;
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), KeyPoint.Id.ToString(), Tourist.Id.ToString(), Tour.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            KeyPoint = new KeyPoint() { Id = Convert.ToInt32(values[1]) };
            Tourist = new Tourist() { Id = Convert.ToInt32(values[2]) };
            Tour = new Tour() { Id = Convert.ToInt32(values[3]) };


        }
    }
}
