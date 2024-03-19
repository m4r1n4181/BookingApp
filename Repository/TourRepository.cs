using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private static TourRepository instance = null;
        private readonly Serializer<Tour> _serializer;
        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public static TourRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourRepository();
            }
            return instance;
        }

        public Tour Get(int id)
        {

            return _tours.Find(x => x.Id == id);

        }

        public Tour GetById(int id)
        {
            return _tours.FirstOrDefault(tour => tour.Id == id);
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(tour => tour.Id) + 1;
        }

        public void Delete(Tour tour)
        {
            _tours.Remove(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

        public Tour Update(Tour tour)
        {
            int index = _tours.FindIndex(t => t.Id == tour.Id);
            _tours[index] = tour;
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public List<Tour> GetByTourGuide(TourGuide tourGuide)
        {
            return _tours.FindAll(tour => tour.TourGuide.Id == tourGuide.Id);
        }

        private bool TourStartsToday(Tour tour)
        {
            return tour.StartDates.Any(date => date.Date == DateTime.Now.Date);
        }

        public List<Tour> GetTodayTours()
        {
            return _tours.FindAll(tour => TourStartsToday(tour) && !tour.IsStarted);
        }

        public void BindTourLocation()
        {
            foreach (Tour tour in _tours)
            {
                int locationId = tour.Location.Id;
                Location location = LocationRepository.GetInstance().Get(locationId);
                if (location != null)
                {
                    tour.Location = location;
                }
                else
                {
                    Console.WriteLine("Error in tourLocation binding");
                }
            }
        }
        public void BindLocations()
        {
            LocationRepository locationRepository = new LocationRepository();
            _tours.ForEach(locR=> {locR.Location = locationRepository.GetById(locR.Location.Id); });

        }
        public List<Tour> GetAllWithLocations()
        {
            _tours = _serializer.FromCSV(FilePath);
            BindLocations();
            return _tours;
        }

       /* public List<Tour> SearchTours(TourSearchParams searchParams)
        {
            BindTourLocation();

            List<Tour> filteredTours = _tours;

            if (!string.IsNullOrWhiteSpace(searchParams.City))
            {
                filteredTours = filteredTours.FindAll(tour => tour.Location.City.Contains(searchParams.City, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(searchParams.Country))
            {
                filteredTours = filteredTours.FindAll(tour => tour.Location.Country.Contains(searchParams.Country, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.Duration != 0)
            {
                filteredTours = filteredTours.FindAll(tour => tour.Duration >= searchParams.Duration);
            }

            if (!string.IsNullOrWhiteSpace(searchParams.Language))
            {
                filteredTours = filteredTours.FindAll(tour => tour.Language.Equals(searchParams.Language));
            }

            if (searchParams.MaxTourists != 0)
            {
                filteredTours = filteredTours.FindAll(tour => tour.MaxTourists >= searchParams.MaxTourists);
            }

            return filteredTours;
        }*/
    }
}
