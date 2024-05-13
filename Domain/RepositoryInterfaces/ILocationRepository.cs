using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {

        public Location GetById(int id);

        public List<Location> GetAll();

        public Location Save(Location location);

        public int NextId();
        public void Delete(Location location);

        public Location Update(Location location);
        public Location Get(int id);

        public int GetIdByCityAndCountry(string city, string country);
    }
}
