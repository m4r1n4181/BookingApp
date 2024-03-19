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

        public void GetAccommodationReservationsToRate(int ownerId)
        {
            _accommodationReservationService.GetAllByOwnerForRating(ownerId);
        }
    }
}
