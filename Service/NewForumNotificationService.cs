﻿using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class NewForumNotificationService
    {
        private INewForumNotificationRepository _notificationRepository;
        private IAccommodationRepository _accommodationRepository;

        public NewForumNotificationService()
        {
            _notificationRepository = Injector.CreateInstance<INewForumNotificationRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public List<NewForumNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public NewForumNotification Get(int id)
        {
            return _notificationRepository.Get(id);
        }

        public NewForumNotification Save(NewForumNotification notification)
        {
            return _notificationRepository.Save(notification);
        }

        public void Delete(NewForumNotification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public void Update(NewForumNotification notification)
        {
            _notificationRepository.Update(notification);
        }

        private bool CheckIfOwnerHasAccommodationForLocation(Forum forum, int userId)
        {
            List<Accommodation> accommodations = _accommodationRepository.GetByOwner(userId);
            foreach (Accommodation accommodation in accommodations)
            {
                if (accommodation.Location.Id == forum.Location.Id)
                {
                    return true;
                }
            }

            return false;
        }
        public List<NewForumNotification> GetNotificationForUser(int userId)
        {
            List<NewForumNotification> notificationList = new List<NewForumNotification>();
            var allNotifications = _notificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && CheckIfOwnerHasAccommodationForLocation(notification.Forum, userId))
                {
                    notification.IsDelivered = true;
                    _notificationRepository.Update(notification);
                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }
    }
}
