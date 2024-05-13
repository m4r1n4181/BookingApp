using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristEntryRepository
    {
        void BindTours();
        List<TouristEntry> GetAll();
        void BindKeyPoint();
        TouristEntry GetById(int id);
        List<TouristEntry> GetAllWithTours();
        TouristEntry Save(TouristEntry touristEntry);
        int NextId();
        TouristEntry Update(TouristEntry touristEntry);
        List<TouristEntry> GetAllByKeyPoint(int keyPointId);
        TouristEntry GetByTourAndTourist(int tourId, int touristId);
    }
}
