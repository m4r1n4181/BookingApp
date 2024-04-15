using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for RescheduleRequestsHandling.xaml
    /// </summary>
    public partial class RescheduleRequestsHandling : Window
    {
        public RescheduleRequestsHandling(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.ReservationRescheduleRequestHandlingViewModel(reservationRescheduleRequest);

        }

        private void AcceptRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeclineRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
