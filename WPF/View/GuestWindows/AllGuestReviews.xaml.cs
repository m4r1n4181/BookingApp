using BookingApp.WPF.ViewModels.GuestViewModels;
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

namespace BookingApp.WPF.View.GuestWindows
{
    /// <summary>
    /// Interaction logic for AllGuestReviews.xaml
    /// </summary>
    public partial class AllGuestReviews : Window
    {
        public AllGuestReviews()
        {
            InitializeComponent();
            DataContext = new AllGuestReviewsViewModel();
        }
    }
}
