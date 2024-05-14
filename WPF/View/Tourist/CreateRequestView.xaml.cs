using BookingApp.WPF.ViewModels.TouristViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for CreateRequestView.xaml
    /// </summary>
    public partial class CreateRequestView : Page
    {
        public CreateRequestView(NavigationService navigationService)
        {
            InitializeComponent();
            var viewModel = new CreateRequestViewModel(navigationService);
            this.DataContext = viewModel;
        }
    }
}
