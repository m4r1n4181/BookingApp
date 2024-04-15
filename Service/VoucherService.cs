using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class VoucherService
    {
        private VoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = new VoucherRepository();
        }
        public Voucher Save(Voucher voucher)
        {

            return _voucherRepository.Save(voucher);
        }


        public Voucher Update(Voucher voucher)
        {
            return _voucherRepository.Update(voucher);
        }

        public void Delete(Voucher voucher)
        {

            _voucherRepository.Delete(voucher);

        }

        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }
        public Voucher GetById(int id)
        {

            return _voucherRepository.GetById(id);

        }
        public List<Voucher> GetAllByTourist(int id)
        {

            return _voucherRepository.GetAllByTourist(id);

        }

        public List<Voucher> GetVouchersThatDidntExpire(int userId)
        {
            List<Voucher> voucherList = new List<Voucher>();
            var allVouchers = _voucherRepository.GetAll();

            foreach (var voucher in allVouchers)
            {
                if (voucher.Tourist.Id == userId && voucher.StatusType==Model.Enums.StatusType.active && voucher.Expires >= DateTime.Now)
                {
                    voucherList.Add(voucher);
                }
            }

            return voucherList;
        }

    }
}
