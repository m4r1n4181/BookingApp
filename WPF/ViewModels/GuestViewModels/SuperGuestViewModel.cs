using BookingApp.Controller;
using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
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
            _accommodationReservationService = new AccommodationReservationService(
                Injector.CreateInstance<IAccommodationReservationRepository>(),
                Injector.CreateInstance<IAccommodationRepository>(),
                Injector.CreateInstance<INotificationRepository>(),
                Injector.CreateInstance<IOwnerReviewRepository>(),
                Injector.CreateInstance<ISuperGuestRepository>());

            SuperGuest  = _superGuestController.GetByUserId(User.Id);
            if(SuperGuest == null)
            {
                SuperGuestVisibility = Visibility.Hidden;
                SuperGuestReservations = _accommodationReservationService.CountReservationsLastYear(User);
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
