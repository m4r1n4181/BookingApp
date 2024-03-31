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

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;//Tours

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }
       /* public void BindDateTime()
        {
            foreach (var tour in _tours)
            {
                // Postavite svojstva Date i Time na osnovu liste StartDates iz ture
                if (tour.StartDates != null && tour.StartDates.Any())
                {
                    // Pretpostavka: StartDates je lista DateTime objekata
                    List<DateTime> = tour.StartDates.First().Date;
                }
            }
        }*/

        public List<Tour> GetAllWithDateTime()
        {
            _tours = _serializer.FromCSV(FilePath);
           // BindDateTime();
            return _tours;
        }

        
        public void BindLocations()
        {
            LocationRepository locationRepository = new LocationRepository();
            _tours.ForEach(t => t.Location = locationRepository.GetById(t.Location.Id));
        }

        public List<Tour> GetAllWithLocations()
        {
            _tours = _serializer.FromCSV(FilePath);
            BindLocations();
            return _tours;
        }

        public List<Tour> SearchTours(TourSearchParams searchParams)
        {
            _tours = _serializer.FromCSV(FilePath);
            BindLocations();

            if (!string.IsNullOrWhiteSpace(searchParams.City))
            {
                _tours = _tours.FindAll(tour => tour.Location != null && tour.Location.City.Contains(searchParams.City, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(searchParams.Country))
            {
                _tours = _tours.FindAll(tour => tour.Location != null && tour.Location.Country.Contains(searchParams.Country, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.Duration != 0)
            {
                _tours = _tours.FindAll(tour => tour.Duration >= searchParams.Duration);
            }

            if (!string.IsNullOrWhiteSpace(searchParams.Language))
            {
                _tours = _tours.FindAll(tour => tour.Language.Equals(searchParams.Language));
            }

            if (searchParams.MaxTourists != 0)
            {
                _tours = _tours.FindAll(tour => tour.MaxTourists >= searchParams.MaxTourists);
            }

            return _tours;
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(tour => tour.Id == id);
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(tour => tour.Id) + 1;
        }

        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour founded = _tours.Find(t => t.Id == tour.Id);
            _tours.Remove(founded);
            _serializer.ToCSV(FilePath, _tours);
        }

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(t => t.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);      
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public List<Tour> GetByTourGuide(TourGuide tourGuide)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(tour => tour.TourGuide.Id == tourGuide.Id);

        }

        private bool TourStartesToday(Tour tour)
        {
            return tour.StartDates.Any(date => date.Date == DateTime.Now.Date);
        }

        public List<Tour> GetTodayTours()
        {

            _tours = _serializer.FromCSV(FilePath);
            BindLocations();
            return _tours.FindAll(tour => TourStartesToday(tour) && !tour.IsStarted);
        }


    }


}