using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class KeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/keyPoint.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private List<KeyPoint> _keyPoint;

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _keyPoint = _serializer.FromCSV(FilePath);
        }
        public void BindTours()
        {
            TourRepository tourRepository = new TourRepository();
            _keyPoint.ForEach(kp => kp.Tour = tourRepository.GetById(kp.Tour.Id));
        }
        public KeyPoint GetById(int id)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            return _keyPoint.FirstOrDefault(keyPoint => keyPoint.Id == id);
        }

        public KeyPoint GetByIdWithTour(int id)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            BindTours();
            return _keyPoint.FirstOrDefault(keyPoint => keyPoint.Id == id);
        }
        public List<KeyPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoint = _serializer.FromCSV(FilePath);
            _keyPoint.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoint);
            return keyPoint;
        }
        public void SaveAll(List<KeyPoint> keyPoints)
        {
            foreach (KeyPoint keyPoint in keyPoints)
            {
                Save(keyPoint);
            }
        }

        public int NextId()
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            if (_keyPoint.Count < 1)
            {
                return 1;
            }
            return _keyPoint.Max(keyPoint => keyPoint.Id) + 1;
        }

        public void Delete(KeyPoint keyPoint)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            KeyPoint founded = _keyPoint.Find(kp => kp.Id == keyPoint.Id);
            _keyPoint.Remove(founded);
            _serializer.ToCSV(FilePath, _keyPoint);
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            KeyPoint current = _keyPoint.Find(kp => kp.Id == keyPoint.Id);
            int index = _keyPoint.IndexOf(current);
            _keyPoint.Remove(current);
            _keyPoint.Insert(index, keyPoint);
            _serializer.ToCSV(FilePath, _keyPoint);
            return keyPoint;
        }

        public List<KeyPoint> GetKeyPointsForTour(int tourId)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            return _keyPoint.FindAll(kp => kp.Tour.Id == tourId);

        } 


    }
}
