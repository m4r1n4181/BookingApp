using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View;
using BookingApp.View.OwnerWindows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestsViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationRescheduleRequest> reservationRescheduleRequests;
        public ObservableCollection<ReservationRescheduleRequest> ReservationRescheduleRequests
        {
            get { return reservationRescheduleRequests; }
            set
            {
                reservationRescheduleRequests = value;
                OnPropertyChanged();
            }
        }
        public ReservationRescheduleRequestController _reservationRescheduleRequestsController;
       // public ICommand RescheduleHandleCommand { get; private set; }
        public ReservationRescheduleRequest SelectedReservationRescheduleRequest { get; set; }


        public ReservationRescheduleRequestsViewModel()
        {
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id));
           
        }

     
        public void RescheduleHandleButton_Click(object sender, RoutedEventArgs e)
        {
            RescheduleRequestsHandling rescheduleRequestsWindow = new RescheduleRequestsHandling(SelectedReservationRescheduleRequest);
            rescheduleRequestsWindow.Show();
        }

    }
}
