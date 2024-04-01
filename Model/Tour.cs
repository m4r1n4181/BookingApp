using BookingApp.Model.Enums;
using BookingApp.Serializer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;


using BookingApp.Model.Enums;
using BookingApp.Serializer;
using BookingApp.Service;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;


namespace BookingApp.Model
{
    public class Tour : ISerializable
    {

        //treba mi klasa loction i klasa keypoint kao parametri u ovoj klasi 
        public int Id { get; set; }
        public User TourGuide { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public Location Location { get; set; }
        public int MaxTourists { get; set; }
        public int AvailableSeats { get; set; }
        public List<DateTime> StartDates { get; set; }
        public int Duration { get; set; }
        public List<string> Pictures { get; set; }

        public bool IsStarted { get; set; } // da li je tura započela
                                            // public List<TouristEntry> TouristEntries { get; set; } // koji od prijavljenih turista su došli na turu



        public Tour()
        {

        }

        public Tour(int id)
        {
            this.Id = id;
        }

        public Tour(string language, Location location, int maxTourists, int duration)
        {
            Language = language;
            Location = location;
            MaxTourists = maxTourists;
            Duration = duration;
        }

        public Tour(int id, TourGuide tourGuide, string name, string description, string language, Location location, int maxTourists, int availableSeats, List<DateTime> startDates, int duration, List<string> pictures, bool isStarted)
        {
            Id = id;
            TourGuide = tourGuide;
            Name = name;
            Description = description;
            Language = language;
            Location = location;
            MaxTourists = maxTourists;
            AvailableSeats = availableSeats;
            StartDates = startDates;
            Duration = duration;
            Pictures = pictures;
            IsStarted = isStarted;
        }





        public string[] ToCSV()
        {

            string startDatesString = string.Join(";", StartDates);
            // string? picturesString = Pictures != null ? string.Join(",", Pictures) : null;
            //takodje nista se ne upisuje u tour.csv i proeriti saveAll keypoints
            string picturesString = string.Join(",", Pictures);
            string[] csvValues = { Id.ToString(), TourGuide.Id.ToString(), Name, Description, Language, Location.Id.ToString(), MaxTourists.ToString(), AvailableSeats.ToString(), startDatesString, Duration.ToString(), picturesString, IsStarted.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourGuide = new User(Convert.ToInt32(values[1]));
            Name = values[2];
            Description = values[3];
            Language = values[4];
            Location = new Location() { Id = Convert.ToInt32(values[5]) };
            MaxTourists = Convert.ToInt32(values[6]);
            AvailableSeats = Convert.ToInt32(values[7]);

            List<string> datesString = values[8].Split(";").ToList();
            StartDates = datesString.Select(s => Convert.ToDateTime(s)).ToList();

            Duration = Convert.ToInt32(values[9]);
            Pictures = values[10].Split(",").ToList();
            IsStarted = Convert.ToBoolean(values[11]);

        }


    }

}
