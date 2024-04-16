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

        private List<Notification> _notifications;


        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }
        public Notification GetById(int id)
        {
            _notifications = _serializer.FromCSV(FilePath);
            return _notifications.FirstOrDefault(notification => notification.Id == id);
        }

        public List<Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Notification Save(Notification notification)
        {
            notification.Id = NextId();
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }

        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(v => v.Id) + 1;
        }

        public void Delete(Voucher voucher)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification founded = _notifications.Find(v => v.Id == voucher.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public Notification Update(Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification current = _notifications.Find(v => v.Id == notification.Id);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }

        public List<Notification> GetByUserId(int userId)
        {
            _notifications = _serializer.FromCSV(FilePath);
            return _notifications.FindAll(n => n.User.Id == userId && n.NotificationStatus == Model.Enums.NotificationStatus.unread);

        }

    }
}
