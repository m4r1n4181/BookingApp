using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class TouristRepository
    {

        private const string FilePath = "../../../Resources/Data/tourist.csv";

        private readonly Serializer<Tourist> _serializer;

        public List<Tourist> _tourist = new List<Tourist>();

        public TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourist = _serializer.FromCSV(FilePath);
        }

        public Tourist GetById(int id)
        {
            _tourist = _serializer.FromCSV(FilePath);
            return _tourist.FirstOrDefault(tourist => tourist.Id == id);
        }
        public List<Tourist> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tourist Save(Tourist tourist)
        {
            tourist.Id = NextId();
            _tourist = _serializer.FromCSV(FilePath);
            _tourist.Add(tourist);
            _serializer.ToCSV(FilePath, _tourist);
            return tourist;
        }

        public int NextId()
        {
            _tourist = _serializer.FromCSV(FilePath);
            if (_tourist.Count < 1)
            {
                return 1;
            }
            return _tourist.Max(c => c.Id) + 1;
        }

        public void Delete(Tourist tourist)
        {
            _tourist = _serializer.FromCSV(FilePath);
            Tourist founded = _tourist.Find(t => t.Id == tourist.Id);
            _tourist.Remove(founded);
            _serializer.ToCSV(FilePath, _tourist);
        }

        public Tourist Update(Tourist tourist)
        {
            _tourist = _serializer.FromCSV(FilePath);
            Tourist current = _tourist.Find(t => t.Id == tourist.Id);
            int index = _tourist.IndexOf(current);
            _tourist.Remove(current);
            _tourist.Insert(index, tourist);
            _serializer.ToCSV(FilePath, _tourist);
            return tourist;
        }

        public Tourist GetByUserId(int id)
        {
            _tourist = _serializer.FromCSV(FilePath);
            return _tourist.FirstOrDefault(tourist => tourist.UserId == id);
        }


    }
}