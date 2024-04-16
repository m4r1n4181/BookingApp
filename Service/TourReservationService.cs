using BookingApp.Model;
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

        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
        }
        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }

       /* public List<TourRepository> GetReservationsForTour(int tourId, DateTime start, DateTime end)
        {
            List<TourReservation> tourReservations = _tourReservationRepository.GetByTour(tourId);
            return tourReservations.FindAll(tR => ;
        }*/
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


    }
}