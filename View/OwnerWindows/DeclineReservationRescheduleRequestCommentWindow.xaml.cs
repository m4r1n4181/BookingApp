using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
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
    /// Interaction logic for DeclineReservationRescheduleRequestCommentWindow.xaml
    /// </summary>
    public partial class DeclineReservationRescheduleRequestCommentWindow : Window
    {
        public DeclineReservationRescheduleRequestCommentWindow(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.DeclineReservationRescheduleRequestCommentViewModel(reservationRescheduleRequest);

        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
