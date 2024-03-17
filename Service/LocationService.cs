using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class LocationService
    {
        private LocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = new LocationRepository();
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}
