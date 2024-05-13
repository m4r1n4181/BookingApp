using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        void BindVoucher();
        Voucher GetById(int id);
        List<Voucher> GetAll();
        Voucher Save(Voucher voucher);
        int NextId();
        void Delete(Voucher voucher);
        Voucher Update(Voucher voucher);
        List<Voucher> GetAllByTourist(int touristId);
        List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers);
        List<Voucher> GetVouchersThatDidntExpire(int userId);
    }
}
