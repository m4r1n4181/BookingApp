using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class MostRequestedForTour
    {
        public User TourGuide { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MostRequestedLanguage { get; set; }
        public string MostRequestedLocation { get; set; }
        public int MaxTourists { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public List<string> Pictures { get; set; }

        public MostRequestedForTour(User tourGuide, string name, string description, string mostRequestedLanguage, string mostRequestedLocation, int maxTourists, DateTime startDate, int duration, List<string> pictures)
        {
            TourGuide = tourGuide;
            Name = name;
            Description = description;
            MostRequestedLanguage = mostRequestedLanguage;
            MostRequestedLocation = mostRequestedLocation;
            MaxTourists = maxTourists;
            StartDate = startDate;
            Duration = duration;
            Pictures = pictures;
        }
    }
}
