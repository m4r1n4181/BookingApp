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
        private TourReservationRepository _tourReservationRepository;
        private TourRepository _tourRepository;
        private VoucherRepository _voucherRepository;

        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
            _voucherRepository = new VoucherRepository();

        }
        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }

        /* public IEnumerable<TourReservation> GetAllTours()
         {
             List<TourReservation> allTourReservations = _tourReservationRepository.GetAll();
             return allTourReservations;
         }
        */


        public List<TourReservation> GetAllTours()
        {
            List<TourReservation> allTourReservations = _tourReservationRepository.GetAll();
            
            return allTourReservations;
        }
      
        /* public List<TourReservation> GetAllWithTours()
         {
             List<TourReservation> allTourReservations = _tourReservationRepository.GetAllWithTours();

             return allTourReservations;
         }*/

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
            Tourist tourist = new Tourist(); //tourParticipant mozda 
            DateTime expires = DateTime.Now.AddDays(365); // Postavljanje datuma isteka na 365 dana od danasnjeg datuma
            Voucher voucher = new Voucher(-1, null, tourist, false, 365, expires, VoucherType.resignation);
            _voucherRepository.Save(voucher);

            _tourReservationRepository.Delete(tourReservation);
        }

        public void CancelAllTourReservationsForTour(int tourId)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == tourId)
                {
                    CancelTourReservation(tourReservation);

                }
            }
        }

        public List<TourReservation> GetAllParticipants(int reservationId)
        {
            return _tourReservationRepository.GetAllParticipants(reservationId);
        }

        public List<TourReservation> GetAllTourReservationsForTourWherePeopleShowed(int tourId)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == tourId ) //moda mi treba i logika kada su se turisti prikljucili na turu - preko keypointa
                {
                    tourReservations.Add(tourReservation);

                }
            }
            return tourReservations;
        }

        public TourAgeGroupStatistic GetAgeStatisticsForTour(int tourId)
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
        }

    }
}