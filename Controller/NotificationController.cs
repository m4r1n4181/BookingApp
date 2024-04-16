using BookingApp.Service;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;

namespace BookingApp.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService;

        public NotificationController()
        {
            _notificationService = new NotificationService();
        }

        public List<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }
        public Notification GetById(int id) {
                return _notificationService.GetById(id);
        }
        public List<Notification> GetByUserId(int id)
        {
            return _notificationService.GetByUserId(id);
        }
        public void ReadAllUserNotifications(int userId)
        {
            _notificationService.ReadAllUserNotifications(userId);
        }
    }
}
