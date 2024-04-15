﻿using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BookingApp.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservationRepository;
        private AccommodationRepository _accommodationRepository;
        public AccommodationReservationService()
        {
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationRepository = new AccommodationRepository();
        }

        public List<AccommodationReservation> GetAllByOwnerForRating(int ownerId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> ownersReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Accommodation.Owner.Id == ownerId && reservation.Departure < DateTime.Now && reservation.Departure > DateTime.Now.AddDays(-5))
                {
                    ownersReservations.Add(reservation);
                }
            }

            return ownersReservations;
        }



        public List<AccommodationReservation> GetAllByGuest(int guestId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> guestsReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Guest.Id == guestId)
                {
                    guestsReservations.Add(reservation);
                }
            }
            return guestsReservations;
        }

        public List<AccommodationReservation> GetAllByGuestForRating(int guestId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> ownersReservations = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in allReservations)
            {
                if (reservation.Guest.Id == guestId && reservation.Departure < DateTime.Now && reservation.Departure > DateTime.Now.AddDays(-5))
                {
                    ownersReservations.Add(reservation);
                }
            }

            return ownersReservations;
        }
      
        public bool DatesIntertwine(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end1 < start2 || start1 > end2)
            {
                return false;
            }
            return true;
        }

        public List<AccommodationReservation> GetReservationsForAccommodation(int accommodationId, DateTime start, DateTime end)
        {
            List<AccommodationReservation> accommodationReservations = _accommodationReservationRepository.GetByAccommodation(accommodationId);

            return accommodationReservations.FindAll(accR => DatesIntertwine(accR.Arrival, accR.Departure, start, end));
        }




        public List<AccommodationReservation> GetFreeDateRanges(int accommodationId,  DateTime start, DateTime end, int numberOfDays)
        {
            Accommodation accommodation = _accommodationRepository.GetById(accommodationId);
            if(accommodation == null)
            {
                return new List<AccommodationReservation>();
            }
            List<AccommodationReservation> accommodationReservations = GetReservationsForAccommodation(accommodationId, start, end);
            DateTime iterDate = start;
            List<AccommodationReservation> freeReservations = new List<AccommodationReservation>();

            while(iterDate.AddDays(numberOfDays) <= end)
            {
                DateTime endIterDate = iterDate.AddDays(numberOfDays);
                if (!accommodationReservations.Any(accR => DatesIntertwine(accR.Arrival, accR.Departure, iterDate, endIterDate)))
                {
                    AccommodationReservation freeReservation = new AccommodationReservation();
                    freeReservation.Accommodation = accommodation;
                    freeReservation.Arrival = iterDate;
                    freeReservation.Departure = endIterDate;
                    freeReservations.Add(freeReservation);
                }
                iterDate = iterDate.AddDays(1);
            }

            return freeReservations;
        }


        public AccommodationReservation Create(AccommodationReservation accommodationReservation)
        {
            accommodationReservation = _accommodationReservationRepository.Save(accommodationReservation);

            return accommodationReservation;
        }
        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            accommodationReservation = _accommodationReservationRepository.Update(accommodationReservation);
            return accommodationReservation;
        }
        public bool IsReschedulePossible(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            List<AccommodationReservation> reservations = _accommodationReservationRepository.GetByAccommodationId(reservationRescheduleRequest.Reservation.Accommodation.Id);
            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.Id == reservationRescheduleRequest.Reservation.Id)
                {
                    reservations.Remove(reservation);
                    break;
                }
            }
            foreach (AccommodationReservation reservation in reservations)
            {
                if (IsDatesIntertwine(reservation.Arrival, reservation.Departure, reservationRescheduleRequest.NewStart, reservationRescheduleRequest.NewEnd))
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsDatesIntertwine(DateTime StartFirst, DateTime EndFirst, DateTime StartSecond, DateTime EndSecond)
        {
            return (StartSecond.Date <= EndFirst.Date && EndSecond.Date >= StartFirst.Date);
        }

    }
}
