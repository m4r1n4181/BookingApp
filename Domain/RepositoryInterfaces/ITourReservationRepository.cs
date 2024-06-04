using BookingApp.Model.Enums;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        TourReservation GetById(int id);
        List<TourReservation> GetAll();
        List<TourReservation> GetAllWithTours();
        TourReservation Save(TourReservation tourReservation);
        int NextId();
        TourReservation Update(TourReservation tourReservation);
        void Delete(TourReservation tourReservation);
        List<TourReservation> GetByTour(int tourId);
        List<TourReservation> GetAllParticipants(int reservationId);
        TourReservation GetByTourAndTourist(int tourId, int touristId);
        List<TourReservation> GetByUser(int id);
        TourStatusType GetTourStatus(TourReservation reservation);

        public List<TourReservation> GetByGuideAndLanguage(int guideId, string language, DateTime lastYear);
    }
}
