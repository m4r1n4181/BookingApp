using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Serializer;

namespace BookingApp.Service
{
    public class AccommodationService
    {
        private IAccommodationRepository _accommodationRepository;

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public Accommodation RegisterAccommondation(Accommodation accommodation)
        {
            accommodation = _accommodationRepository.Save(accommodation);

            return accommodation;
        }



        public List<Accommodation> SearchAccommodations(AccommodationSearchParams searchParams)
        {
            return _accommodationRepository.SearchAccommodation(searchParams);
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public List<Accommodation> GetAllWithLocations()
        {
            return _accommodationRepository.GetAllWithLocations();
        }
        public List<Accommodation> GetByOwner(int id)
        {

            return _accommodationRepository.GetByOwner(id);
        }
        public Accommodation GetById(int id)
        {
            return _accommodationRepository.GetById(id);
        }
    }

}
