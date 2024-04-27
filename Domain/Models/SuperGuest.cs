using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class SuperGuest : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public int Points {  get; set; }



        public SuperGuest() { }

        public SuperGuest(User guest, DateTime start, DateTime end, int points)
        {
            Guest = guest;
            Start = start;
            End = end;
            Points = points;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Guest.Id.ToString(), Start.ToString(), End.ToString(), Points.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = new User(Convert.ToInt32(values[1]));
            Start = Convert.ToDateTime(values[2]);
            End = Convert.ToDateTime(values[3]);
            Points = Convert.ToInt32(values[4]);
        }

    }
}
