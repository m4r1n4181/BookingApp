using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Model.Enums;
using BookingApp.Serializer;
using ISerializable = BookingApp.Serializer.ISerializable;
using NotificationStatus = BookingApp.Model.Enums.NotificationStatus;

namespace BookingApp.Model
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }

        public String Message { get; set; }

        public NotificationStatus NotificationStatus { get; set; }

        public Notification() { }

        public Notification(int id, User user, string message, NotificationStatus notificationStatus)
        {
            Id = id;
            User = user;
            Message = message;
            NotificationStatus = notificationStatus;
            
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), User.Id.ToString(), Message, NotificationStatus.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new User(Convert.ToInt32(values[1]));
            Message = Convert.ToString(values[2]);
            Enum.TryParse(values[3], out NotificationStatus notificationStatus);
            NotificationStatus = notificationStatus;
        }
    }
}

