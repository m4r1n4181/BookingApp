using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
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

            var filteredRequests = _tourRequest;

            if (tourRequestSearch.City != null)
            {
                filteredRequests = filteredRequests.Where(a =>
                    a.Location.City.Contains(tourRequestSearch.City, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (tourRequestSearch.Country != null)
            {
                filteredRequests = filteredRequests.Where(a =>
                    a.Location.Country.Contains(tourRequestSearch.Country, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (tourRequestSearch.Status != null)
            {
                filteredRequests = filteredRequests.Where(a =>
                    a.RequestStatus == tourRequestSearch.Status).ToList();
            }

            if (tourRequestSearch.MaxTourists != 0)
            {
                filteredRequests = filteredRequests.Where(a =>
                    a.MaxTourists >= tourRequestSearch.MaxTourists).ToList();
            }

            if (tourRequestSearch.StartDate != null && tourRequestSearch.EndDate != null)
            {
                filteredRequests = filteredRequests.Where(a => a.StartDate >= tourRequestSearch.StartDate &&
                     a.EndDate <= tourRequestSearch.EndDate).ToList();
            }

            return filteredRequests;
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
        public Location GetMostRequestedLocationLastYear()
        {
            DateTime lastYearDate = DateTime.Now.AddYears(-1);
            var requestsLastYear = _tourRequest.Where(tr => tr.StartDate >= lastYearDate).ToList();

            var locationCount = requestsLastYear
                .GroupBy(tr => new { tr.Location.City, tr.Location.Country })
                .Select(group => new { Location = group.Key, Count = group.Count() })
                .OrderByDescending(item => item.Count)
                .FirstOrDefault();

            if (locationCount != null && locationCount.Count > 0)
            {
                var mostRequestedLocation = new Location
                {
                    City = locationCount.Location.City,
                    Country = locationCount.Location.Country
                };

                return mostRequestedLocation;
            }

            return null;
        }

        public string GetMostRequestedLanguageLastYear()
        {
            DateTime lastYearDate = DateTime.Now.AddYears(-1);
            var requestsLastYear = _tourRequest.Where(tr => tr.StartDate >= lastYearDate).ToList();

            var languageCount = new Dictionary<string, int>();

            foreach (var request in requestsLastYear)
            {
                if (!languageCount.ContainsKey(request.Language))
                {
                    languageCount[request.Language] = 1;
                }
                else
                {
                    languageCount[request.Language]++;
                }
            }

            var mostRequestedLanguage = languageCount.OrderByDescending(kv => kv.Value).FirstOrDefault().Key;

            return mostRequestedLanguage;
        }


        public List<TourRequest> GetAllTourRequestsForUser(int userId)
        {
            List<TourRequest> myRequests = new List<TourRequest>();
            var allRequests = GetAll();

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Tourist.Id == userId)
                {
                    if ((request.StartDate - DateTime.Today).TotalDays <= 2 && request.RequestStatus == RequestStatusType.Standby)
                    {
                        request.RequestStatus = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), "Declined");
                        Update(request);
                    }
                    myRequests.Add(request);
                }
            }

            return myRequests;
        }


        public List<int> YearsOfTourRequests(int guestId)
        {
            List<int> years = new List<int>();
            foreach (TourRequest tourRequest in GetAll())
            {
                if (tourRequest.Tourist.Id == guestId)
                {
                    years.Add(tourRequest.StartDate.Year);
                }
            }
            return years.Distinct().ToList();
        }

        public TourRequestPercentageDto GetPercentageOfTourRequest(int userId)
        {
            int acceptedRequests = 0;
            int rejectedRequests = 0;
            int numberOfPeopleInAcceptedRequests = 0;

            TourRequestPercentageDto tourRequestPercentage = new TourRequestPercentageDto(0, 0, 0);

            foreach (TourRequest tourRequest in GetAllTourRequestsForUser(userId))
            {
                if (tourRequest.RequestStatus == RequestStatusType.Approved)
                {
                    acceptedRequests += 1;
                    numberOfPeopleInAcceptedRequests += tourRequest.MaxTourists;
                }
                else if (tourRequest.RequestStatus == RequestStatusType.Declined)
                {
                    rejectedRequests += 1;
                }
            }

            CalculateRequestPercentage(tourRequestPercentage, acceptedRequests, rejectedRequests, numberOfPeopleInAcceptedRequests);

            return tourRequestPercentage;
        }

        public TourRequestPercentageDto GetPercentageOfTourRequestForYear(int userId, int year)
        {
            int acceptedRequests = 0;
            int rejectedRequests = 0;
            int numberOfPeopleInAcceptedRequests = 0;

            TourRequestPercentageDto tourRequestPercentage = new TourRequestPercentageDto(0, 0, 0);

            foreach (TourRequest tourRequest in GetAllTourRequestsForUser(userId))
            {
                if (tourRequest.StartDate.Year == year)
                {
                    if (tourRequest.RequestStatus == RequestStatusType.Approved)
                    {
                        acceptedRequests += 1;
                        numberOfPeopleInAcceptedRequests += tourRequest.MaxTourists;
                    }
                    else if (tourRequest.RequestStatus == RequestStatusType.Declined)
                    {
                        rejectedRequests += 1;
                    }
                }

            }

            CalculateRequestPercentage(tourRequestPercentage, acceptedRequests, rejectedRequests, numberOfPeopleInAcceptedRequests);

            return tourRequestPercentage;
        }

        private void CalculateRequestPercentage(TourRequestPercentageDto tourRequestPercentage, int acceptedRequests, int rejectedRequests, int numberOfPeopleInAcceptedRequests)
        {
            double totalRequests = acceptedRequests + rejectedRequests;

            if (totalRequests > 0)
            {
                double acceptedRequestsPercentage = (acceptedRequests * 100.0) / totalRequests;   //decimal bolje
                double rejectedRequestsPercentage = (rejectedRequests * 100.0) / totalRequests;
                tourRequestPercentage.PercentageOfAcceptedRequests = (int)Math.Round(acceptedRequestsPercentage);
                tourRequestPercentage.PercentageOfRejectedRequests = (int)Math.Round(rejectedRequestsPercentage);

                if (acceptedRequests > 0)
                {
                    tourRequestPercentage.AverageNumberOfPeopleInAcceptedRequests = numberOfPeopleInAcceptedRequests / acceptedRequests;
                }
            }
        }

        public int CountRequestsByLocationForTourist(Location location, int id)
        {
            List<TourRequest> tourRequests = GetAllTourRequestsForUser(id);
            return tourRequests.Count(tr => tr.Location.Id == location.Id);
        }

        public int CountRequestsByLanguageForTourist(string language, int id)
        {
            List<TourRequest> tourRequests = GetAllTourRequestsForUser(id);
            return tourRequests.Count(tr => tr.Language == language);
        }

        public int CountRequestForTourist(int id)
        {
            List<TourRequest> tourRequests = GetAllTourRequestsForUser(id);
            return tourRequests.Count();
        }

    }


}

