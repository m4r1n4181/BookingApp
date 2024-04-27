using BookingApp.Domain.Models;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISuperGuestRepository
    { 
        public SuperGuest GetById(int id);

        public List<SuperGuest> GetAll();

        public SuperGuest Save(SuperGuest superGuest);
        public SuperGuest GetByUserId(int userId);

        public void Delete(SuperGuest superGuest);
        public SuperGuest Update(SuperGuest superGuest);
    }
}
