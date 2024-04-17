using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class VoucherRepository
    {

        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public void BindVoucher() //treba da bajndujem voucher sa turistom samo 
        {
            TouristRepository touristRepository = new TouristRepository();
            _vouchers.ForEach(voucher => { voucher.Tourist = touristRepository.GetById(voucher.Tourist.Id); });
        }
        public Voucher GetById(int id)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FirstOrDefault(voucher => voucher.Id == id);
        }

        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(v => v.Id) + 1;
        }

        public void Delete(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher founded = _vouchers.Find(v => v.Id == voucher.Id);
            _vouchers.Remove(founded);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public Voucher Update(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher current = _vouchers.Find(v => v.Id == voucher.Id);
            int index = _vouchers.IndexOf(current);
            _vouchers.Remove(current);
            _vouchers.Insert(index, voucher);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public List<Voucher> GetAllByTourist(int touristId)
        {
            return _vouchers.Where(voucher => voucher.Tourist.Id == touristId).ToList();
        }

        public List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers)
        {
            List<Voucher> voucherList = new List<Voucher>();
            foreach (Voucher voucher in vouchers)
            {
                if (!voucher.IsUsed)
                {
                    voucherList.Add(voucher);
                }
            }
            return voucherList;
        }

        public List<Voucher> GetVouchersThatDidntExpire(int userId)
        {
            List<Voucher> unusedVouchers = GetVouchersThatArentUsed(GetAll());
            List<Voucher> validVouchers = new List<Voucher>();

            foreach (Voucher voucher in unusedVouchers)
            {
                if (voucher.Tourist.Id == userId && voucher.StatusType == Model.Enums.StatusType.active && voucher.Expires >= DateTime.Now)
                {
                    validVouchers.Add(voucher);
                }
            }

            return validVouchers;
        }


    }
}