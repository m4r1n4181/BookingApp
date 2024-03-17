using System.Collections.Generic;
using System.Runtime.Serialization;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Model.Enums;


namespace BookingApp.Repository
{
    public class GuestRepository
    { 

        private readonly Serializer<Guest> _serializer;

        public List<Guest> _guests = new List<Guest>();



    }
}


