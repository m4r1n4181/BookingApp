using BookingApp.Controller;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for ReservationRescheduleRequestsWindow.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestsWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<ReservationRescheduleRequest> ReservationRescheduleRequests { get; set; }
        public ReservationRescheduleRequestController _reservationRescheduleRequestsController;

        public ReservationRescheduleRequest SelectedReservationRescheduleRequest { get; set; }
        public ReservationRescheduleRequestsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _reservationRescheduleRequestsController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequests = new ObservableCollection<ReservationRescheduleRequest>(_reservationRescheduleRequestsController.GetAllRequestsForHandling());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RequestHandlingButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservationRescheduleRequest == null)
            {
                return;
            }
           // ReservationRescheduleRequestHandlingWindow ReservationRescheduleRequestHandling = new ReservationRescheduleRequestHandlingWindow(SelectedReservationRescheduleRequest);
            //ReservationRescheduleRequestHandling.Show();
            Close();
        }
    }
}
