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
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-reservation.csv";
        private readonly Serializer<TourReservation> _serializer;
        public List<TourReservation> _tourReservations;
        

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

       

        public TourReservation GetById(int id)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.FirstOrDefault(t => t.Id == id);
        }
        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAllWithTours()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            BindTours(); // Popunjava Tour za svaku TourReservation
            return _tourReservations;
        }

        private void BindTours()
        {
            TourRepository tourRepository = new TourRepository();
            foreach (var tourReservation in _tourReservations)
            {
                tourReservation.Tour = tourRepository.GetById(tourReservation.Tour.Id);
            }
        }


        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations = _serializer.FromCSV(FilePath);
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }

        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(a => a.Id) + 1;
        }
     
        public TourReservation Update(TourReservation tourReservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation current = _tourReservations.Find(t => t.Id == tourReservation.Id);
            int index = _tourReservations.IndexOf(current);
            _tourReservations.Remove(current);
            _tourReservations.Insert(index, tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
           
            return tourReservation;
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation founded = _tourReservations.Find(tR => tR.Id == tourReservation.Id);
            _tourReservations.Remove(founded);
            _serializer.ToCSV(FilePath, _tourReservations);
        }

        public List<TourReservation> GetByTour(int tourId)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.FindAll(c => c.Tour.Id == tourId);
        }

    }
}
