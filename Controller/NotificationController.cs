using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class NotificationController
    {

        private NotificationService _notificationService;
        public NotificationController(NotificationService notificationService) 
        {
            _notificationService = notificationService;
        }
        public NotificationController() {
            _notificationService = new NotificationService();
            
        }

        public List<Notification> GetAll()
        {
            return _notificationService.GetAllNotifications();
        }
        public Notification Create(Notification notification)
        {
            return _notificationService.CreateNotification(notification);
        }
        public List<Notification> GetAllByUser(int userId)
        {
            return _notificationService.GetAllUnseenByUser(userId);
        }

        public void SetSeenNotificationsForUser(int userId)
        {
            _notificationService.SetSeenNotificationsForUser(userId);
        }

    }
}
