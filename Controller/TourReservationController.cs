using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourReservationController
    {
        private TourReservationService _tourReservationService;
        public TourReservationController()
        {
            _tourReservationService = new TourReservationService();
        }

        public void GetAllTours()
        {
            _tourReservationService.GetAllTours();
        }

        public List<TourReservation> GetAvaibleSeats(int id)
        {
            return _tourReservationService.GetAvailableSeats(id);
        }
        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationService.Update(tourReservation);
        }
    }
}