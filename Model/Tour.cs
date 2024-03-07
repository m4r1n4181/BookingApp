using System;
using System.Collections.Generic;


namespace BookingApp.Model
{
    public class Tour : Location
    {

        //treba mi klasa loction i klasa keypoint kao parametri u ovoj klasi 
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public int AvaibleSeats { get; set; }  
        public List<DateTime> StartDates { get; set; }
        public int Duration { get; set; }
        public List<string> Images { get; set; }

        public bool IsStarted { get; set; } // da li je tura započela
       // public List<TouristEntry> TouristEntries { get; set; } // koji od prijavljenih turista su došli na turu

        public Tour(
            int id,
            string name,
            string description,
            string language,
            int maxTourists,
            List<DateTime> startDates,
            int duration,
            List<string> images,
            bool isStarted)
           // List<TouristEntry> touristEntries )
        {
            Id = id;
            Name = name;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            StartDates = startDates;
            Duration = duration;
            Images = images;
            IsStarted = isStarted;
            //TouristEntries = touristEntries;
        }

        public Tour()
        { 
            StartDates = new List<DateTime>();
            Images = new List<string>();
            IsStarted = false;
            // TouristEntries = new List<TouristEntry>();
        }
    }
 
}
