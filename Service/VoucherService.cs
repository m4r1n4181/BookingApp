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

        public List<Voucher> GetVouchersThatDidntExpire()
        {
            List<Voucher> voucherList = new List<Voucher>();
            var allVouchers = _voucherRepository.GetAll();
            for (int i = 0; i < allVouchers.Count(); i++)
            {
                var voucher = allVouchers.ElementAt(i);
                if (voucher.ExpirationDate >= DateTime.Now)
                {
                    voucherList.Add(voucher);
                }
                else
                {
                    //ako vaucer nije iskoristen
                    var unusedVouchers = GetVouchersThatArentUsed(allVouchers);
                    if (voucher.ExpirationDate >= DateTime.Now)
                    {
                        foreach (Voucher unusedVoucher in unusedVouchers)
                        {
                            _voucherRepository.Delete(unusedVoucher);
                        }
                    }

                }
            }
            return voucherList;
        }

        public List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers)
        {
            List<Voucher> voucherList = new List<Voucher>();
            foreach (Voucher voucher in vouchers)
            {
                if (voucher.IsUsed == false)
                {
                    voucherList.Add(voucher);
                }
            }
            return voucherList;
        }
    }
}