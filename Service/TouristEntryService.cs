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
        private TouristEntryRepository _touristEntryRepository;
        private TourRepository _tourRepository;
        private TourReservationRepository _tourReservationRepository;
        private NotificationRepository _notificationRepository;

        public TouristEntryService()
        {
            _touristEntryRepository = new TouristEntryRepository();
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            _notificationRepository = new NotificationRepository();
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
