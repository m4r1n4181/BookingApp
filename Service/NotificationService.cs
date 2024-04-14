using BookingApp.Model;
using BookingApp.Repository;
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

    }
}