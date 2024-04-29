using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {

        private const string FilePath = "../../../Resources/Data/tour-requests.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequest;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequest = _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequest = _serializer.FromCSV(FilePath);
            _tourRequest.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequest);
            return tourRequest;
        }

        public int NextId()
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            if (_tourRequest.Count < 1)
            {
                return 1;
            }
            return _tourRequest.Max(c => c.Id) + 1;
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            TourRequest founded = _tourRequest.Find(c => c.Id == tourRequest.Id);
            _tourRequest.Remove(founded);
            _serializer.ToCSV(FilePath, _tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequest.Find(c => c.Id == tourRequest.Id);
            int index = _tourRequest.IndexOf(current);
            _tourRequest.Remove(current);
            _tourRequest.Insert(index, tourRequest);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourRequest);
            return tourRequest;
        }

        public TourRequest GetById(int id)
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            return _tourRequest.FirstOrDefault(tr => tr.Id == id);
        }

        public void BindTourRequestUser() //za svaki tourrequest mi daj iz useraReposiotry turistu
        {
            UserRepository userRepository = new UserRepository();
            _tourRequest.ForEach(tR => tR.Tourist = userRepository.GetById(tR.Tourist.Id));

        }
        public List<TourRequest> GetAllWithUser()
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            BindTourRequestUser();
            return _tourRequest;
        }

        public void BindTourRequestLocation() // za svaki request daj mi location
        {
            LocationRepository locationRepository = new LocationRepository();
            _tourRequest.ForEach(tR => tR.Location = locationRepository.GetById(tR.Location.Id));

        }

        public List<TourRequest> GetAllWithLocations()
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            BindTourRequestLocation();
            return _tourRequest;
        }

        public List<TourRequest> GetByTourist(int touristId) //bi trebalo da bude ovako
        {
            return _tourRequest.FindAll(i => i.Tourist.Id == touristId);
        }

        public List<TourRequest> GetByTourGuide(int tourGuideId) //bi trebalo da bude ovako
        {
           // BindTourRequestLocation();
            return _tourRequest.FindAll(i => i.TourGuide.Id == tourGuideId);
        }

        public List<TourRequest> SearchTourRequest(TourRequestSearch tourRequestSearch)
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            BindTourRequestLocation();
           
            if (tourRequestSearch.City != null)
            {
                _tourRequest = _tourRequest.FindAll(a => a.Location.City.Contains(tourRequestSearch.City, StringComparison.OrdinalIgnoreCase));
            }

            if (tourRequestSearch.Country != null)
            {
                _tourRequest = _tourRequest.FindAll(a => a.Location.Country.Contains(tourRequestSearch.Country, StringComparison.OrdinalIgnoreCase));
            }
            if (tourRequestSearch.Status != null)
            {
                _tourRequest = _tourRequest.FindAll(a => a.RequestStatus == tourRequestSearch.Status);
            }
            if (tourRequestSearch.MaxTourists != 0)
            {
                _tourRequest = _tourRequest.FindAll(a => a.MaxTourists >= tourRequestSearch.MaxTourists);
            }

            if (tourRequestSearch.StartDate != null || tourRequestSearch.EndDate != null)
            {
                _tourRequest = _tourRequest.FindAll(a =>
                    (tourRequestSearch.StartDate == null || a.StartDate >= tourRequestSearch.StartDate) &&
                    (tourRequestSearch.EndDate == null || a.EndDate <= tourRequestSearch.EndDate));
            }


            return _tourRequest;
        }

        public List<string> GetUniqueLanguagesFromTourRequests()
        {
            var uniqueLanguages = _tourRequest.Select(tr => tr.Language).Distinct().ToList();
            return uniqueLanguages;
        }

        public List<Location> GetUniqueLocationsFromTourRequests()
        {
            BindTourRequestLocation();
            List<Location> uniqueLocations = _tourRequest
                .Select(tr => tr.Location)
                .Distinct()
                .GroupBy(l => l.Id) // Grupiraj po ID-u lokacije
                .Select(grp => grp.First()) // Odaberi prvu lokaciju iz svake grupe
                .ToList();

            return uniqueLocations;
        }

        public List<int> GetUniqueYearsFromTourRequests()
        {
            List<int> uniqueYears = _tourRequest
                .Select(tr => tr.StartDate.Year) 
                .Distinct()
                .ToList();

            return uniqueYears;
        }

        public int CountRequestsByLocation(Location location)
        {
            return _tourRequest.Count(tr => tr.Location.Id == location.Id);
        }

        public int CountRequestsByLanguage(string language)
        {
            return _tourRequest.Count(tr => tr.Language == language);
        }

        public int CountRequestsByYear(int year)
        {
            return _tourRequest.Count(tr => tr.StartDate.Year == year);
        }

        public Dictionary<string, int> CountRequestsByYearAndMonth(int year)
        {
            var requestsByYear = _tourRequest
                .Where(tr => tr.StartDate.Year == year)
                .ToList();

            var requestsByYearAndMonth = new Dictionary<string, int>();

            for (int month = 1; month <= 12; month++)
            {
                var requestsInMonth = requestsByYear.Count(tr => tr.StartDate.Month == month);
                requestsByYearAndMonth.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month), requestsInMonth);
            }

            return requestsByYearAndMonth;
        }
    }


}

