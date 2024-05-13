<<<<<<<< HEAD:WPF/View/OwnerPages/WelcomePageLogIn.xaml.cs
﻿using System;
========
﻿using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using System;
>>>>>>>> feature/create-tour-from-request:WPF/View/OwnerWindows/MonthlyStats.xaml.cs
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
using System.Windows.Navigation;
using System.Windows.Shapes;

<<<<<<<< HEAD:WPF/View/OwnerPages/WelcomePageLogIn.xaml.cs
namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for WelcomePageLogIn.xaml
    /// </summary>
    public partial class WelcomePageLogIn : Page
    {
        public WelcomePageLogIn()
        {
            InitializeComponent();
========
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

>>>>>>>> feature/create-tour-from-request:WPF/View/OwnerWindows/MonthlyStats.xaml.cs
        }
    }
}
