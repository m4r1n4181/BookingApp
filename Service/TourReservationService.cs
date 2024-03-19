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

        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
        }

        public List<TourReservation> GetAllTours()
        {
            List<TourReservation> allReservations = _tourReservationRepository.GetAllTours();
            
            return allReservations;
        }
    }
}