using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TouristEntryService
    {
        private TouristEntryRepository _touristEntryRepository;
        private TourRepository _tourRepository;

        public TouristEntryService()
        {
            _touristEntryRepository = new TouristEntryRepository();
            _tourRepository = new TourRepository();
        }

        public void AddTouristEntry(TouristEntry touristEntry)
        {
            int tourId = touristEntry.KeyPoint.Tour.Id;
            Tour tour = _tourRepository.GetById(tourId);

            touristEntry.Tour = tour;
            _touristEntryRepository.Save(touristEntry);
        }
    }
}
