using BookingApp.Model;
using BookingApp.WPF.ViewModels.TouristViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.View;
using GalaSoft.MvvmLight.Views;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourAttendanceView.xaml
    /// </summary>
    public partial class TourAttendanceView : Page
    {
        public TourAttendanceView(TourReservation tourReservation, NavigationService navigationService)
        {
            InitializeComponent();
            var viewModel = new TourAttendanceViewModel(tourReservation,navigationService);
            this.DataContext = viewModel;
        }
    }
}
