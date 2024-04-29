using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View;
using BookingApp.View.OwnerWindows;
using BookingApp.ViewModels.OwnerViewModels;
using BookingApp.WPF.ViewModels.OwnerPageViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestsPage.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestsPage : Page
    {
        public NavigationService navigationService;   
        public ReservationRescheduleRequestsPage()
        {
            InitializeComponent();
            ReservationRescheduleRequestPageViewModel reservationRescheduleRequestPageViewModel = new ReservationRescheduleRequestPageViewModel(navigationService);  
            DataContext = reservationRescheduleRequestPageViewModel;
        }

    }
}
