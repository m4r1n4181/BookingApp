// TourReservation.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; } // Id rezervacije
        public int GuestsNumber { get; set; }

        public Tour Tour { get; set; }
        public List<TourParticipants> Tourists { get; set; }

        public TouristEntry TouristEntry { get; set; }          //Moze biti NULL

        public Tourist Tourist { get; set; }    


        public TourReservation() { }

        public TourReservation(int id, int guestsNumber, Tour tour, List<TourParticipants> tourists, TouristEntry touristEntry, Tourist tourist)
        {
            Id = id;
            GuestsNumber = guestsNumber;
            this.Tour = tour;
            Tourists = tourists;
            this.TouristEntry = touristEntry;
            Tourist = tourist;
        }

        public TourReservation(int guestsNumber, List<TourParticipants> participants)
        {
            GuestsNumber = guestsNumber;
            Tourists = participants;
        }

        public string[] ToCSV()
        {
            string touristIds = string.Join(";", Tourists.Select(t => t.Id.ToString()));
            string[] csvValues = { Id.ToString(), GuestsNumber.ToString(), Tour.Id.ToString(), touristIds, TouristEntry.Id.ToString(), Tourist.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestsNumber = Convert.ToInt32(values[1]);
            Tour = new Tour() { Id = Convert.ToInt32(values[2]) };

            // Kreiramo novu listu za učesnike rezervacije
            Tourists = new List<TourParticipants>();

            // Dodajemo sve učesnike u listu
            string[] touristIds = values[3].Split(';');
            foreach (string touristId in touristIds)
            {
                TourParticipants participant = new TourParticipants() { Id = Convert.ToInt32(touristId) };
                Tourists.Add(participant);
            }
            TouristEntry = new TouristEntry() { Id = Convert.ToInt32(values[4]) }; //PUCA ne prikazuje se stranica uopste 
            Tourist = new Tourist() { Id = Convert.ToInt32(values[5]) };

        }

    }
}
