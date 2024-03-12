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
            KeyPoint founded = _keyPoint.Find(keyPoint => keyPoint.Id == keyPoint.Id);
            _keyPoint.Remove(founded);
            _serializer.ToCSV(FilePath, _keyPoint);
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            _keyPoint = _serializer.FromCSV(FilePath);
            KeyPoint current = _keyPoint.Find(keyPoint => keyPoint.Id == keyPoint.Id);
            int index = _keyPoint.IndexOf(current);
            _keyPoint.Remove(current);
            _keyPoint.Insert(index, keyPoint);
            _serializer.ToCSV(FilePath, _keyPoint);
            return keyPoint;
        }


    }
}
