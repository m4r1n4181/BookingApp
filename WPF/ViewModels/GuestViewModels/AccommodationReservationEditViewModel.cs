﻿using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.GuestViewModels
{
    public class AccommodationReservationEditViewModel
    {

        public RelayCommand ConfirmCommand { get; set; }
        
        private DateTime _newArrival;
        public DateTime NewArrival
        {
            get => _newArrival;
            set
            {
                if (value != _newArrival)
                {
                    _newArrival = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _newDeparture;
        public DateTime NewDeparture
        {
            get => _newDeparture;
            set
            {
                if (value != _newDeparture)
                {
                    _newDeparture = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationReservation SelectedAccommodationReservation;
        private ReservationRescheduleRequestController _reservationRescheduleRequestController;
        public AccommodationReservationEditViewModel(AccommodationReservation accommodationReservation) 
        {
            SelectedAccommodationReservation = accommodationReservation;

            NewArrival = DateTime.Now;
            NewDeparture = DateTime.Now;

            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            ConfirmCommand = new RelayCommand(Button_Confirm_Click);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Confirm_Click(object sender)
        {
            ReservationRescheduleRequest request = new ReservationRescheduleRequest()
            {
                Reservation = SelectedAccommodationReservation,
                Guest = SignInForm.LoggedUser,
                NewStart = NewArrival,
                NewEnd = NewDeparture,
                Status = Model.Enums.RequestStatusType.Standby
            };
            _reservationRescheduleRequestController.Save(request);
            //Close();
        }
    }
}

