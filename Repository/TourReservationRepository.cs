using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-reservation.csv";
        private readonly Serializer<TourReservation> _serializer;
        public List<TourReservation> _tourReservations;
        public List<TourParticipants> _tourParticipants;
        public TourRepository _tourRepository;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }



        public TourReservation GetById(int id)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            BindTourists();
            BindKeyPoints();
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

        public void BindKeyPoints()
        {
            TouristEntryRepository touristEntryRepository = new TouristEntryRepository();
            _tourReservations.ForEach(tr => tr.TouristEntry = touristEntryRepository.GetById(tr.TouristEntry.Id));
        }

        public void BindTours() //bind touru sa njegovim rezervacijama 
        {
            TourRepository tourRepository = new TourRepository();
            foreach (var tourReservation in _tourReservations)
            {
                tourReservation.Tour = tourRepository.GetById(tourReservation.Tour.Id);
            }
        }

        public void BindTourists() //bind touru sa njegovim rezervacijama 
        {
            TouristRepository touristRepository = new TouristRepository();
            foreach (var tourReservation in _tourReservations)
            {
                tourReservation.Tourist = touristRepository.GetById(tourReservation.Tourist.Id);
            }
        }
        public void BindParticipants()  
        {
            TourParticipantRepository tourParticipantRepository = new TourParticipantRepository();
            foreach (var tourReservation in _tourReservations)
            {
                tourReservation.Tourists = tourParticipantRepository.FindParticipants(tourReservation.Tourists);
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

        public List<TourReservation> GetByTour(int tourId) // sve rezervacije za tu turu 
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            BindTourists();
            return _tourReservations.FindAll(c => c.Tour.Id == tourId);
        }
        public List<TourReservation> GetAllParticipants(int reservationId) //uzela sve participants za jednu tu rez
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.Where(t => t.Tourists.Any(p => p.Id == reservationId)).ToList();
        }

        public TourReservation GetByTourAndTourist(int tourId, int touristId)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            BindParticipants();
            return _tourReservations.FirstOrDefault(tr => tr.Tour.Id == tourId && tr.Tourist.Id == touristId);
        }

        public List<TourReservation> GetByUser(int id)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            BindTours();
            return _tourReservations.FindAll(c => c.UserId == id);
        }

        public TourStatusType GetTourStatus(TourReservation reservation)
        {
            Tour tour = _tourRepository.GetById(reservation.Tour.Id);
            return tour.TourStatus;
        }


    }
}
