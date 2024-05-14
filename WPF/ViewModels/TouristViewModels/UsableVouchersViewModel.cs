using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class UsableVouchersViewModel
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public VoucherController _voucherController;
        public Voucher _voucher;
        public User User { get; set; }
        public Voucher SelectedVoucher { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand UseVoucherCommand { get; private set; }
        public UsableVouchersViewModel()
        {
            _voucherController = new VoucherController();
            Vouchers = new ObservableCollection<Voucher>(_voucherController.GetVouchersThatDidntExpire(SignInForm.LoggedUser.Id));
            UseVoucherCommand = new RelayCommand(UseVoucher);
        }
        private void UseVoucher(object param)
        {

            if (SelectedVoucher != null)
            {
                // Označi vaučer kao korišćenog
                SelectedVoucher.IsUsed = true;

                // Ažuriraj vaučer u repozitorijumu
                _voucherController.Update(SelectedVoucher);

                // Ukloni vaučer iz liste prikazanih vaučera
                Vouchers.Remove(SelectedVoucher);

                MessageBox.Show("Voucher successfully used!");
            }
            else
            {
                MessageBox.Show("Please select a voucher to use.");
            }
        }
    }
}