using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View;
using BookingApp.View.OwnerWindows;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class ReservationRescheduleRequestsViewModel : ViewModelBase
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
        public ICommand RescheduleHandleCommand { get; private set; }

        public ReservationRescheduleRequestsViewModel()
        {
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id));

            RescheduleHandleCommand = new RelayCommand(ExecuteRescheduleHandleCommand, CanExecuteRescheduleHandleCommand);
        }

        private void ExecuteRescheduleHandleCommand()
        {
            if (SelectedReservationRescheduleRequest != null)
            {
                RescheduleRequestsHandling rescheduleRequestsWindow = new RescheduleRequestsHandling(SelectedReservationRescheduleRequest);
                rescheduleRequestsWindow.Show();
            }
        }

        private bool CanExecuteRescheduleHandleCommand()
        {
            // Add any conditions for when the command can execute, if needed
            return true;
        }
    }
}
