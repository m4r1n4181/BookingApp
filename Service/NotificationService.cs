using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class NotificationService
    {
        private NotificationRepository _notificationRepository;

        public NotificationService()
        {
            _notificationRepository = new NotificationRepository();
        }


        public List<Notification> GetAllNotifications()
        {
            return _notificationRepository.GetAll();
        }

        public Notification CreateNotification(Notification notification)
        {
            return _notificationRepository.Save(notification);
        }

        public Notification UpdateNotification(Notification notification)
        {
            return _notificationRepository.Update(notification);
        }

        public Notification GetNotificationById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public List<Notification> GetAllUnseenByUser(int userId)
        {
            List<Notification> notifications = _notificationRepository.GetAllByUser(userId);
            return notifications.FindAll(n => n.Status == Model.Enums.NotificationStatus.Unseen);
        }

        public void SetSeenNotificationsForUser(int userId)
        {
            List<Notification> unseenNotifications = GetAllUnseenByUser(userId);
            foreach(Notification notification in unseenNotifications)
            {
                notification.Status = Model.Enums.NotificationStatus.Seen;
                _notificationRepository.Update(notification);
            }
        }
    }
}