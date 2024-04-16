using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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


        public List<TourReservation> GetAvailableSeats(int id)
        {
            return _tourReservationService.GetAvailableSeats(id);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationService.Update(tourReservation);
        }


        public void CancelTourReservation(TourReservation tourReservation)
        {
            _tourReservationService.CancelTourReservation(tourReservation);
        }


        public void CancelAllTourReservationsForTour(int tourId)
        {
            _tourReservationService.CancelAllTourReservationsForTour(tourId); //brisem zapravo sve rezervacije tj participants
        }

      


    }
}