
using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    class TourParticipantService
    {

        private ITourParticipantRepository _tourParticipantRepository;
        private ITourReservationRepository _tourReservationRepository;

        public TourParticipantService()
        {
            _tourParticipantRepository = Injector.CreateInstance<ITourParticipantRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
        }
        public TourParticipants CreateParticipant(TourParticipants tourParticipant)
        {
            tourParticipant = _tourParticipantRepository.Save(tourParticipant);


            return tourParticipant;

        }
    }
}
