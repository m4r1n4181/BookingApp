using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        public Notification GetById(int id);
        public List<Notification> GetAll();

        public Notification Save(Notification notification);

        public int NextId();

        public void Delete(Voucher voucher);

        public Notification Update(Notification notification);

        public List<Notification> GetByUserId(int userId);
    }
}
