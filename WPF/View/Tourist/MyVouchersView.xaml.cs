using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View;
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
using System.Windows.Shapes;
using BookingApp.WPF.ViewModels.Tourist;
using BookingApp.WPF.ViewModels.TouristViewModels;
using System.Windows.Navigation;

namespace BookingApp.WPF.View.Tourist
{
    public partial class MyVouchersView : Page
    {
        public static User User { get; set; }
        public MyVouchersView(NavigationService navigationService)
        {
            InitializeComponent();
            var viewModel = new MyVouchersView(navigationService);
            this.DataContext = viewModel;
        }
    }
}