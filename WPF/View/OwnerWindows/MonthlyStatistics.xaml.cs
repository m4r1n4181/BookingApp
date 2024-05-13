using BookingApp.DTO;
using BookingApp.Model;
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
    /// Interaction logic for MonthlyStatistics.xaml
    /// </summary>
    public partial class MonthlyStatistics : Window
    {
        public MonthlyStatistics(AccommodationByYearStatisticDto selectedStatistic, Accommodation accommodation)
        {

            InitializeComponent();
            this.DataContext = new ViewModels.OwnerViewModels.MonthlyStatisticsViewModel(selectedStatistic, accommodation);

        }
    }
}