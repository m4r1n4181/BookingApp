using BookingApp.WPF.ViewModels.TourGuideViewModels;
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
    /// Interaction logic for RequestStatistics.xaml
    /// </summary>
    public partial class UserRequestStatistics : Page
    {
        public UserRequestStatistics(NavigationService navigationService)
        {
            InitializeComponent();
            var viewModel = new UserRequestStatisticsViewModel(navigationService);
            this.DataContext = viewModel;
        }
        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UserRequestStatisticsViewModel viewModel)
            {
                viewModel.LocationComboBox_SelectionChangedCommand.Execute(null);
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UserRequestStatisticsViewModel viewModel)
            {
                viewModel.LanguageComboBox_SelectionChangedCommand.Execute(null);
            }
        }

    }
}
