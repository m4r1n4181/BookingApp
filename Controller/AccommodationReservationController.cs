using Booking.App;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class AccommodationReservationController
    {
        private AccommodationReservationService _accommodationReservationService;
        public AccommodationReservationController()
        {
            _accommodationReservationService = new AccommodationReservationService();
        }

        public AccommodationReservation Create(AccommodationReservation accommodationReservation)
        {
            if (accommodationReservation.Guest == null)
            {
                accommodationReservation.Guest = new Guest();
            }

            // Postavljanje Id svojstva za Guest
            accommodationReservation.Guest.Id = 1;
            return _accommodationReservationService.Create(accommodationReservation);
        }

        public List<AccommodationReservation> GetFreeRangeDays(int accommodationId, DateTime start, DateTime end, int numberOfDays)
        {
            return _accommodationReservationService.GetFreeDateRanges(accommodationId, start, end, numberOfDays);
        }


    }
}
