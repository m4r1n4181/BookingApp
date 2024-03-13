using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Guest : User
    {
        public Guest() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }




        public Guest(string name, string surname, string username, string password) : base(username, password)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public Guest(int id)
        {
            this.Id = id;
        }
    }
}