using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.GuestViewModels
{
    public class SuperGuestViewModel
    {
        public int SuperGuestPoints { get; set; }
        public int SuperGuestReservations { get; set; }
        public Visibility SuperGuestVisibility { get; set; }
        public SuperGuest SuperGuest { get; set; }  
        public User User { get; set; }

        private SuperGuestController _superGuestController;
        private AccommodationReservationController _accommodationReservationController;
        private AccommodationReservationService _accommodationReservationService; 
        public SuperGuestViewModel() 
        {
            User = SignInForm.LoggedUser;
            _superGuestController = new SuperGuestController();
            _accommodationReservationController = new AccommodationReservationController();
            _accommodationReservationService = new AccommodationReservationService();

            SuperGuest  = _superGuestController.GetByUserId(User.Id);
            if(SuperGuest == null)
            {
                SuperGuestVisibility = Visibility.Hidden;
            }
            else
            {
               
                SuperGuestPoints = SuperGuest.Points;
                SuperGuestReservations = _accommodationReservationService.CountReservationsLastYear(User);
                SuperGuestVisibility = Visibility.Visible;

            }
        }
    }
}
