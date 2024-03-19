using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace BookingApp.Model
{
    public class Guest : User, BookingApp.Serializer.ISerializable
    {

        public Guest() { }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Guest(string name, string surname, string username, string password) : base(username, password, UserType.Guest)
        {
            Name = name;
            Surname = surname;
        }

        // Implementacija metoda iz interfejsa ISerializable
        public string[] ToCSV()
        {
            // Pretvaranje svojstava objekta u niz stringova
            string[] csvValues = { Name, Surname, Username, Password };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            // Postavljanje svojstava objekta na vrednosti iz niza stringova
           
                Name = values[0];
                Surname = values[1];
                Username = values[2];
                Password = values[3];
         
        }

        public Guest(int id)
        {
            this.Id = id;
        }
    }
}