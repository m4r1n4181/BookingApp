using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class LocationService
    {
        private ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository =Injector.CreateInstance<ILocationRepository>();
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location Get(int id)
        {
           return _locationRepository.Get(id);
        }

        public int GetIdByCityAndCountry(string city, string country)
        {
            return _locationRepository.GetIdByCityAndCountry( city,  country);
        }
    }
}
