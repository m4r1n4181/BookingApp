using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.ViewModel.TouristViewModels;
using BookingApp.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View
{
    public partial class UsableVouchers : Window
    {
        public UsableVouchers()
        {
            InitializeComponent();
            this.DataContext = new UsableVouchersViewModel();
        }
   
    }
}
