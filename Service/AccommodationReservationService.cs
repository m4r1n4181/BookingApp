﻿using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservationRepository;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }

        public List<AccommodationReservation> GetAllByOwnerForRating(int ownerId)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAllWithAccommodations();
            List<AccommodationReservation> ownersReservations = new List<AccommodationReservation>();

            foreach(AccommodationReservation reservation in allReservations)
            {
                if(reservation.Accommodation.Owner.Id == ownerId && reservation.Departure < DateTime.Now && reservation.Departure > DateTime.Now.AddDays(-5))
                {
                    ownersReservations.Add(reservation);
                }
            }

            return ownersReservations;
        }

        /*public List<AccommodationReservation> GetAccommodationReservationsToRate(int ownerId)
        {
            List<AccommodationReservation> allAccommodationReservations = _accommodationReservationRepository.GetAll();
            List<AccommodationReservation> accommodationReservationsToRate = new List<AccommodationReservation>();
            DateTime currentDate = DateTime.Now;
            foreach (AccommodationReservation accommodationReservation in allAccommodationReservations)
            {
                if (accommodationReservation.Departure < currentDate)
                {
                    if ((currentDate - accommodationReservation.Departure).TotalDays <= 5)
                    {
                        accommodationReservationsToRate.Add(accommodationReservation);
                    }

                }
            }
            return accommodationReservationsToRate;

        }
        */
    }
}
