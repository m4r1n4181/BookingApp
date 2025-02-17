﻿using BookingApp.Model;
using BookingApp.WPF;
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
    /// Interaction logic for YearlyStatistics.xaml
    /// </summary>
    public partial class YearlyStatistics : Window
    {
        public YearlyStatistics(Accommodation accommodation)
        {
            this.DataContext = new ViewModels.OwnerViewModels.YearlyStatisticsViewModel(accommodation);
            InitializeComponent();
        }
    }
}
