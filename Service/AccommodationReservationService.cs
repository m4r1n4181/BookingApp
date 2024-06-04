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
        private ReservationRescheduleRequestService _reservationRescheduleService;
        private AccommodationOwnerReviewService _accommodationOwnerService;

        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository,IAccommodationRepository accommodationRepository,INotificationRepository notificationRepository,IOwnerReviewRepository ownerReviewRepository,ISuperGuestRepository superGuestRepository)//kontr prima interfejse, tamo gde se poziva konstr ide telo ovoga
        {

            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;
            _notificationRepository = notificationRepository;
            _ownerReviewRepository = ownerReviewRepository;
            _superGuestRepository = superGuestRepository;
            _reservationRescheduleService = new ReservationRescheduleRequestService(Injector.CreateInstance<IReservationRescheduleRequestRepository>());
            _accommodationOwnerService = new AccommodationOwnerReviewService();
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

        public List<Location> GetWorstTreePopularLocations()
        {
            return _accommodationReservationRepository.GetWorstTreePopularLocations();
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
        public AccommodationReservation Create(AccommodationReservation accommodationReservation, User user)
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

        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            List<AccommodationReservation> reservations = _accommodationReservationRepository.GetAllWithAccommodations();
            return reservations.Where(reservation => reservation.Accommodation.Id == id).ToList();
        }
        public List<AccommodationReservation> GetCancelledByUserId(int guest)
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.Guest.Id == guest && accommodationReservation.Status == Model.Enums.AccommodationReservationStatus.Canceled)
                {
                    accommodationReservations.Add(accommodationReservation);
                }
            }

            return accommodationReservations;
        }

        //Year statisic
        public AccommodationByYearStatisticDto CheckIfReservationIsCancelledOrRescheduledForYear(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            if (reservation.Status == Model.Enums.AccommodationReservationStatus.Canceled)
            {
                byYear.CancelledReservationsNum++;
            }
            if (_reservationRescheduleService.IsReservationRescheduled(reservation))
            {
                byYear.RescheduledReservationsNum++;
            }
            if (_accommodationOwnerService.IsReservationWithRenovationRecommendations(reservation))
            {
                byYear.RecommendationForRenovationNum++;
            }

            return byYear;
        }

        private AccommodationByYearStatisticDto AddReservationYearWhichNotExists(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            byYear.Year = reservation.Arrival.Year;
            byYear.CancelledReservationsNum = 0;
            byYear.ReservationsNum = 1;
            byYear.RescheduledReservationsNum = 0;
            byYear = CheckIfReservationIsCancelledOrRescheduledForYear(byYear, reservation);
            return byYear;
        }

        private AccommodationByYearStatisticDto AddReservationYearWhichExists(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            byYear.ReservationsNum++;
            byYear = CheckIfReservationIsCancelledOrRescheduledForYear(byYear, reservation);

            return byYear;
        }

        private void AddReservationYearToStatistics(List<AccommodationByYearStatisticDto> statistics, AccommodationReservation reservation)
        {
            AccommodationByYearStatisticDto byYear = null;
            foreach (AccommodationByYearStatisticDto accommodationByYearStatisticDto in statistics)
            {
                if (accommodationByYearStatisticDto.Year == reservation.Arrival.Year)
                {
                    byYear = accommodationByYearStatisticDto;
                    break;
                }
            }
            if (byYear == null)
            {
                byYear = new AccommodationByYearStatisticDto(0, 0, 0, 0, 0);
                statistics.Add(AddReservationYearWhichNotExists(byYear, reservation));
            }
            else
            {
                AddReservationYearWhichExists(byYear, reservation);
            }
        }

        public List<AccommodationByYearStatisticDto> GetYearStatisticForAccommodation(int accommodationId)
        {
            List<AccommodationByYearStatisticDto> statistics = new List<AccommodationByYearStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationYearToStatistics(statistics, reservation);
            }

            return statistics;
        }
        public AccommodationByYearStatisticDto GetStatisticForYear(int accommodationId, int year)
        {
            return GetYearStatisticForAccommodation(accommodationId).FirstOrDefault(ss => ss.Year == year);

        }


        public AccommodationByMonthStatisticDto CheckIfReservationIsCancelledOrRescheduledForMonth(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            if (reservation.Status == Model.Enums.AccommodationReservationStatus.Canceled)
            {
                byMonth.CancelledReservationsNum++;
            }
            if (_reservationRescheduleService.IsReservationRescheduled(reservation))
            {
                byMonth.RescheduledReservationsNum++;
            }
            if (_accommodationOwnerService.IsReservationWithRenovationRecommendations(reservation))
            {
                byMonth.RecommendationForRenovationNum++;
            }
            return byMonth;
        }
        private AccommodationByMonthStatisticDto AddReservationMonthWhichNotExists(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            byMonth.Month = reservation.Arrival.Month;
            byMonth.CancelledReservationsNum = 0;
            byMonth.ReservationsNum = 1;
            byMonth.RescheduledReservationsNum = 0;
            byMonth = CheckIfReservationIsCancelledOrRescheduledForMonth(byMonth, reservation);
            return byMonth;
        }

        private AccommodationByMonthStatisticDto AddReservationMonthWhichExists(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            byMonth.ReservationsNum++;
            byMonth = CheckIfReservationIsCancelledOrRescheduledForMonth(byMonth, reservation);
            return byMonth;
        }

        private void AddReservationMonthToStatistics(List<AccommodationByMonthStatisticDto> statistics, AccommodationReservation reservation, int year)
        {
            AccommodationByMonthStatisticDto byMonth = null;
            foreach (AccommodationByMonthStatisticDto accommodationByMonthStatisticDto in statistics)
            {
                if (accommodationByMonthStatisticDto.Month == reservation.Arrival.Month && year == reservation.Arrival.Year)
                {
                    byMonth = accommodationByMonthStatisticDto;
                    break;
                }
            }
            if (byMonth == null)
            {
                byMonth = new AccommodationByMonthStatisticDto(0, 0, 0, 0, 0);
                statistics.Add(AddReservationMonthWhichNotExists(byMonth, reservation));
            }
            else
            {
                AddReservationMonthWhichExists(byMonth, reservation);
            }
        }

        public List<AccommodationByMonthStatisticDto> GetMonthStatisticForAccommodation(int year, int accommodationId)
        {
            List<AccommodationByMonthStatisticDto> statisticsByMonth = new List<AccommodationByMonthStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.Arrival.Year == year)
                {
                    AddReservationMonthToStatistics(statisticsByMonth, reservation, year);
                }
            }

            return statisticsByMonth;
        }

        //Best year
        private void AddReservationYearToBestStatistics(List<BestStatisticDto> statistics, AccommodationReservation reservation)
        {
            BestStatisticDto byYear = null;
            foreach (BestStatisticDto bestStatisticDto in statistics)
            {
                if (bestStatisticDto.Year == reservation.Arrival.Year)
                {
                    byYear = bestStatisticDto;
                    break;
                }
            }
            if (byYear == null)
            {
                byYear = new BestStatisticDto() { Year = reservation.Arrival.Year, DaysReserved = (int)(reservation.Departure - reservation.Arrival).TotalDays };
                statistics.Add(byYear);
            }
            else
            {
                byYear.DaysReserved += (int)(reservation.Departure - reservation.Arrival).TotalDays;
            }
        }

        public int GetBestYearForAccommodation(int accommodationId)
        {
            List<BestStatisticDto> statistics = new List<BestStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationYearToBestStatistics(statistics, reservation);
            }
            int max = 0;
            if (statistics.Any())
            {
                max = statistics.Max(i => i.DaysReserved);
            }
            else
            {
                return max;
            }
            BestStatisticDto bestStatistic = statistics.First(x => x.DaysReserved == max);
            return bestStatistic.Year;
        }

        //Best month
        private void AddReservationMonthToBestStatistics(int year, List<BestStatisticMonthDto> statistics, AccommodationReservation reservation)
        {
            BestStatisticMonthDto byMonth = null;
            foreach (BestStatisticMonthDto bestStatisticDto in statistics)
            {
                if (bestStatisticDto.Month == reservation.Arrival.Month && reservation.Arrival.Year == year)
                {
                    byMonth = bestStatisticDto;
                    break;
                }
            }
            if (byMonth == null)
            {
                byMonth = new BestStatisticMonthDto() { Month = reservation.Arrival.Month, DaysReserved = (int)(reservation.Departure - reservation.Arrival).TotalDays };
                statistics.Add(byMonth);
            }
            else
            {
                byMonth.DaysReserved += (int)(reservation.Departure - reservation.Arrival).TotalDays;
            }
        }

        public int GetBestMonthForAccommodation(int year, int accommodationId)
        {
            List<BestStatisticMonthDto> statistics = new List<BestStatisticMonthDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.Arrival.Year == year)
                {
                    AddReservationMonthToBestStatistics(year, statistics, reservation);
                }
            }

            int max = statistics.Max(i => i.DaysReserved);
            BestStatisticMonthDto bestStatistic = statistics.First(x => x.DaysReserved == max);

            return bestStatistic.Month;
        }
        public List<Location> GetTopThreePopularLocations()
        {
            return _accommodationReservationRepository.GetTopThreePopularLocations();
            
        
        }

    }
}

