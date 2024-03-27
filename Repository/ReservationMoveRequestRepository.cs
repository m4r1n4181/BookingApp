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
    public class ReservationMoveRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationMoveRequests.csv";
        private readonly Serializer<ReservationMoveRequest> _serializer;
        public List<ReservationMoveRequest> _reservationMoveRequests;

        public ReservationMoveRequestRepository()
        {
            _serializer = new Serializer<ReservationMoveRequest>();
            _reservationMoveRequests = _serializer.FromCSV(FilePath);
        }
        public List<ReservationMoveRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

    }

}
