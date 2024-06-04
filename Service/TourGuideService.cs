using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourGuideService
    {
        private ITourGuideRepository _tourGuideRepository;
        private ITourRepository _tourRepository;
        private IVoucherRepository _voucherRepository;
        private ITourReservationRepository _tourReservationRepository;
        private readonly TourService _tourService;


        public TourGuideService()
        {
            _tourGuideRepository = Injector.CreateInstance<ITourGuideRepository>();
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourService = new TourService();

        }

        public List<TourGuide> GetAll()
        {
            return _tourGuideRepository.GetAll();
        }

        public TourGuide GetById(int id)
        {
            return _tourGuideRepository.GetById(id);
        }

        public void Resignation(int guideId)
        {
            TourGuide guide = _tourGuideRepository.GetById(guideId);
            if (guide != null)
            {
                CancelAllTourReservationsForGuide(guideId);
            }
        }

        private void CancelAllTourReservationsForGuide(int guideId)
        {
            List<Tour> futureTours = _tourService.GetFutureToursByGuideId(guideId);
            foreach (Tour tour in futureTours)
            {
                tour.TourStatus = TourStatusType.cancelled;
                _tourRepository.Update(tour);

                // Otkazivanje svih rezervacija za ovu turu
                _tourReservationRepository.GetByTour(tour.Id).ForEach(reservation => CancelTourReservation(reservation));
            }
        }

        private void CancelTourReservation(TourReservation tourReservation)
        {
            Tourist tourist = tourReservation.Tourist;
            DateTime expires = DateTime.Now.AddYears(2);
            Voucher voucher = new Voucher(-1, tourist, StatusType.active, expires, false, 1, VoucherType.resignation);
            _voucherRepository.Save(voucher);
        }

        public bool IsSuperGuide(int id)
        {
            TourGuide guide = _tourGuideRepository.GetById(id);

            if (guide != null)
            {
                return guide.IsSuperGuide;
            }
            else
            {
                return false; 
            }
        }
    }
}
