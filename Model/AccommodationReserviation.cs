using System;

namespace Booking.App
{
    public class AccommodationReservation
    {
        private int ReservationId { get; set; }
        private int AccommodationId { get; set; }
        private int GuestId {  get; set; }
        private DateTime Arrival {  get; set; }
        private DateTime Departure { get; set; }

    }
}