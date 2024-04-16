using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourSearchParams
    {
        public string City { get; set; }
        public string Country { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public int AvailableSeats { get; set; }



    }
}