using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class Owner : User
    {
        private int OwnerId { get; set; }

        public Owner() { }

        public Owner(int id)
        {
            this.OwnerId = id;
        }
     }

}