// TourReservationRepository.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-reservation.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> TourReservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            TourReservations = _serializer.FromCSV(FilePath);
        }

        public void BindReservations()
        {
            TourRepository tourRepository = new TourRepository();   
            TourReservations.ForEach(touR => { touR.Tour = tourRepository.GetById(touR.Tour.Id); });
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAllTours()
        {
            TourReservations = _serializer.FromCSV(FilePath);
            BindReservations();
            return TourReservations;
        }

        // Metoda za čuvanje rezervacija tura
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
            return TourReservations.Max(reservation => reservation.Id) + 1;
        }

        public void Delete(TourReservation tourReservation)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            TourReservation founded = TourReservations.Find(reservation => reservation.Id == tourReservation.Id);
            TourReservations.Remove(founded);
            _serializer.ToCSV(FilePath, TourReservations);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            TourReservations = _serializer.FromCSV(FilePath);
            TourReservation current = TourReservations.Find(reservation => reservation.Id == tourReservation.Id);
            int index = TourReservations.IndexOf(current);
            TourReservations.Remove(current);
            TourReservations.Insert(index, tourReservation);
            _serializer.ToCSV(FilePath, TourReservations);
            return tourReservation;
        }
    }
}
