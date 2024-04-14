using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class NotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";
        private readonly Serializer<Notification> _serializer;
        public List<Notification> Notifications { get; set; }
        public NotificationRepository() 
        {
            _serializer = new Serializer<Notification>();
            Notifications = _serializer.FromCSV(FilePath);

        }


        public List<Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Notification Update(Notification notification)
        {
            Notifications = _serializer.FromCSV(FilePath);
            Notification current = Notifications.Find(a => a.Id == notification.Id);
            int index = Notifications.IndexOf(current);
            Notifications.Remove(current);
            Notifications.Insert(index, notification);
            _serializer.ToCSV(FilePath, Notifications);
            return notification;
        }


        public Notification Save(Notification notification)
        {
            notification.Id = NextId();
            Notifications = _serializer.FromCSV(FilePath);
            Notifications.Add(notification);
            _serializer.ToCSV(FilePath, Notifications);
            return notification;
        }

        public int NextId()
        {
            Notifications = _serializer.FromCSV(FilePath);
            if (Notifications.Count < 1)
            {
                return 1;
            }
            return Notifications.Max(a => a.Id) + 1;
        }

        public Notification GetById(int id)
        {
            Notifications = _serializer.FromCSV(FilePath);
            return Notifications.FirstOrDefault(acc => acc.Id == id);
        }



    }


}
