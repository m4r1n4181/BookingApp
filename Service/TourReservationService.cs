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

       
        public List<TourReservation> GetAllTours()
        {
            List<TourReservation> allTourReservations = _tourReservationRepository.GetAll();
            
            return allTourReservations;
        }

        public List<TourReservation> GetAllWithTours()
        {
            List<TourReservation> allTourReservations = _tourReservationRepository.GetAllWithTours();

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
            Tourist tourist = new Tourist();
            Voucher voucher = new Voucher(-1, null, tourist, false, 365, VoucherType.resignation);
            _voucherRepository.Save(voucher);

            _tourReservationRepository.Delete(tourReservation);
        }

        public void CancelAllTourReservationsForTour(int tourEventId)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == tourEventId)
                {
                    CancelTourReservation(tourReservation);

                }
            }
        }



    }
}