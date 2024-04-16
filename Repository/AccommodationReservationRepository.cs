using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace BookingApp.Repository
{
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation-reservation.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        public List<AccommodationReservation> AccommodationReservations;
        public List<User> _guests;
        private static AccommodationReservationRepository instance = null;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            AccommodationReservations = _serializer.FromCSV(FilePath);
        }
     
        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public List<AccommodationReservation> GetAllWithAccommodations()
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            BindAccommodations();
            return AccommodationReservations;
        }
        public void BindAccommodations() // sa rezervacijama
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
           /* foreach(var accommodationReservation in AccommodationReservations)
            {
                accommodationReservation.Accommodation = accommodationRepository.GetById(accommodationReservation.Accommodation.Id);
            }*/
            AccommodationReservations.ForEach(accR => { accR.Accommodation = accommodationRepository.GetById(accR.Accommodation.Id); });
        }
        public void BindGuests()
        {
            UserRepository userRepository = new UserRepository();
           /* foreach(var accommodationReservation in AccommodationReservations){
                accommodationReservation.Guest = userRepository.GetById(accommodationReservation.Guest.Id);
            }*/
            AccommodationReservations.ForEach(accR => { accR.Guest = userRepository.Get(accR.Guest.Id); });
        }

        public static AccommodationReservationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new AccommodationReservationRepository();
            }
            return instance;
        }
        public AccommodationReservation Get(int id)
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);   
            BindAccommodations();
            BindGuests();
            return AccommodationReservations.Find(ar => ar.Id == id);
        }

       
  
        public List<AccommodationReservation> GetAllWithGuests()
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            BindGuests();
            return AccommodationReservations;
        }


        public List<AccommodationReservation> GetAllWithGuestsAndAccommodations()
        {
            AccommodationReservations = GetAllWithGuests();
            BindAccommodations();
            return AccommodationReservations;
        }
           public AccommodationReservation GetById(int id)
        {
            AccommodationReservations = GetAllWithGuestsAndAccommodations();
            return AccommodationReservations.FirstOrDefault(t => t.Id == id);
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            AccommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, AccommodationReservations);
            return accommodationReservation;
        }

        public int NextId()
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            if (AccommodationReservations.Count < 1)
            {
                return 1;
            }
            return AccommodationReservations.Max(a => a.Id) + 1;
        }
        public void Delete(AccommodationReservation accommodationreservation)
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = AccommodationReservations.Find(c => c.Id == accommodationreservation.Id);
            AccommodationReservations.Remove(founded);
            _serializer.ToCSV(FilePath, AccommodationReservations);
        }
        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = AccommodationReservations.Find(a => a.Id == accommodationReservation.Id);
            int index = AccommodationReservations.IndexOf(current);
            AccommodationReservations.Remove(current);
            AccommodationReservations.Insert(index, accommodationReservation);
            _serializer.ToCSV(FilePath, AccommodationReservations);
            return accommodationReservation;
        }



        public List<AccommodationReservation> GetByAccommodation(int accommodationId)
        {
            AccommodationReservations = _serializer.FromCSV(FilePath);
            return AccommodationReservations.FindAll(c => c.Accommodation.Id == accommodationId);
        }
        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            List<AccommodationReservation> reservations = GetAllWithAccommodations();
            return reservations.Where(reservation => reservation.Accommodation.Id == id).ToList();
        }

        public List<AccommodationReservation> GetByOwnerId(int id)
        {
            List<AccommodationReservation> reservations = GetAllWithAccommodations();
            return reservations.Where(reservation => reservation.Accommodation.Owner.Id == id).ToList();
        }
    }
}
