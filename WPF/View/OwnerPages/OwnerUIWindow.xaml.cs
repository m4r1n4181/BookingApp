using BookingApp.WPF.ViewModels;
using BookingApp.WPF.ViewModels.OwnerPageViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for OwnerUIWindow.xaml
    /// </summary>
    public partial class OwnerUIWindow : Window, INotifyPropertyChanged

    {
        string questionMark = "\u003F";
        string plusMark = "\uFF0B";
        string allAccom = "\u22EE";
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public OwnerUIWindow()
        {
            InitializeComponent();
            Header = questionMark + " Requests to move reservation";
            this.DataContext = this;
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

        private void RegisterAccommodationButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationPage registerAccommodationPage = new RegisterAccommodationPage();
            Header = plusMark + " Accommodation registration";
            this.frame.Navigate(registerAccommodationPage);
        }
        private void AllAccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            
            AllAccommodationsPage allAccommodationsPage = new AllAccommodationsPage();
            Header = " " +allAccom+ " All accommodations";
            this.frame.Navigate(allAccommodationsPage);
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
