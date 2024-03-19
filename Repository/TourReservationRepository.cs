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
        public List<TourReservation> TourReservations;
        

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            TourReservations = _serializer.FromCSV(FilePath);
        }

        public void BindTours()
        {
            TourRepository tourRepository = new TourRepository();
            TourReservations.ForEach(tR => { tR.Tour = tourRepository.GetById(tR.Tour.Id); });
        }

        public TourReservation GetById(int id)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            return TourReservations.FirstOrDefault(t => t.Id == id);
        }
        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAllWithTours()
        {
            TourReservations = _serializer.FromCSV(FilePath);
            BindTours();
            return TourReservations;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            TourReservations = _serializer.FromCSV(FilePath);
            TourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, TourReservations);
            return tourReservation;
        }

        public int NextId()
        {
            TourReservations = _serializer.FromCSV(FilePath);
            if (TourReservations.Count < 1)
            {
                return 1;
            }
            return TourReservations.Max(a => a.Id) + 1;
        }
     
        public TourReservation Update(TourReservation tourReservation)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            TourReservation current = TourReservations.Find(t => t.Id == tourReservation.Id);
            int index = TourReservations.IndexOf(current);
            TourReservations.Remove(current);
            TourReservations.Insert(index, tourReservation);
            _serializer.ToCSV(FilePath, TourReservations);
            return tourReservation;
        }
     
        public List<TourReservation> GetByTour(int tourId)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            return TourReservations.FindAll(c => c.Tour.Id == tourId);
        }

    }
}
