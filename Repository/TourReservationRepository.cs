using BookingApp.Model;
using BookingApp.Model.Enums;
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
        public TourRepository _tourRepository;
        

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            TourReservations = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }

        public void BindTours()
        {
            TourRepository tourRepository = new TourRepository();
            TourReservations.ForEach(tR => { tR.Tour = tourRepository.GetById(tR.Tour.Id); });
        }

        /*public void BindTourParticipants()
        {
            TourParticipantRepository tourParticipantsRepository = new TourParticipantRepository();
            TourReservations.ForEach(tR => { tourParticipantsRepository.GetById(tR.Tour.Id); });
        }*/

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

        public void Delete(TourReservation tourreservation)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            TourReservation founded = TourReservations.Find(c => c.Id == tourreservation.Id);
            TourReservations.Remove(founded);
            _serializer.ToCSV(FilePath, TourReservations);
        }

        public List<TourReservation> GetByTour(int tourId)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            return TourReservations.FindAll(c => c.Tour.Id == tourId);
        }

        public List<TourReservation> GetByUser(int id)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            BindTours();
            return TourReservations.FindAll(c => c.UserId == id);
        }

        public TourStatusType GetTourStatus(TourReservation reservation)
        {
            Tour tour = _tourRepository.GetById(reservation.Tour.Id);
            return tour.TourStatus;
        }

    }
}
