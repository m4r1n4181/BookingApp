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
    public partial class ReservationRescheduleRequestsPage : Page, INotifyPropertyChanged
    {
        //public NavigationService navigationService;   
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
            DataContext = this;
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllForOwner(SignInForm.LoggedUser.Id));

        }

        private void ViewRequestButton_Click(object sender, RoutedEventArgs e)
        {
            
            RescheduleRequestsHandlingPage rescheduleRequestsHandlingPage = new RescheduleRequestsHandlingPage(SelectedReservationRescheduleRequest);
            this.NavigationService.Navigate(rescheduleRequestsHandlingPage);
        }
    }
}
