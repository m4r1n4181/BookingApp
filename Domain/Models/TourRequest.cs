using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; } 
        public Location Location { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Tourist { get; set; } // proveri dal ovo ti odgovara jel nez dal treba particapanti mozda?
        public User TourGuide { get; set; }
        public RequestStatusType RequestStatus { get; set; }
        public DateTime? SelectedDate { get; set; }

        public TourRequest() { }

        public TourRequest(int id, Location location, string language, int maxTourists, string description, DateTime startDate, DateTime endDate, User tourist, User tourGuide, RequestStatusType requestStatus)
        {
            Id = id;
            Location = location;
            Language = language;
            MaxTourists = maxTourists;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Tourist = tourist;
            TourGuide = tourGuide;
            RequestStatus = requestStatus;
        }

        public string[] ToCSV()
        {
            string selectedDateStr = (SelectedDate == null) ? "null" : SelectedDate.ToString();
            string[] csvValues = { Id.ToString(), Location.Id.ToString(), Language.ToString(), MaxTourists.ToString(), Description, StartDate.ToString(), EndDate.ToString(), Tourist.Id.ToString(), TourGuide.Id.ToString(), RequestStatus.ToString(), selectedDateStr };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            Language = values[2];
            MaxTourists = Convert.ToInt32(values[3]);
            Description = values[4];
            StartDate = Convert.ToDateTime(values[5]);
            EndDate = Convert.ToDateTime(values[6]);
            Tourist = new User() { Id = Convert.ToInt32(values[7]) };
            TourGuide = new User() { Id = Convert.ToInt32(values[8]) };
            RequestStatus = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), values[9]);
            if (values.Length <= 10 || values[10] == "null")
            {
                SelectedDate = null;
            }
            else
            {
                SelectedDate = Convert.ToDateTime(values[10]);

            }

        }
      
    }
}
