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

        public TouristEntryService()
        {
            _touristEntryRepository = new TouristEntryRepository();
        }

        public void AddTouristEntry(TouristEntry touristEntry)
        {
            _touristEntryRepository.Save(touristEntry);
        }
    }
}
