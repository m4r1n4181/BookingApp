using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        void BindTours();
        KeyPoint GetById(int id);
        KeyPoint GetByIdWithTour(int id);
        List<KeyPoint> GetAll();
        KeyPoint Save(KeyPoint keyPoint);
        void SaveAll(List<KeyPoint> keyPoints);
        int NextId();
        void Delete(KeyPoint keyPoint);
        KeyPoint Update(KeyPoint keyPoint);
        List<KeyPoint> GetKeyPointsForTour(int tourId);
        List<KeyPoint> GetActiveKeyPointByTour(int tourId);
    }
}
