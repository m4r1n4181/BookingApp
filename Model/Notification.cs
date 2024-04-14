using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Enums.NotificationStatus Status { get; set; }

        public string Message { get; set; }


        public Notification() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), User.Id.ToString(), Status.ToString(), Message };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new User(Convert.ToInt32(values[1]));
            Enum.TryParse(values[2], out Enums.NotificationStatus status);
            Status = status;
            Message = values[3];


        }
    }
}