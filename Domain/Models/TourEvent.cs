﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourEvent : BookingApp.Serializer.ISerializable
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartTime { get; set; }

        public TourEvent() { }

        public TourEvent(int id, Tour tour, DateTime startTime)
        {
            Id = id;
            Tour = tour;
            StartTime = startTime;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Tour.Id.ToString(), StartTime.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Tour = new Tour() { Id = Convert.ToInt32(values[1]) };
            StartTime = DateTime.Parse(values[2]);
        }
    }
}
