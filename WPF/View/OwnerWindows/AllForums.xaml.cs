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
    /// Interaction logic for AllForums.xaml
    /// </summary>
    public partial class AllForums : Window
    {
        public AllForums()
        {
            InitializeComponent();
            this.DataContext = new AllForumsViewModel();
        }

       
    }
}
