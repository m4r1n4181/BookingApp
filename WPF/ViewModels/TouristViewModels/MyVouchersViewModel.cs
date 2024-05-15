using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using BookingApp.View;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class MyVouchersViewModel
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public VoucherController _voucherController;
        public Voucher _voucher;
        public NavigationService Navigation {  get; set; }
        // public User User { get; set; }

        public MyVouchersViewModel(NavigationService navigation)
        {
            _voucherController = new VoucherController();
            // User = user;
            Navigation = navigation;
            Vouchers = new ObservableCollection<Voucher>(_voucherController.GetAllByTourist(SignInForm.LoggedUser.Id));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
