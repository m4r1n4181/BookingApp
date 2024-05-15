using BookingApp.Model;
using BookingApp.WPF.ViewModels.Tourist;
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
using System.Windows.Shapes;
using BookingApp.WPF.View.Tourist;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Navigation;
using BookingApp.View;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindowViewModel : Window
    {
        public NavigationService Navigation { get; set; }
        public TouristMainWindowViewModel(NavigationService navigation)
        {
            Navigation = navigation;
        }

    }
}

