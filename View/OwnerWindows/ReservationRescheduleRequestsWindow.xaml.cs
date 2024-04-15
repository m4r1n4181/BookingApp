using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.ViewModels.OwnerViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.View.OwnerWindows
{
    public partial class ReservationRescheduleRequestsWindow : Window
    {
        public ReservationRescheduleRequestsWindow()
        {
            InitializeComponent();
            DataContext = new ReservationRescheduleRequestsViewModel();
        }
    }
}
