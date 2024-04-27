using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourRequestSearch
    {
        public string City { get; set; }
        public string Country { get; set; }
        public RequestStatusType? Status { get; set; }
        public int MaxTourists { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
