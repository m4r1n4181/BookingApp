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
    }
}
