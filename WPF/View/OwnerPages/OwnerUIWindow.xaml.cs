using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for OwnerUIWindow.xaml
    /// </summary>
    public partial class OwnerUIWindow : Window, INotifyPropertyChanged

    {
        List<Type> navigationStack = new List<Type>();
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                if (value != _header)
                {
                    _header = value;
                    OnPropertyChanged("Header");
                }
            }
        }

        public OwnerUIWindow()
        {
            InitializeComponent();
            DataContext = this;

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.frame.CanGoBack)
            {
                this.frame.GoBack();
            }
            else
            {
                MessageBox.Show("No entries in back navigation history.");
            }

        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.frame.Navigate(menu);

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequestsPage reservationRescheduleRequestsPage = new ReservationRescheduleRequestsPage();
            this.frame.Navigate(reservationRescheduleRequestsPage);

        }
        private void RegisterAccommodationButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AllAccommodationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
