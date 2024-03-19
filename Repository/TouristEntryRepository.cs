using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristEntryRepository
    {
        private const string FilePath = "../../../Resources/Data/touristEntry.csv";

        private readonly Serializer<TouristEntry> _serializer;

        private List<TouristEntry> _touristEntry;

        public TouristEntryRepository()
        {
            _serializer = new Serializer<TouristEntry>();
            _touristEntry = _serializer.FromCSV(FilePath);
        }


        public void BindTours() //bindovala turu sa turistima koji su se pojavili na turi 
        {
           TourRepository tourRepository = new TourRepository();
            _touristEntry.ForEach(touristEntry => { touristEntry.Tour = tourRepository.GetById(touristEntry.Tour.Id); });

        }

        public List<TouristEntry> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TouristEntry> GetAllWithTours()
        {
            _touristEntry = _serializer.FromCSV(FilePath);
            BindTours();
            return _touristEntry;
        }


        public TouristEntry Save(TouristEntry touristEntry)
        {
            touristEntry.Id = NextId();
            _touristEntry = _serializer.FromCSV(FilePath);
            _touristEntry.Add(touristEntry);
            _serializer.ToCSV(FilePath, _touristEntry);
            return touristEntry;
        }

        public int NextId()
        {
            _touristEntry = _serializer.FromCSV(FilePath);
            if (_touristEntry.Count < 1)
            {
                return 1;
            }
            return _touristEntry.Max(touristEntry => touristEntry.Id) + 1;
        }

        public void Delete(TourGuide tourGuide)
        {
            _touristEntry = _serializer.FromCSV(FilePath);
            TouristEntry founded = _touristEntry.Find(touristEntry => touristEntry.Id == touristEntry.Id);
            _touristEntry.Remove(founded);
            _serializer.ToCSV(FilePath, _touristEntry);
        }

        public TouristEntry Update(TouristEntry touristEntry)
        {
            _touristEntry = _serializer.FromCSV(FilePath);
            TouristEntry current = _touristEntry.Find(touristEntry => touristEntry.Id == touristEntry.Id);
            int index = _touristEntry.IndexOf(current);
            _touristEntry.Remove(current);
            _touristEntry.Insert(index, touristEntry);
            _serializer.ToCSV(FilePath, _touristEntry);
            return touristEntry;
        }

       

    }
}
