using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourParticipantRepository
    {
        TourParticipants GetById(int id);
        List<TourParticipants> FindParticipants(List<TourParticipants> tourParticipants);
        TourParticipants Save(TourParticipants participant);
        int NextId();
        void Delete(TourParticipants participants);
        TourParticipants Update(TourParticipants participants);
    }
}
