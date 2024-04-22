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

namespace BookingApp.View.ViewModel.TouristViewModels
{
    public class MyVouchersViewModel
    {   
            public ObservableCollection<Voucher> Vouchers { get; set; }
            public VoucherController _voucherController;
            public Voucher _voucher;
           // public User User { get; set; }

            public MyVouchersViewModel( )
            {
                _voucherController = new VoucherController();
               // User = user;
                Vouchers = new ObservableCollection<Voucher>(_voucherController.GetAllByTourist(SignInForm.LoggedUser.Id));
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }

