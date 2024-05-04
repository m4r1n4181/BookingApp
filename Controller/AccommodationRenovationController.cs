using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class AccommodationRenovationController
    {
        private readonly AccommodationRenovationService _accommodationRenovationService;

        public AccommodationRenovationController()
        {
            _accommodationRenovationService = new AccommodationRenovationService();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovationService.GetAll();
        }
        public List<AccommodationRenovation> GetAllPreviousRenovations(int id)
        {

            return _accommodationRenovationService.GetAllPreviousRenovations(id);
        }

        public List<AccommodationRenovation> GetAllScheduledRenovations(int id)
        {
            return _accommodationRenovationService.GetAllScheduledRenovations(id);
        }
        public List<AccommodationRenovation> GetAllForOwner(int id)

        {
            return _accommodationRenovationService.GetAllForOwner(id);
        }
        public AccommodationRenovation Get(int id)
        {
            return _accommodationRenovationService.Get(id);
        }

        public AccommodationRenovation Save(AccommodationRenovation accommodationRenovation)
        {

            return _accommodationRenovationService.Save(accommodationRenovation);
        }

        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            _accommodationRenovationService.Delete(accommodationRenovation);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            return _accommodationRenovationService.Update(accommodationRenovation);
        }

        public List<DateRange> FindAllAvailableTerms(Accommodation accommodation, DateTime Start, DateTime End, int Duration)
        {
            return _accommodationRenovationService.FindAllAvailableTerms(accommodation, Start, End, Duration);
        }

        public List<AccommodationRenovation> GetAllValidRenovations(Accommodation accommodation)
        {
            return _accommodationRenovationService.GetAllValidRenovations(accommodation);
        }

        public void CancelRenovation(AccommodationRenovation renovation)
        {
            _accommodationRenovationService.CancelRenovation(renovation);
        }

    }
}
