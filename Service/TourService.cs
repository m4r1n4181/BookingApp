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
        private TourRepository tourRepository;

        public TourService(TourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }
        //metoda da se kreira pa doda tura u repository
        public void Create(int id,
            string name,
            string description,
            string language,
            int maxTourists,
            List<DateTime> startDates,
            int duration,
            List<string> images,
            bool isStarted)
        {
            // Validacija ili dodatne provere pre kreiranja ture

            // Kreiranje nove ture
            Tour newTour = new Tour(id, name, description, language, maxTourists, startDates, duration, images, isStarted);

            // Dodavanje ture u repository
            tourRepository.Add(newTour);
        }

        public List<Tour> GetAllTours()
        {
            return tourRepository.GetAllTours();
        }
    }

}
