using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class SuperGuestRepository : ISuperGuestRepository
    {

        private const string FilePath = "../../../Resources/Data/superGuests.csv";
        private readonly Serializer<SuperGuest> _serializer;
        public List<SuperGuest> SuperGuests;

        public SuperGuestRepository()
        {
            _serializer = new Serializer<SuperGuest>();
            SuperGuests = _serializer.FromCSV(FilePath);
        }

        public SuperGuest GetById(int id)
        {
            SuperGuests = _serializer.FromCSV(FilePath);
            return SuperGuests.FirstOrDefault(sg => sg.Guest.Id == id,null);
        }

        public List<SuperGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public SuperGuest Save(SuperGuest superGuest)
        {
            superGuest.Id = NextId();
            var superGuests = _serializer.FromCSV(FilePath);
            superGuests.Add(superGuest);
            _serializer.ToCSV(FilePath, superGuests);
            return superGuest;
        }

        private int NextId()
        {
            var superGuests = _serializer.FromCSV(FilePath);
            if (superGuests.Count < 1)
            {
                return 1;
            }
            return superGuests.Max(sg => sg.Id) + 1;
        }

        public SuperGuest GetByUserId(int userId)
        {
            SuperGuests = _serializer.FromCSV(FilePath);
            return SuperGuests.FirstOrDefault(sg => sg.Guest.Id == userId);
        }

        public void Delete(SuperGuest superGuest)
        {
            List<SuperGuest> superGuests = _serializer.FromCSV(FilePath);
            SuperGuest foundSuperGuest = superGuests.FirstOrDefault(sg => sg.Id == superGuest.Id);
            if (foundSuperGuest != null)
            {
                superGuests.Remove(foundSuperGuest);
                _serializer.ToCSV(FilePath, superGuests);
            }
        }

        public SuperGuest Update(SuperGuest superGuest)
        {

            SuperGuests = _serializer.FromCSV(FilePath);

            SuperGuest trenutni = SuperGuests.Find(sg => sg.Id == superGuest.Id);

            if (trenutni != null)
            {
                trenutni.Guest = superGuest.Guest;
                trenutni.Start = superGuest.Start;
                trenutni.End = superGuest.End;
                trenutni.Points = superGuest.Points;
                _serializer.ToCSV(FilePath, SuperGuests);
            }

            return superGuest;
        }

    }
}
