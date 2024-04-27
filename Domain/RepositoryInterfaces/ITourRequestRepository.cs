using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest Save(TourRequest tourRequest);
        void Delete(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);
    }
}
