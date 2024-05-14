using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View;
using BookingApp.ViewModels;
using BookingApp.WPF.View.OwnerPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class ReservationRescheduleRequestPageViewModel : ViewModelBase
    {
        public ReservationRescheduleRequest SelectedReservationRescheduleRequest { get; set; }
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
        public NavigationService NavigationService { get; set; }
        public RelayCommand ViewRequestCommand { get; set; }
        public ReservationRescheduleRequestPageViewModel(NavigationService service)

        {
            this.NavigationService = service;
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id));

            ViewRequestCommand = new RelayCommand(Execute_RequestHandlingCommand, CanHandle);
        }

        public void Refresh()
        {
            ReservationRescheduleRequests.Clear();
            foreach (ReservationRescheduleRequest request in _reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id))
            {
                ReservationRescheduleRequests.Add(request);
            }
        }
        public bool CanHandle(object param)
        {
            return SelectedReservationRescheduleRequest != null;
        }
        private void Execute_RequestHandlingCommand(object sender)
        {
            RescheduleRequestsHandlingPage reservationRescheduleRequestHandling = new RescheduleRequestsHandlingPage(SelectedReservationRescheduleRequest);
            NavigationService.Navigate(reservationRescheduleRequestHandling);
            Refresh();
        }



    }
}
