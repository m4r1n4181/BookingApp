using System;
using System.Collections.Generic;

namespace BookingApp.Model
{
    public class TourReservation
    {
        public Tour Tour { get; set; } // Tura koja je rezervisana
        public List<Tourist> Participants { get; set; } // Lista učesnika rezervacije (turista)
        public DateTime ReservationDate { get; set; } // Datum kada je rezervacija napravljena

        public TourReservation(Tour tour)
        {
            Tour = tour;
            Participants = new List<Tourist>();
            ReservationDate = DateTime.Now; // Postavljamo trenutni datum kao podrazumijevani datum rezervacije
        }

        public bool ReserveTour(int numberOfPeople)
        {
            if (Tour.MaxTourists - Tour.AvaibleSeats < numberOfPeople)
            {
                // Nema dovoljno slobodnih mjesta na turi
                Console.WriteLine("Nema dovoljno slobodnih mjesta na turi.");
                Console.WriteLine($"Trenutno je dostupno {Tour.AvaibleSeats} slobodnih mjesta.");
                return false;
            }

            // Rezervišemo ture
            Tour.AvaibleSeats -= numberOfPeople;
            return true;
        }

        public void CancelReservation()
        {
            // Vraćamo mjesta ako je rezervacija otkazana
            Tour.AvaibleSeats += Participants.Count;
            Participants.Clear();
        }
    }
}
