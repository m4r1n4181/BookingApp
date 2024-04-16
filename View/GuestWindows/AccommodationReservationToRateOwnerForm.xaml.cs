using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.ViewModels.GuestViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View.GuestWindows
{
    public partial class AccommodationReservationToRateOwnerForm : Window//, INotifyPropertyChanged
    {
        
        public AccommodationReservationToRateOwnerForm()
        {
            InitializeComponent();
            DataContext = new AccommodationReservationToRateOwnerFormViewModel();


        }


    }
}
