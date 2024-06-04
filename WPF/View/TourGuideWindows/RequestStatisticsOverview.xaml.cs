using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for RequestStatisticsOverview.xaml
    /// </summary>
    public partial class RequestStatisticsOverview : Window
    {

        public RequestStatisticsOverview()
        {
            InitializeComponent();
            this.DataContext = new RequestStatisticsViewModel();

        }

        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RequestStatisticsViewModel viewModel)
            {
                viewModel.LocationComboBox_SelectionChangedCommand.Execute(null);
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RequestStatisticsViewModel viewModel)
            {
                viewModel.LanguageComboBox_SelectionChangedCommand.Execute(null);
            }
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RequestStatisticsViewModel viewModel)
            {
                viewModel.YearComboBox_SelectionChangedCommand.Execute(null);
            }
        }

    }
}

