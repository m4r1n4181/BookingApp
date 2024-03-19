﻿using BookingApp.Service;
using BookingApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.DTO;
namespace BookingApp.Controller
{
    public class TourController
    {
        private TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }

        public void EndTour(int id)
        {
            _tourService.EndTour(id);
        }

        public void CreateTour(Tour tour)
        {
            _tourService.CreateTour(tour);

        public List<Tour> GetAllWithLocations()
        {
            return _tourService.GetAllWithLocations();
        }

        public List<Tour> GetTodayTours()
        {
            return _tourService.GetTodayTours();
        }

        public void StartTour(int id)
        {
            _tourService.StartTour(id);
        }

    }


}
