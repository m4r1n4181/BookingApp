using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Serializer;


namespace BookingApp.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "..\\..\\..\\Resources\\Data\\accommodation.csv";
        private readonly Serializer<Accommodation> _serializer;
        public List<Accommodation> Accommodations; //{ get; set; } = new List<Accommodation>();


        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            Accommodations = _serializer.FromCSV(FilePath);
        }
        public void RegisterAccommodation(Accommodation accommodation)
        {

        }

        public Accommodation GetById(int id)
        {
            Accommodations = _serializer.FromCSV(FilePath);
            return Accommodations.FirstOrDefault(acc => acc.Id == id);
        }

        public void BindLocations()
        {
            LocationRepository locationRepository = new LocationRepository();
            Accommodations.ForEach(locR => { locR.Location = locationRepository.GetById(locR.Location.Id); });
        }

     
        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            Accommodations = _serializer.FromCSV(FilePath);
            Accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, Accommodations);
            return accommodation;
        }

        public int NextId()
        {
            Accommodations = _serializer.FromCSV(FilePath);
            if (Accommodations.Count < 1)
            {
                return 1;
            }
            return Accommodations.Max(a => a.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            Accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = Accommodations.Find(c => c.Id == accommodation.Id);
            Accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, Accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            Accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = Accommodations.Find(a => a.Id == accommodation.Id);
            int index = Accommodations.IndexOf(current);
            Accommodations.Remove(current);
            Accommodations.Insert(index, accommodation);        
            _serializer.ToCSV(FilePath, Accommodations);
            return accommodation;
        }

        public List<Accommodation> GetByOwner(Owner owner)
        {
            Accommodations = _serializer.FromCSV(FilePath);
            return Accommodations.FindAll(a => a.Owner.Id == owner.Id);
        }

        public List<Accommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<Accommodation> GetAllWithLocations()
        {
            Accommodations = _serializer.FromCSV(FilePath);
            BindLocations();
            return Accommodations;
        }


        public List<Accommodation> SearchAccommodation(AccommodationSearchParams searchParams) 
        {
            Accommodations = _serializer.FromCSV(FilePath);
            BindLocations();
            if (searchParams.Name != null)
            {
                Accommodations = Accommodations.FindAll(a => a.Name.Contains(searchParams.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.City != null)
            {
                Accommodations = Accommodations.FindAll(a => a.Location.City.Contains(searchParams.City, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.Country != null)
            {
                Accommodations = Accommodations.FindAll(a => a.Location.Country.Contains(searchParams.Country, StringComparison.OrdinalIgnoreCase));
            }
            if(searchParams.Type != null)
            {
                Accommodations = Accommodations.FindAll(a => a.Type == searchParams.Type);
            }
            if(searchParams.MaxGests != 0)
            {
                Accommodations = Accommodations.FindAll(a => a.MaxGuests >= searchParams.MaxGests);
            }
            if(searchParams.MinReservationDays != 0)
            {
                Accommodations = Accommodations.FindAll(a => a.MinReservationDays <= searchParams.MinReservationDays);
            }


            return Accommodations;
        }

   


    }


}
