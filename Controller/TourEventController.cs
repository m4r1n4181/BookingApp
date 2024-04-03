using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{

    public class TourEventController
    {

        private readonly TourEventService _tourEventService;

        public TourEventController()
        {
            _tourEventService = new TourEventService();
        }

        public List<TourEvent> GetTourEventsInFuture()
        {
            return _tourEventService.GetTourEventsInFuture();
        }

        public List<TourEvent> GetTourEventsForNow()
        {
            return _tourEventService.GetTourEventsForNow();
        }

    }
 }
