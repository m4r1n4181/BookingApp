using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Voucher : ISerializable
    {

        public int Id { get; set; }
        public User TourGuide { get; set; }
        public Tourist Tourist { get; set; }
        public bool IsUsed { get; set; }
        public int Duration { get; set; }

        public VoucherType Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Voucher() { }

        public Voucher(int id, User tourGuide, Tourist tourist, bool isUsed, int duration, VoucherType type, DateTime expirationDate)    
        {
            Id = id;
            TourGuide = tourGuide;
            Tourist = tourist;
            IsUsed = isUsed;
            Duration = duration;
            Type = type;
            ExpirationDate = expirationDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourGuide.Id.ToString(), Tourist.Id.ToString(), IsUsed.ToString(), Duration.ToString(), Type.ToString(), ExpirationDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourGuide = new User(Convert.ToInt32(values[1]));
            Tourist = new Tourist(Convert.ToInt32(values[2]));
            IsUsed = bool.Parse(values[3]);
            Duration = Convert.ToInt32(values[4]);
            Enum.TryParse(values[5], out VoucherType voucherType);
            Type = voucherType;
            ExpirationDate = Convert.ToDateTime(values[6]);
        }
    }


}