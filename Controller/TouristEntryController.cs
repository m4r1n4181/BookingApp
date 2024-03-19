using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TouristEntryController
    {
        private TouristEntryService _touristEntryService;

        public TouristEntryController()
        {
            _touristEntryService = new TouristEntryService();
        }

        public void AddTouristEntry(TouristEntry touristEntry)
        {
            
            _touristEntryService.AddTouristEntry(touristEntry);

            
        }
    }
}
