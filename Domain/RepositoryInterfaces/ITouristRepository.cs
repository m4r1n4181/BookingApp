using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        Tourist GetById(int id);
        List<Tourist> GetAll();
        Tourist Save(Tourist tourist);
        int NextId();
        void Delete(Tourist tourist);
        Tourist Update(Tourist tourist);
        Tourist GetByUserId(int id);
    }
}
