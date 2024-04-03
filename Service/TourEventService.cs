using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourEventService
    {
        private TourEventRepository _tourEventRepository;
        private TourReservationRepository _tourReservationRepository;
        private TourReservationService _tourReservationService;

        public TourEventService()
        {
            _tourEventRepository = new TourEventRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourReservationService = new TourReservationService();
        }

        public List<TourEvent> GetAll()
        {
            return _tourEventRepository.GetAll();
        }

  

        public TourEvent Save(TourEvent tourEvent)
        {

            return _tourEventRepository.Save(tourEvent);
        }

        public void Delete(TourEvent tourEvent)
        {

            _tourEventRepository.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventRepository.Update(tourEvent);
        }


        public List<TourEvent> GetByTour(int id)
        {

            return _tourEventRepository.GetByTour(id);
        }


        public List<TourEvent> GetTourEventsForNow()
        {
            List<TourEvent> _tourEventsForNow = new List<TourEvent>();

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {

                if (tourEvent.StartTime.Date == DateTime.Today)
                {
                    _tourEventsForNow.Add(tourEvent);
                }
            }
            return _tourEventsForNow;
        }
        public List<TourEvent> GetTourEventsInFuture()
        {
            List<TourEvent> _tourEventsForNow = new List<TourEvent>();

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if (tourEvent.StartTime.Date > DateTime.Today.AddDays(2)) //48sati pre pocetka ture
                {
                    _tourEventsForNow.Add(tourEvent);
                }
            }
            return _tourEventsForNow;
        }

        public void CancelTourEvent(TourEvent tourEvent)
        {
            _tourReservationService.CancelAllTourReservationsForTourEvent(tourEvent.Id);
            _tourEventRepository.Delete(tourEvent);
        }
    }
}
