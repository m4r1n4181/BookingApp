using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class VoucherController
    {
        private readonly VoucherService _voucherService;

        public VoucherController()
        {
            _voucherService = new VoucherService();
        }

        public Voucher Save(Voucher voucher)
        {

            return _voucherService.Save(voucher);
        }


        public Voucher Update(Voucher voucher)
        {
            return _voucherService.Update(voucher);
        }

        public List<Voucher> GetAll()
        {
            return _voucherService.GetAll();
        }

        public Voucher GetById(int id)
        {

            return _voucherService.GetById(id);

        }

        public List<Voucher> GetAllByTourist(int id)
        {

            return _voucherService.GetAllByTourist(id);

        }

        public void Delete(Voucher voucher)
        {
            _voucherService.Delete(voucher);
        }


        public List<Voucher> GetVouchersThatDidntExpire(int id)
        {
            return _voucherService.GetVouchersThatDidntExpire(id);
        }

        /* public List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers)
         {
             return _voucherService.GetVouchersThatArentUsed(vouchers);
         }*/
    }
}
