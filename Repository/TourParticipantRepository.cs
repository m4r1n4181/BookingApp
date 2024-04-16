using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourParticipantRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-participants.csv";

        private readonly Serializer<TourParticipants> _serializer;

        public List<TourParticipants> _participants = new List<TourParticipants>();


        public TourParticipantRepository()
        {
            _serializer = new Serializer<TourParticipants>();
            _participants = _serializer.FromCSV(FilePath);
        }

        public TourParticipants GetById(int id)
        {
            _participants = _serializer.FromCSV(FilePath);
            return _participants.FirstOrDefault(participant => participant.Id == id);
        }

        public List<TourParticipants> FindParticipants(List<TourParticipants> tourParticipants)
        {
            List<TourParticipants> participants = new List<TourParticipants>();
            foreach(TourParticipants tourParticipant in tourParticipants)
            {
                participants.Add(GetById(tourParticipant.Id));
            }

            return participants;
        }

        public TourParticipants Save(TourParticipants participant)
        {
            participant.Id = NextId();
            _participants = _serializer.FromCSV(FilePath);
            _participants.Add(participant); // Dodajemo učesnika u listu
            _serializer.ToCSV(FilePath, _participants); // Čuvamo promjene u CSV datoteci
            return participant;
        }



        public int NextId()
        {
            _participants = _serializer.FromCSV(FilePath);
            if (_participants.Count < 1)
            {
                return 1;
            }
            return _participants.Max(c => c.Id) + 1;
        }

        public void Delete(TourParticipants participants)
        {
            _participants = _serializer.FromCSV(FilePath);
            TourParticipants founded = _participants.Find(t => t.Id == participants.Id);
            _participants.Remove(founded);
            _serializer.ToCSV(FilePath, _participants);
        }

        public TourParticipants Update(TourParticipants participants)
        {
            _participants = _serializer.FromCSV(FilePath);
            TourParticipants current = _participants.Find(t => t.Id == participants.Id);
            int index = _participants.IndexOf(current);
            _participants.Remove(current);
            _participants.Insert(index, participants);
            _serializer.ToCSV(FilePath, _participants);
            return participants;
        }

        /*public void AddParticipant(TourParticipants participant)
        {
            string[] csvData = participant.ToCSV();
            string csvString = string.Join(",", csvData);
        }*/



    }
}