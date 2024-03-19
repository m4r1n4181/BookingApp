using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationSearchParams
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AccommodationType? Type { get; set; }

        public int MaxGests {  get; set; }

        public int MinReservationDays {  get; set; }



    }
}
