using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Service
{
    public class NotificationService
    {
        private NotificationRepository _notificationRepository;

        public NotificationService() {
            
            _notificationRepository = new NotificationRepository();
        }

        public List<Notification> GetAll()
        {

            return _notificationRepository.GetAll();
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public List<Notification> GetByUserId(int id)
        {
            return _notificationRepository.GetByUserId(id);
        }

        public void ReadAllUserNotifications(int userId)
        {
            List<Notification> userNotifications = _notificationRepository.GetByUserId(userId);
            foreach (Notification notification in userNotifications)
            {
                notification.NotificationStatus = Model.Enums.NotificationStatus.read;
                _notificationRepository.Update(notification);

            }
        }
    }
}
