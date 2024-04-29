using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _location;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _location = _serializer.FromCSV(FilePath);
        }



        public Location GetById(int id)
        {
            _location = _serializer.FromCSV(FilePath);
            return _location.FirstOrDefault(location => location.Id == id);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _location = _serializer.FromCSV(FilePath);
            _location.Add(location);
            _serializer.ToCSV(FilePath, _location);
            return location;
        }

        public int NextId()
        {
            _location = _serializer.FromCSV(FilePath);
            if (_location.Count < 1)
            {
                return 1;
            }
            return _location.Max(location => location.Id) + 1;
        }

        public void Delete(Location location)
        {
            _location = _serializer.FromCSV(FilePath);
            Location founded = _location.Find(location => location.Id == location.Id);
            _location.Remove(founded);
            _serializer.ToCSV(FilePath, _location);
        }

        public Location Update(Location location)
        {
            _location = _serializer.FromCSV(FilePath);
            Location current = _location.Find(location => location.Id == location.Id);
            int index = _location.IndexOf(current);
            _location.Remove(current);
            _location.Insert(index, location);
            _serializer.ToCSV(FilePath, _location);
            return location;
        }
        public Location Get(int id)
        {

            return _location.Find(x => x.Id == id);

        }
    }
}
