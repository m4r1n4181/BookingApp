using BookingApp.Model.Enums;
using BookingApp.Serializer;
using Microsoft.Win32;
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
        public StatusType StatusType {  get; set; }   //status active used expired
        public bool IsUsed { get; set; }
        public int Duration { get; set; }

        public DateTime Expires { get; set; }

        public VoucherType Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Voucher() { }

        public Voucher(int id,User tourGuide, Tourist tourist, StatusType statusType, DateTime expires, bool isUsed, int duration,  VoucherType type)
        {
            Id = id;
            TourGuide = tourGuide;
            Tourist = tourist;
            StatusType = statusType;
            Expires = expires;
            IsUsed = isUsed;
            Duration = duration;
            Type = type;
            
 
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourGuide.Id.ToString(), Tourist.Id.ToString(), StatusType.ToString(),Expires.ToString(), IsUsed.ToString(), Duration.ToString(),Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourGuide = new User(Convert.ToInt32(values[1]));
            Tourist = new Tourist(Convert.ToInt32(values[2]));
            Enum.TryParse(values[3], out StatusType statusType);
            StatusType = statusType;
            Expires = DateTime.Parse(values[4]);
            IsUsed = bool.Parse(values[5]);
            Duration = Convert.ToInt32(values[6]);
            Enum.TryParse(values[7], out VoucherType voucherType);
            Type = voucherType;
        }
    }


}