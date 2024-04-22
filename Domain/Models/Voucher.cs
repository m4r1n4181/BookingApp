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
        public Tourist Tourist { get; set; }
        public StatusType StatusType { get; set; }   //status active used expired
        public bool IsUsed { get; set; }
        public int Duration { get; set; }

        public DateTime Expires { get; set; }

        public VoucherType Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Voucher() { }

        public Voucher(int id, Tourist tourist, StatusType statusType, DateTime expires, bool isUsed, int duration, VoucherType type)
        {
            Id = id;
            Tourist = tourist;
            StatusType = statusType;
            Expires = expires;
            IsUsed = isUsed;
            Duration = duration;
            Type = type;


        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Tourist.Id.ToString(), StatusType.ToString(), Expires.ToString(), IsUsed.ToString(), Duration.ToString(), Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Tourist = new Tourist(Convert.ToInt32(values[1]));
            Enum.TryParse(values[2], out StatusType statusType);
            StatusType = statusType;
            Expires = DateTime.Parse(values[3]);
            IsUsed = bool.Parse(values[4]);
            Duration = Convert.ToInt32(values[5]);
            Enum.TryParse(values[6], out VoucherType voucherType);
            Type = voucherType;
        }
    }


}