using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestsPage.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestsPage : Page, INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReservationRescheduleRequestsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id));

        }

        private void ViewRequestButton_Click(object sender, RoutedEventArgs e)
        {

            RescheduleRequestsHandlingPage rescheduleRequestsHandlingPage = new RescheduleRequestsHandlingPage(SelectedReservationRescheduleRequest);
            this.NavigationService.Navigate(rescheduleRequestsHandlingPage);
            //pitati kako view model da Navigate drugi page 
        }
    }
}
