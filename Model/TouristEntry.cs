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

        public TouristEntry() { }  
        public TouristEntry(int id, KeyPoint keyPoint, User tourist)
        {
            Id = id;
            KeyPoint = keyPoint;
            Tourist = tourist;
        }

        public int Id { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public User Tourist { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), KeyPoint.Id.ToString(), Tourist.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            KeyPoint = new KeyPoint() { Id = Convert.ToInt32(values[1]) };
            Tourist = new User() { Id = Convert.ToInt32(values[2]) };
            
        }
    }
}
