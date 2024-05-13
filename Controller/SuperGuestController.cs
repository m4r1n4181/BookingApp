using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class SuperGuestController
    {
        private readonly SuperGuestService _superGuestService;
        private AccommodationReservationService _recommendationReservationService;

        public SuperGuestController()
        {
            _superGuestService = new SuperGuestService();
        }

        public SuperGuest GetById(int id)
        {
            return _superGuestService.GetById(id);
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestService.GetAll();
        }

        public SuperGuest SaveSuperGuest(SuperGuest superGuest)
        {
            return _superGuestService.Save(superGuest);
        }

        public SuperGuest GetByUserId(int userId)
        {
            return _superGuestService.GetByUserId(userId);
        }
    }
}
