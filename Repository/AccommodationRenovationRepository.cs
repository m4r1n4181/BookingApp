using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRenovations.csv";


        private readonly Serializer<AccommodationRenovation> _serializer;

        private List<AccommodationRenovation> _accommodationRenovations;

        public AccommodationRenovationRepository()
        {
            _serializer = new Serializer<AccommodationRenovation>();
            _accommodationRenovations = _serializer.FromCSV(FilePath);
        }

        public void BindAccommodationRenovationWithAccommodation()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            _accommodationRenovations.ForEach(ar => ar.Accommodation = accommodationRepository.GetById(ar.Accommodation.Id));


        }
        public AccommodationRenovation Save(AccommodationRenovation accommodationRenovation)
        {
            accommodationRenovation.Id = NextId();
            _accommodationRenovations.Add(accommodationRenovation);
            _serializer.ToCSV(FilePath, _accommodationRenovations);
            return accommodationRenovation;
        }
        public List<AccommodationRenovation> GetAllForOwner(int id)

        {
            List<AccommodationRenovation> renovations = GetAllWithAccommodation();


            return renovations.Where(renovations => renovations.Accommodation.Owner.Id == id).ToList();
        }
        public List<AccommodationRenovation> GetAllPreviousRenovations(int id)
        {
            List<AccommodationRenovation> renovations = GetAllWithAccommodation();
            DateTime today = DateTime.Today;
            return renovations.Where(renovation => renovation.End < today).ToList();
        }

        public List<AccommodationRenovation> GetAllScheduledRenovations(int id)
        {
            List<AccommodationRenovation> renovations = GetAllWithAccommodation();
            DateTime today = DateTime.Today;
            return renovations.Where(renovation => renovation.Start >= today).ToList();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovations;
        }
        public List<AccommodationRenovation> GetAllWithAccommodation()
        {
            List<AccommodationRenovation> renovations = GetAll();
            BindAccommodationRenovationWithAccommodation();

            return renovations;
        }

        public AccommodationRenovation Get(int id)
        {
            return _accommodationRenovations.Find(ar => ar.Id == id);
        }

        public int NextId()
        {
            if (_accommodationRenovations.Count < 1)
            {
                return 1;
            }
            return _accommodationRenovations.Max(ar => ar.Id) + 1;
        }
        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            AccommodationRenovation founded = _accommodationRenovations.Find(ar => ar.Id == accommodationRenovation.Id);
            _accommodationRenovations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationRenovations);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            AccommodationRenovation current = _accommodationRenovations.Find(ar => ar.Id == accommodationRenovation.Id);
            int index = _accommodationRenovations.IndexOf(current);
            _accommodationRenovations.Remove(current);
            _accommodationRenovations.Insert(index, accommodationRenovation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodationRenovations);
            return accommodationRenovation;
        }

        public List<AccommodationRenovation> GetByAccommodationId(int id)
        {
            List<AccommodationRenovation> renovations = GetAll();
            return renovations.Where(renovations => renovations.Accommodation.Id == id).ToList();
        }
    }
}
