﻿using BookingApp.Model.Enums;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Models
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public List<TourRequest> SimpleTourRequests { get; set; }
        public User Tourist { get; set; }
        public RequestStatusType Status { get; set; }
        public User? TourGuide { get; set; }
        public DateTime? SelectedDate { get; set; }

        public ComplexTourRequest()
        {
            SimpleTourRequests = new List<TourRequest>();
        }

        public ComplexTourRequest(int id, List<TourRequest> simpleTourRequests, User tourist, RequestStatusType status, User tourGuide)
        {
            Id = id;
            SimpleTourRequests = simpleTourRequests;
            Tourist = tourist;
            Status = status;
            TourGuide = tourGuide;
        }

        public string[] ToCSV()
        {
            string selectedDateStr = (SelectedDate == null) ? "null" : SelectedDate.Value.ToString("o"); // Koristite "o" format za standardni DateTime string

            string tourGuideId = (TourGuide == null) ? "null" : TourGuide.Id.ToString();
            StringBuilder requestList = new StringBuilder();
            foreach (TourRequest tourRequest in SimpleTourRequests)
            {
                requestList.Append(tourRequest.Id.ToString());
                requestList.Append(",");
            }
            if (requestList.Length > 0)
            {
                requestList.Remove(requestList.Length - 1, 1);
            }
            string[] csvValues =
            {
                Id.ToString(),
                Tourist.Id.ToString(),
                Status.ToString(),
                requestList.ToString(),
                tourGuideId,
                selectedDateStr
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tourist = new User() { Id = Convert.ToInt32(values[1]) };
            Status = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), values[2]);
            string[] tourRequestIds = values[3].Split(",");
            SimpleTourRequests = new List<TourRequest>();

            foreach (string tourRequestId in tourRequestIds)
            {
                int requestId = int.Parse(tourRequestId.Trim());
                TourRequest tourRequest = new TourRequest { Id = requestId };
                SimpleTourRequests.Add(tourRequest);
            }
            TourGuide = new User() { Id = Convert.ToInt32(values[4]) };

            // Provera da li je vrednost "null" pre konverzije
            if (values[5] == "null")
            {
                SelectedDate = null;
            }
            else
            {
                SelectedDate = Convert.ToDateTime(values[5]);
            }
        }
    }
}

