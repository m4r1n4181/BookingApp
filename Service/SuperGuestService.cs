using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class SuperGuestService
    {
        private readonly ISuperGuestRepository _superGuestRepository;

        public SuperGuestService( )
        {
            _superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();
        }

        public SuperGuest GetById(int id)
        {
            return _superGuestRepository.GetById(id);
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestRepository.GetAll();
        }

        public SuperGuest Save(SuperGuest superGuest)
        {
            return _superGuestRepository.Save(superGuest);
        }

        public SuperGuest GetByUserId(int userId)
        {
            return _superGuestRepository.GetByUserId(userId);
        }
    }
}

