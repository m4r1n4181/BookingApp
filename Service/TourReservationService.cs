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
        /*public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }*/

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
            Tour tour = _tourRepository.Get(id);
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

        public List<Tour> GetAlternativeTours(Location location)
        {
            // Pretpostavljamo da imamo listu tura i da želimo vratiti one koje su na istoj lokaciji
            return _tourRepository.GetAll().Where(tour => tour.Location == location && tour.AvailableSeats > 0).ToList();
        }

       /* public List<TourReservation> GetAllTourReservationsForTourEventWherePeopleShowed(int tourEventId)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == tourEventId && tourReservation.KeyPointWhenGuestCame.Id != -1)
                {
                    tourReservations.Add(tourReservation);

                }
            }
            return tourReservations;
        }*/
        public List<TourReservation> GetReservationsByUserId(int userId)
        {
            return _tourReservationRepository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public List<TourReservation> GetPreviousReservationsByUserId(int userId)
        {
            return _tourReservationRepository.GetAllWithTours().Where(r => r.Tour.IsCompleted && r.UserId == userId).ToList();
        }

        public List<TourReservation> GetActiveReservationsByUserId(int userId)
        {
            return _tourReservationRepository.GetAllWithTours().Where(r => r.Tour.IsStarted && r.UserId == userId).ToList();
        }


    }
}