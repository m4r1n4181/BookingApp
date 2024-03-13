using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


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

        public Location Location { get; set; }

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
            Location location,
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
            Location = location;
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

        public string[] GetToCSV()
        {
            string[] csvValues = new string[]
            {
                Id.ToString(),
                Name,
                Description,
                Language,
                MaxTourists.ToString(),
                string.Join(";", StartDates), // Pretvoriti listu datuma u string, odvojene tačka-zarezom
                Duration.ToString(),
                string.Join(";", Images), // Pretvoriti listu slika u string, odvojene tačka-zarezom
                IsStarted.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            Language = values[3];
            MaxTourists = int.Parse(values[4]);
            StartDates = new List<DateTime>();
            foreach (var dateStr in values[5].Split(';'))
            {
                StartDates.Add(DateTime.Parse(dateStr)); // Pretpostavka: Datum je u standardnom formatu
            }
            Duration = int.Parse(values[6]);
            Images = new List<string>(values[7].Split(';'));
            Location = new Location(values[8], values[9]);
            IsStarted = bool.Parse(values[8]);
        }


        public static List<Tour> LoadToursFromCsv(string filePath)
        {
            List<Tour> tours = new List<Tour>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(','); // Pretpostavljamo da su podaci u CSV fajlu odvojeni zarezom
                        Tour tour = new Tour();
                        tour.FromCSV(values);
                        tours.Add(tour);
                    }
                }
            }
            catch (IOException e)
            {
                // Handle exception if file reading fails
            }

            return tours;
        }

        // Metoda za prikaz svih tura
        public static List<Tour> GetAllTours(string filePath)
        {
            // Učitaj sve ture iz CSV fajla
            List<Tour> allTours = LoadToursFromCsv(filePath);

            // Vrati listu svih tura
            return allTours;
        }

    }

}
