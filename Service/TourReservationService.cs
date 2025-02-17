﻿using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservationRepository;
        private ITourRepository _tourRepository;
        private IVoucherRepository _voucherRepository;

        public TourReservationService()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();

        }
        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }


        public List<TourReservation> GetAllTours()
        {
            List<TourReservation> allTourReservations = _tourReservationRepository.GetAll();

            return allTourReservations;
        }
      

        public TourReservation Create(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }


        public List<TourReservation> GetAvailableSeats(int id)
        {
            // Dobijamo sve rezervacije za turu sa datim ID-jem
            List<TourReservation> tourReservations = _tourReservationRepository.GetByTour(id);

            // Pronalazimo maksimalan broj sedišta za datu turu
            Tour tour = _tourRepository.GetById(id);
            int maxSeats = tour.MaxTourists;

            // Racunamo ukupan broj rezervisanih sedišta
            int totalReservedSeats = tourReservations.Sum(r => r.GuestsNumber);

            // Pronalazimo broj dostupnih sedišta
            int availableSeats = maxSeats - totalReservedSeats;

            // Kreiramo listu rezervacija sa dostupnim sedištima
            List<TourReservation> availableReservations = new List<TourReservation>();
            availableReservations.Add(new TourReservation { GuestsNumber = availableSeats });

            return availableReservations;
        }

        public void CancelTourReservation(TourReservation tourReservation)
        {
            Tourist tourist = tourReservation.Tourist; 
            DateTime expires = DateTime.Now.AddDays(365); // Postavljanje datuma isteka na 365 dana od danasnjeg datuma
            Voucher voucher = new Voucher(-1,tourist, StatusType.active, expires, false, 1, VoucherType.cancellation);
            _voucherRepository.Save(voucher);

        }

        public void CancelAllTourReservationsForTour(int tourId)
        {
            Tour tour = _tourRepository.GetById(tourId);
            tour.TourStatus = TourStatusType.cancelled;
            _tourRepository.Update(tour);

            _tourReservationRepository.GetByTour(tourId).ForEach(res => CancelTourReservation(res));
            
        }

       

        

        /*public void CancelAllTourReservationsForTour(int tourId)
        {
            Tour tour = _tourRepository.GetById(tourId);
            tour.TourStatus = TourStatusType.cancelled;
            _tourRepository.Update(tour);

            _tourReservationRepository.GetByTour(tourId).ForEach(res => CancelTourReservation(res));

        }*/

        /*public List<TourReservation> GetAllParticipants(int reservationId)
        {
            return _tourReservationRepository.GetAllParticipants(reservationId);
        }*/

        public List<TourReservation> GetAllTourReservationsForTourWherePeopleShowed(int tourId)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == tourId) //moda mi treba i logika kada su se turisti prikljucili na turu - preko keypointa
                {
                    tourReservations.Add(tourReservation);

                }
            }
            return tourReservations;
        }

        /* public TourAgeGroupStatistic GetAgeStatisticsForTour(int tourId)
         {
             TourAgeGroupStatistic tourAgeGroupStatistic = new TourAgeGroupStatistic(0, 0, 0);
             foreach (TourReservation tourReservation in GetAllTourReservationsForTourWherePeopleShowed(tourId))
             {
                 foreach (TourParticipants tourist in tourReservation.Tourists)
                 {
                     if (tourist.Age <= 18)
                     {
                         tourAgeGroupStatistic.To18 += 1;
                     }
                     else if (tourist.Age > 18 && tourist.Age <= 50)
                     {
                         tourAgeGroupStatistic.From18To50 += 1;
                     }
                     else
                     {
                         tourAgeGroupStatistic.From50 += 1;
                     }
                 }
             }
             return tourAgeGroupStatistic;
         }*/
        public List<TourReservation> GetByUserId(int userId)
        {
            var reservations = _tourReservationRepository.GetAll().Where(r => r.UserId == userId).ToList();
            return reservations;
        }

        public TourStatusType GetTourStatus(TourReservation reservation)
        {
            Tour tour = _tourRepository.GetById(reservation.Tour.Id);
            return tour.TourStatus;
        }
        /* public List<TourReservation> GetPreviousReservationsByUserId(int userId)
         {
             return _tourReservationRepository.GetAllWithTours().Where(r => r.Tour.IsCompleted && r.UserId == userId).ToList();
         }

         public List<TourReservation> GetActiveReservationsByUserId(int userId)
         {
             return _tourReservationRepository.GetAllWithTours().Where(r => r.Tour.IsStarted && r.UserId == userId).ToList();
         }

         */
    }
}