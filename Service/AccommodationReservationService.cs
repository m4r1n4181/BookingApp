using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BookingApp.Service
{
    public class AccommodationReservationService
    {
        private IAccommodationReservationRepository _accommodationReservationRepository;
        private IAccommodationRepository _accommodationRepository;
        private INotificationRepository _notificationRepository;
        private IOwnerReviewRepository _ownerReviewRepository;
        private ISuperGuestRepository _superGuestRepository;

        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository,IAccommodationRepository accommodationRepository,INotificationRepository notificationRepository,IOwnerReviewRepository ownerReviewRepository,ISuperGuestRepository superGuestRepository)//kontr prima interfejse, tamo gde se poziva konstr ide telo ovoga
        {

            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;
            _notificationRepository = notificationRepository;
            _ownerReviewRepository = ownerReviewRepository;
            _superGuestRepository = superGuestRepository;
        }

        public List<AccommodationReservation> GetAllByOwnerForRating(int ownerId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> ownersReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Accommodation.Owner.Id == ownerId && reservation.Departure < DateTime.Now && reservation.Departure > DateTime.Now.AddDays(-5))
                {
                    ownersReservations.Add(reservation);
                }
            }

            return ownersReservations;
        }



        public List<AccommodationReservation> GetAllByGuest(int guestId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> guestsReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Guest.Id == guestId)
                {
                    guestsReservations.Add(reservation);
                }
            }
            return guestsReservations;
        }

        public List<AccommodationReservation> GetAllByGuestForRating(int guestId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> ownersReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Guest.Id == guestId && reservation.Departure < DateTime.Now && reservation.Departure > DateTime.Now.AddDays(-5))
                {
                    if (_ownerReviewRepository.GetOneByReservation(reservation.Id) == null)
                    {
                        ownersReservations.Add(reservation);
                    }
                }
            }

            return ownersReservations;
        }

        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }

        public List<AccommodationReservation> GetReservationsForAccommodation(int accommodationId, DateTime start, DateTime end)
        {
            List<AccommodationReservation> accommodationReservations = _accommodationReservationRepository.GetByAccommodation(accommodationId);

            return accommodationReservations.FindAll(accR => DatesIntertwine(accR.Arrival, accR.Departure, start, end));
        }




        public List<AccommodationReservation> GetFreeDateRanges(int accommodationId, DateTime start, DateTime end, int numberOfDays)
        {
            Accommodation accommodation = _accommodationRepository.GetById(accommodationId);
            if (accommodation == null)
            {
                return new List<AccommodationReservation>();
            }
            List<AccommodationReservation> accommodationReservations = GetReservationsForAccommodation(accommodationId, start, end);
            DateTime iterDate = start;
            List<AccommodationReservation> freeReservations = new List<AccommodationReservation>();

            while (iterDate.AddDays(numberOfDays) <= end)
            {
                DateTime endIterDate = iterDate.AddDays(numberOfDays);
                if (!accommodationReservations.Any(accR => DatesIntertwine(accR.Arrival, accR.Departure, iterDate, endIterDate)))
                {
                    AccommodationReservation freeReservation = new AccommodationReservation();
                    freeReservation.Accommodation = accommodation;
                    freeReservation.Arrival = iterDate;
                    freeReservation.Departure = endIterDate;
                    freeReservations.Add(freeReservation);
                }
                iterDate = iterDate.AddDays(1);
            }

            return freeReservations;
        }

        public bool IsSuperGuest(User user)
        {
            return _superGuestRepository.GetById(user.Id) != null;
        }
        public AccommodationReservation Create(AccommodationReservation accommodationReservation,User user)
        {

            // Kreiranje rezervacije
            accommodationReservation = _accommodationReservationRepository.Save(accommodationReservation);

            int reservationsLastYear = CountReservationsLastYear(user);
            if (reservationsLastYear >= 10)
            {
                // Gost postaje super-gost
                if (!IsSuperGuest(user))
                {
                    // Dodeli mu status super-gosta i bonus poene
                    SuperGuest superGuest = new SuperGuest(user, DateTime.Now, DateTime.Now.AddYears(1), 6);
                    _superGuestRepository.Save(superGuest);
                }

            }
            


            // Ažuriranje statusa super-gosta i bonus poena
            UpdateSuperGuestStatus(user);

            return accommodationReservation;
        }

            public int CountReservationsLastYear(User user)
            {
                DateTime oneYearAgo = DateTime.Now.AddYears(-1);
                return _accommodationReservationRepository.GetAll().Count(r => r.Guest.Id == user.Id && r.CreatedAt > oneYearAgo);
            }

        public void CheckSuperGuestPeriod(int superGuestId)
        {
            SuperGuest superGuest = _superGuestRepository.GetById(superGuestId);
            if (superGuest != null)
            {
                if (DateTime.Now >= superGuest.End)
                {
                    // Poništavanje statusa super-gosta
                    _superGuestRepository.Delete(superGuest);
                }
                _superGuestRepository.Update(superGuest);

            }
        }
        public void UpdateSuperGuestStatus(User user)
            {
                SuperGuest superGuest = _superGuestRepository.GetById(user.Id);
                if (superGuest != null)
                {
                // Ažuriranje poena
                superGuest.Points--;
                _superGuestRepository.Update(superGuest);

                // Provera isteka statusa super-gosta
                CheckSuperGuestPeriod(superGuest.Id);
                }
        }

      


        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
            {
                accommodationReservation = _accommodationReservationRepository.Update(accommodationReservation);
                return accommodationReservation;
            }
            public bool IsReschedulePossible(ReservationRescheduleRequest reservationRescheduleRequest)
            {
                List<AccommodationReservation> reservations = _accommodationReservationRepository.GetByAccommodationId(reservationRescheduleRequest.Reservation.Accommodation.Id);

                foreach (AccommodationReservation reservation in reservations)
                {
                    if (IsDatesIntertwine(reservation.Arrival, reservation.Departure, reservationRescheduleRequest.NewStart, reservationRescheduleRequest.NewEnd))
                    {
                        return false;
                    }
                }

                return true;
            }

            public bool IsDatesIntertwine(DateTime StartFirst, DateTime EndFirst, DateTime StartSecond, DateTime EndSecond)
            {
                return (StartSecond.Date <= EndFirst.Date && EndSecond.Date >= StartFirst.Date);
            }


            public bool CancelReservation(int reservationId)
            {
                AccommodationReservation reservation = _accommodationReservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    return false;
                }
                int cancellationDays = (reservation.Accommodation.CancellationDays < 1) ? 1 : reservation.Accommodation.CancellationDays;

                if (DateTime.Now > reservation.Arrival.AddDays(-cancellationDays))
                {
                    return false;
                }

                reservation.Status = Model.Enums.AccommodationReservationStatus.Canceled;
                _accommodationReservationRepository.Update(reservation);

                User logged = SignInForm.LoggedUser;
                string message = "Guest: " + logged.Username + " has cancelled reservation for accommodation "
                    + reservation.Accommodation.Name + " for date " + reservation.Arrival;
                Notification notification = new Notification()
                {
                    User = reservation.Accommodation.Owner,
                    NotificationStatus = Model.Enums.NotificationStatus.unread,
                    Message = message
                };
                _notificationRepository.Save(notification);
                return true;
            }

        }
    }

