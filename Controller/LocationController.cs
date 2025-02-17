﻿using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class LocationController
    {
        private LocationService _locationService;

        public LocationController()
        {
            _locationService = new LocationService();
        }

        public List<Location> GetAll()
        {
            return _locationService.GetAll();
        }

        public Location Get(int id)
        {
            return _locationService.Get(id);
        }

        public int GetIdByCityAndCountry(string city, string country)
        {
            return _locationService.GetIdByCityAndCountry(city, country);
        }
    }

}
