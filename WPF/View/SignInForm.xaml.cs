﻿using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.View.OwnerWindows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.View.ViewModel;
using BookingApp.WPF.Views.GuestWindows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;
        private NotificationController _notificationController;

        public static User LoggedUser { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _notificationController = new NotificationController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
               
                if (user.Password == txtPassword.Password)
                {
                    LoggedUser = user;
                    
                    if (user.Type == UserType.TourGuide)
                    {
                        //TourGuideHomePage tourGuideHomePage= new TourGuideHomePage();
                        //tourGuideHomePage.Show();
                        /* CreateTourForm createTourForm = new CreateTourForm();
                         createTourForm.Show();
                         LiveTourView liveTourView = new LiveTourView();
                         liveTourView.Show();*/

                    }
                    else if (user.Type == UserType.Owner)
                    {
                        OwnerMainWindow ownerMainWindow = new OwnerMainWindow();
                        ownerMainWindow.Show();
                         ReservationRescheduleRequestsWindow reservationRescheduleRequestsWindow = new ReservationRescheduleRequestsWindow();
                        reservationRescheduleRequestsWindow.Show();
                         //RegisterAccommodationForm registerAccommodationForm = new RegisterAccommodationForm();
                        //registerAccommodationForm.Show();
                       //AccommodationReservationToRateForm accommodationReservationToRateForm = new AccommodationReservationToRateForm(user);
                        //accommodationReservationToRateForm.Show();
                        // AccommodationsOverviewWindow accommodationOverviewWindow = new AccommodationsOverviewWindow();
                        //accommodationOverviewWindow.Show();
                      AccommodationOwnerReviewsWindow accommodationOwnerReviewsWindow = new AccommodationOwnerReviewsWindow();
                       accommodationOwnerReviewsWindow.Show();

                    }

                    else if (user.Type == UserType.Tourist)
                    {
                        TourOverviewForm tourOverviewForm = new TourOverviewForm(LoggedUser);
                        tourOverviewForm.Show();

                        List<Notification> notifications = _notificationController.GetByUserId(LoggedUser.Id);
                        foreach(Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Message, "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        _notificationController.ReadAllUserNotifications(LoggedUser.Id);
                    }
                    else if(user.Type == UserType.Guest)
                    {
                      //  AccommodationSearch accommodationSearch = new AccommodationSearch();
                       // accommodationSearch.Show();

                       // GuestWindows.AccommodationReservationToRateOwnerForm accommodationReservationToRateForm = new GuestWindows.AccommodationReservationToRateOwnerForm(user);
                        //accommodationReservationToRateForm.Show();
                        

                       // GuestWindows.AccommodationAllReservations accommodationAllReservations = new GuestWindows.AccommodationAllReservations(user);
                        //accommodationAllReservations.Show();

                        GuestHomePage guestHomePage = new GuestHomePage();
                        guestHomePage.Show();

                        List<Notification> notifications = _notificationController.GetByUserId(LoggedUser.Id);
                        foreach(Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        _notificationController.ReadAllUserNotifications(LoggedUser.Id);
                        Close();
                     

                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }
    }
}