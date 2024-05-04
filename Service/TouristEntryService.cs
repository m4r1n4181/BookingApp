using BookingApp.DependencyInjection;
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
    public class TouristEntryService
    {
        private ITouristEntryRepository _touristEntryRepository;
        private ITourRepository _tourRepository;
        private ITourReservationRepository _tourReservationRepository;
        private INotificationRepository _notificationRepository;

        public TouristEntryService()
        {
            _touristEntryRepository = Injector.CreateInstance<ITouristEntryRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }

        public void AddTouristEntry(TouristEntry touristEntry)
        {
            int tourId = touristEntry.KeyPoint.Tour.Id;
            Tour tour = _tourRepository.GetById(tourId);

            touristEntry.Tour = tour;
            _touristEntryRepository.Save(touristEntry);

            SendNotificationForEntry(tour, touristEntry);
        }

        private void SendNotificationForEntry(Tour tour, TouristEntry touristEntry)
        {
            TourReservation tourReservation = _tourReservationRepository.GetByTourAndTourist(tour.Id, touristEntry.Tourist.Id);
            string message = "You have been added to tour " + tour.Name + " with participants:";
            foreach(TourParticipants participants in tourReservation.Tourists)
            {
                message += participants.FirstName + " " + participants.LastName + ", ";
            }
            User user = new User() { Id = touristEntry.Tourist.UserId };

            Notification notification = new Notification()
            {
                Message = message,
                NotificationStatus = Model.Enums.NotificationStatus.unread,
                User = user
            };
            _notificationRepository.Save(notification);
        }
    }
}
