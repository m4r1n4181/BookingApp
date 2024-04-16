using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourParticipantController
    {
        private TourParticipantService _tourParticipantService;
        public TourParticipantController()
        {
            _tourParticipantService = new TourParticipantService();
        }
       /* public void AddParticipant(TourReservation reservation, TourParticipants participant)
        {
            _tourParticipantService.AddParticipant(reservation, participant);
        }*/
    }
}
