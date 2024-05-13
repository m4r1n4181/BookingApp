﻿using BookingApp.Model.Enums;
using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserType Type { get; set; }

        public User() { }
        public User(int id) {
            Id = id;

        }

        public User( string username, string password, UserType type)
        {
            Username = username;
            Password = password;
            Type = type;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Enum.TryParse(values[3], out UserType userType);
            Type = userType;
        }
    }
}
