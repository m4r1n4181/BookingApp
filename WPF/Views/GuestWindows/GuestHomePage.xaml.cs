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

namespace BookingApp.WPF.Views.GuestWindows
{
    /// <summary>
    /// Interaction logic for GuestHomePage.xaml
    /// </summary>
    public partial class GuestHomePage : Window
    {
        public GuestHomePage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Reschedule_View_Button_Click(object sender, RoutedEventArgs e)
        {
            GuestRequests guestRequests = new GuestRequests();
            guestRequests.Show();
        }
    }
}
