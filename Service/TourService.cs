using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Service
{
    public class TourService
    {
        private TourRepository _tourRepository;

        public TourService()
        {
            _tourRepository = new TourRepository();
        }

    public Tour CreateTour(Tour tour)
        {
            tour = _tourRepository.Save(tour);

            return tour;

        }

    }

}
