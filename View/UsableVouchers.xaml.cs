using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View
{
    public partial class UsableVouchers : Window
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
        public UsableVouchers(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _voucherController = new VoucherController();
            Vouchers = new ObservableCollection<Voucher>(_voucherController.GetVouchersThatDidntExpire(user.Id));
        }

        private void UseVoucher_Click(object sender, RoutedEventArgs e)
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
