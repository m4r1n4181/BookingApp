using System;
using System.Collections.Generic;
using System.Linq;
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
        private AccommodationOwnerReviewService _accommodationOwnerReviewService;

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _accommodationOwnerReviewService = new AccommodationOwnerReviewService();
        }

        public Accommodation RegisterAccommondation(Accommodation accommodation)
        {
            accommodation = _accommodationRepository.Save(accommodation);

            return accommodation;
        }


        public List<Accommodation> GetAllSorted()
        {
            List<Accommodation> accommodations = GetAllWithLocations();

            // Sort the accommodations such that super owners' accommodations come first
            return accommodations.OrderBy(a => !_accommodationOwnerReviewService.IsSuperOwner(a.Owner.Id)).ToList();
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
        public void Delete(Accommodation accommodation)
        {
            _accommodationRepository.Delete(accommodation);
        }
        public Accommodation GetById(int id)
        {
            return _accommodationRepository.GetById(id);
        }
    }

}
