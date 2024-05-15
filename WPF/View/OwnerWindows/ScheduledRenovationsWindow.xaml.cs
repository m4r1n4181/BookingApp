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

namespace BookingApp.WPF.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for ScheduledRenovationsWindow.xaml
    /// </summary>
    public partial class ScheduledRenovationsWindow : Window
    {
        public ScheduledRenovationsWindow()
        {
            InitializeComponent();
            this.DataContext = new ScheduledRenovationsWindowViewModel();
        }
    }
}
