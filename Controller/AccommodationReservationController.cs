using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
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
       // private readonly AccommodationReservationService _accommodationReservationService;
        public AccommodationReservationController()
        {
            _accommodationReservationService = new AccommodationReservationService();
        }

        public AccommodationReservation Create(AccommodationReservation accommodationReservation,User user)
        {
          /*  if (accommodationReservation.Guest == null)
            {
                accommodationReservation.Guest = new Guest();
            }*/

            // Postavljanje Id svojstva za Guest
            //accommodationReservation.Guest.Id = 4; ovo je bilo tu, a ovako ga postavim na logovanog
            accommodationReservation.Guest = SignInForm.LoggedUser;
            return _accommodationReservationService.Create(accommodationReservation,user);
        }

        public List<AccommodationReservation> GetFreeRangeDays(int accommodationId, DateTime start, DateTime end, int numberOfDays)
        {
            return _accommodationReservationService.GetFreeDateRanges(accommodationId, start, end, numberOfDays);
        }
        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationService.Update(accommodationReservation);
        }
        public bool IsReschedulePossible(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            return _accommodationReservationService.IsReschedulePossible(reservationRescheduleRequest);
        }

        public List<AccommodationByYearStatisticDto> GetYearStatisticForAccommodation(int accommodationId)
        {
            return _accommodationReservationService.GetYearStatisticForAccommodation(accommodationId);
        }

        public List<AccommodationByMonthStatisticDto> GetMonthStatisticForAccommodation(int year, int accommodationId)
        {
            return _accommodationReservationService.GetMonthStatisticForAccommodation(year, accommodationId);
        }

        public int GetBestYearForAccommodation(int accommodationId)
        {
            return _accommodationReservationService.GetBestYearForAccommodation(accommodationId);
        }

        public int GetBestMonthForAccommodation(int year, int accommodationId)
        {
            return _accommodationReservationService.GetBestMonthForAccommodation(year, accommodationId);
        }


    }
}
