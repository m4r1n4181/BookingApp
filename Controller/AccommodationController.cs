using System;
using System.Collections.Generic;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;

namespace BookingApp.Controller
{
    public class AccommodationController
    {
        private AccommodationService _accommodationService;
        public AccommodationController()
        {
            _accommodationService = new AccommodationService();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationService.GetAll();
        }

        public Accommodation Create(Accommodation accommodation)
        {
            return _accommodationService.RegisterAccommondation(accommodation);
        }
        public void Delete(Accommodation accommodation)
        {
            _accommodationService.Delete(accommodation);
        }
        public List<Accommodation> GetAllWithLocations()
        {
            return _accommodationService.GetAllWithLocations();
        }

        public void RegisterAccommondation(Accommodation accommondation)
        {
            _accommodationService.RegisterAccommondation(accommondation);

        }

        public List<Accommodation> SearchAccommodations(AccommodationSearchParams searchParams)
        {
            return _accommodationService.SearchAccommodations(searchParams);
        }
        public List<Accommodation> GetByOwner(int id)
        {

            return _accommodationService.GetByOwner(id);
        }
        public Accommodation GetById(int id)
        {
            return _accommodationService.GetById(id);
        }

        internal List<Accommodation> GetAllSorted()
        {
            return _accommodationService.GetAllSorted();
        }
    }

}