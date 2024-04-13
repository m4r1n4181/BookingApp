using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;

namespace BookingApp.Domain.Models
{
    public class ReservationRescheduleRequest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public User Guest { get; set; }
        public DateTime NewStart { get; set; }
        public DateTime NewEnd { get; set; }
        public string Comment { get; set; }
        public RequestStatusType Status { get; set; }

        public ReservationRescheduleRequest()
        {

        }

        public ReservationRescheduleRequest(int id, AccommodationReservation reservation, User guest, DateTime newStart, DateTime newEnd, string comment, RequestStatusType status)
        {
            Id = id;
            Reservation = reservation;
            Guest = guest;
            NewStart = newStart;
            NewEnd = newEnd;
            Comment = comment;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                Guest.Id.ToString(),
                NewStart.ToString(),
                NewEnd.ToString(),
                Comment,
                Status.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Guest = new User(Id = Convert.ToInt32(values[2]));
            NewStart = Convert.ToDateTime(values[3]);
            NewEnd = Convert.ToDateTime(values[4]);
            Comment = values[5];           
            Status = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), values[6]);
        }
    }
}

