
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

        private TourParticipantRepository _tourParticipantRepository;
        private TourReservationRepository _tourReservationRepository;

        public TourParticipantService()
        {
            _tourParticipantRepository = new TourParticipantRepository();
            _tourReservationRepository = new TourReservationRepository();
        }
        public TourParticipants CreateParticipant(TourParticipants tourParticipant)
        {
            tourParticipant = _tourParticipantRepository.Save(tourParticipant);


            return tourParticipant;

        }
    }
}
