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

        public int UserId {  get; set; }
        public Tour Tour { get; set; }
        public List<TourParticipants> Tourists { get; set; }
       // public KeyPoint KeyPointWhenGuestCame { get; set; }
        public TourReservation() { }

        public TourReservation(int id, int guestsNumber, int userId, Tour tour, List<TourParticipants> tourists/*, KeyPoint keyPointWhenGuestCame*/)
        {
            Id = id;
            GuestsNumber = guestsNumber;
            UserId = userId;
            this.Tour = tour;
            Tourists = tourists;
            //KeyPointWhenGuestCame = keyPointWhenGuestCame;
        }

        public TourReservation(int guestsNumber, List<TourParticipants> participants)
        {
            GuestsNumber = guestsNumber;
            Tourists = participants;
        }

        public string[] ToCSV()
        {
            string touristIds = string.Join(";", Tourists.Select(t => t.Id.ToString()));
            string[] csvValues = { Id.ToString(), GuestsNumber.ToString(), UserId.ToString(), Tour.Id.ToString(), touristIds/*, KeyPointWhenGuestCame.Id.ToString()*/ };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestsNumber = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            Tour = new Tour() { Id = Convert.ToInt32(values[3]) };

            // Kreiramo novu listu za učesnike rezervacije
            Tourists = new List<TourParticipants>();

            // Dodajemo sve učesnike u listu
            string[] touristIds = values[4].Split(';');
            foreach (string touristId in touristIds)
            {
                TourParticipants participant = new TourParticipants() { Id = Convert.ToInt32(touristId) };
                Tourists.Add(participant);
            }

           // KeyPointWhenGuestCame = new KeyPoint() { Id = Convert.ToInt32(values[5]) };
        }




    }
}
