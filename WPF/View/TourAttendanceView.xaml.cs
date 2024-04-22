using BookingApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.ViewModel.TouristViewModels;
using BookingApp.View.ViewModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using BookingApp.ViewModel;
using BookingApp.View.ViewModel.TouristViewModels;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourAttendanceView.xaml
    /// </summary>
    public partial class TourAttendanceView : Window
    {
        public TourAttendanceView(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = new TourAttendanceViewModel(tourReservation);
        }
    }
}
