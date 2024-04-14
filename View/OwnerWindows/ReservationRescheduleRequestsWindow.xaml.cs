using BookingApp.Controller;
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
    /// Interaction logic for ReservationRescheduleRequestsWindow.xaml
    /// </summary>
    public partial class ReservationRescheduleRequestsWindow : Window
    { 
        public ReservationRescheduleRequestsWindow()
        {
            InitializeComponent();
              this.DataContext = new ViewModels.OwnerViewModels.ReservationRescheduleRequestsViewModel();
            
        }
    }
}
