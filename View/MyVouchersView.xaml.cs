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
    public partial class MyVouchersView : Window
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public VoucherController _voucherController;
        public Voucher _voucher;
        public User User { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MyVouchersView(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _voucherController = new VoucherController();
            Vouchers = new ObservableCollection<Voucher>(_voucherController.GetAllByTourist(user.Id));
        }
    }
}
