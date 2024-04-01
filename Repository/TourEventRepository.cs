using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourEventRepository
    {

        private const string FilePath = "../../../Resources/Data/tourEvent.csv";



        private readonly Serializer<TourEvent> _serializer;

        public List<TourEvent> _tourEvents;

        public TourEventRepository()
        {

            _serializer = new Serializer<TourEvent>();
            _tourEvents = _serializer.FromCSV(FilePath);
        }



        public void BindTourEventTour()
        {
            TourRepository tourRepository = new TourRepository();
            _tourEvents.ForEach(tourEvent => { tourEvent.Tour = tourRepository.GetById(tourEvent.Tour.Id); });

        }
        public TourEvent Save(TourEvent tourEvent)
        {
            tourEvent.Id = NextId();
            //_tourEvents = _serializer.FromCSV(FilePath);
            _tourEvents.Add(tourEvent);
            _serializer.ToCSV(FilePath, _tourEvents);
            return tourEvent;

        }

        public int NextId()
        {
            //_tourEvents = _serializer.FromCSV(FilePath);
            if (_tourEvents.Count < 1)
            {
                return 1;
            }
            return _tourEvents.Max(te => te.Id) + 1;
        }

        public void Delete(TourEvent tourEvent)
        {
            //_tourEvents = _serializer.FromCSV(FilePath);
            TourEvent founded = _tourEvents.Find(tp => tp.Id == tourEvent.Id);
            _tourEvents.Remove(founded);
            _serializer.ToCSV(FilePath, _tourEvents);
        }

        public TourEvent Update(TourEvent tourEvent)
        {
            //_tourEvents = _serializer.FromCSV(FilePath);
            TourEvent current = _tourEvents.Find(te => te.Id == tourEvent.Id);
            int index = _tourEvents.IndexOf(current);
            _tourEvents.Remove(current);
            _tourEvents.Insert(index, tourEvent);
            _serializer.ToCSV(FilePath, _tourEvents);
            return tourEvent;
        }

        public List<TourEvent> GetAll()
        {

            return _tourEvents;

        }
        public TourEvent GetById(int id)
        {
            _tourEvents = _serializer.FromCSV(FilePath);
            return _tourEvents.FirstOrDefault(tourEvent => tourEvent.Id == id);
        }


        public List<TourEvent> GetByTour(int tourId)
        {
            return _tourEvents.FindAll(t => t.Tour.Id == tourId);
        }
    }
}
