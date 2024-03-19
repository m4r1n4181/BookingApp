using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/tourGuide.csv";

        private readonly Serializer<TourGuide> _serializer;

        private List<TourGuide> _tourGuide;

        public TourGuideRepository()
        {
            _serializer = new Serializer<TourGuide>();
            _tourGuide = _serializer.FromCSV(FilePath);
        }

        public List<TourGuide> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourGuide Save(TourGuide tourGuide)
        {
            tourGuide.Id = NextId();
            _tourGuide = _serializer.FromCSV(FilePath);
            _tourGuide.Add(tourGuide);
            _serializer.ToCSV(FilePath, _tourGuide);
            return tourGuide;
        }

        public int NextId()
        {
            _tourGuide = _serializer.FromCSV(FilePath);
            if (_tourGuide.Count < 1)
            {
                return 1;
            }
            return _tourGuide.Max(tourGuide => tourGuide.Id) + 1;
        }

        public void Delete(TourGuide tourGuide)
        {
            _tourGuide = _serializer.FromCSV(FilePath);
            TourGuide founded = _tourGuide.Find(tg => tg.Id == tourGuide.Id);
            _tourGuide.Remove(founded);
            _serializer.ToCSV(FilePath, _tourGuide);
        }

        public TourGuide Update(TourGuide tourGuide)
        {
            _tourGuide = _serializer.FromCSV(FilePath);
            TourGuide current = _tourGuide.Find(tg => tg.Id == tourGuide.Id);
            int index = _tourGuide.IndexOf(current);
            _tourGuide.Remove(current);
            _tourGuide.Insert(index, tourGuide);
            _serializer.ToCSV(FilePath, _tourGuide);
            return tourGuide;
        }


    }


}
