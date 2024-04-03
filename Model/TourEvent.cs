﻿using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.Model

{
    public class TourEvent : ISerializable
    {

        public int Id { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartTime { get; set; }

        public bool Done { get; set; }

        public TourEvent() { }



        public TourEvent(int id, Tour tour, DateTime startTime, bool done)
        {
            Id = id;
            Tour = tour;
            StartTime = startTime;
            Done = done;
        }

        public TourEvent(int id)
        {
            Id = id;
        }

        public string[] ToCSV()
        {


            string[] csvValues ={ Id.ToString(),Tour.Id.ToString(),StartTime.ToString(),Done.ToString()};
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Tour = new Tour() { Id = Convert.ToInt32(values[1]) };
            StartTime = DateTime.Parse(values[2]);
            Done = bool.Parse(values[3]);

        }

    }
}


