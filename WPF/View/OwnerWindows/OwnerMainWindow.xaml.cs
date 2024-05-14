using BookingApp.Controller;
using BookingApp.WPF.View.OwnerPages;
using BookingApp.WPF.View.OwnerWindows;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        private AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public OwnerMainWindow()
        {
            InitializeComponent();
            this.DataContext = new OwnerMainViewModel();

        }

    }
}
