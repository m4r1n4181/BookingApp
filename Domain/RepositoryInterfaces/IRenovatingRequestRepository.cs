using BookingApp.Domain.Models;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovatingRequestRepository
    {
        public RenovatingRequest GetById(int id);

        public List<RenovatingRequest> GetAll();
        List<RenovatingRequest> GetAllWithAccommodationReservation();
        RenovatingRequest Save(RenovatingRequest renovatingRequest);
    }
}
