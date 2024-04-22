using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuestWindows
{
    /// <summary>
    /// Interaction logic for GuestRequests.xaml
    /// </summary>
    /// 
   
    public partial class GuestRequests : Window
    {

        private ReservationRescheduleRequestController _reservationRescheduleRequestController;

        public ObservableCollection<ReservationRescheduleRequest> Requests { get; set; }
        public GuestRequests()
        {
            InitializeComponent();
            this.DataContext = this;
            int loggedId = SignInForm.LoggedUser.Id;
            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();
            Requests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestController.GetAllForGuest(loggedId));
        }

    }
}
