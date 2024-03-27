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
    }
}
