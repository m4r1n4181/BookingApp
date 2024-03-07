using System;
using System.Collections.Generic;

namespace BookingApp.Model
{
    public enum AccommodationType
    {
        apartment,
        house,
        hut
    }
    public class Accommodation
    {
        private int AccommodationId {  get; set; }
        private int OwnerId {  get; set; }
        private string Name { get; set; }
        private AccommodationType Type { get; set; }
        private Location Location { get; set; }
        private int MaxGuests { get; set; }
        private int MinReservationDays { get; set; }
        private int CancellationDays { get; set; }
        private List<string> Pictures { get; set; }


        public Accommodation() {}
        
        public Accommodation(int idAccommodation, int idOwner, string name, AccommodationType type, Location location, int maxGuests, int minReservationDays, int cancellationDays)
            {
                this.AccommodationId = idAccommodation;
                this.OwnerId = idOwner;
                this.Name = name;
                this.Type = type;
                this.Location = location;
                this.MinReservationDays = minReservationDays;
                this.CancellationDays = cancellationDays;
                this.Pictures = new List<string>();
            }


    }



}
