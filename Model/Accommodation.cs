using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Model
{
   
    public class Accommodation : ISerializable
    {
        public int Id {  get; set; }
        public Owner Owner { get; set; }
        public string Name { get; set; }
        public AccommodationType Type { get; set; }
        public Location Location { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationDays { get; set; }
        public List<string> Pictures { get; set; }



        public Accommodation() 
        {
        }
        public Accommodation(string name, AccommodationType type, int maxGuests, int minReservationDays, int cancellationDays)
        {
            Name = name;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationDays = cancellationDays;
        }

        public Accommodation( string name, AccommodationType type, Location location, int maxGuests, int minReservationDays, int cancellationDays, List<string> pictures)
        {
           // Owner = new Owner(user);
            Name = name;
            Type = type;
            Location = location;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationDays = cancellationDays;
            Pictures = pictures;
        }
        public Accommodation(int id, Owner owner, string name, AccommodationType type, Location location, int maxGuests, int minReservationDays, int cancellationDays, List<string> pictures)
        {
            Id = id;
            Owner = owner;
            Name = name;
            Type = type;
            Location = location;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationDays = cancellationDays;
            Pictures = pictures;
        }
        public string[] ToCSV()
        {
            string picturesString = string.Join(",", Pictures);
            string[] csvValues = { Id.ToString(), Owner.Id.ToString(), Name, Type.ToString(), Location.Id.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancellationDays.ToString(), picturesString };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Owner = new Owner(Convert.ToInt32(values[1]));
            Name = values[2];
            Enum.TryParse(values[3], out AccommodationType loadedType);
            Type = loadedType;
            Location = new Location() { Id = Convert.ToInt32(values[4])};
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancellationDays = Convert.ToInt32(values[7]);
            Pictures = values[8].Split(",").ToList();
            
        }

    }



}
