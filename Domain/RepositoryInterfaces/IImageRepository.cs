using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
   public interface IImageRepository
    {
        List<Image> GetAll();
        Image Get(int id);
        Image Save(Image image);
        void Delete(Image image);
        Image Update(Image image);
    }
}
