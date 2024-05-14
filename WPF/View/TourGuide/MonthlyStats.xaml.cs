using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for MonthlyStats.xaml
    /// </summary>
    public partial class MonthlyStats : Window
    {
        public MonthlyStats(int selectedYear)
        {
            InitializeComponent();
            this.DataContext = new MonthlyStatsViewModel(selectedYear);

        }
    }
}
