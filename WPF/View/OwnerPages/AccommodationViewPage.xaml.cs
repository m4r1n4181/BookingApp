﻿using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.WPF.ViewModels.OwnerPageViewModels;
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

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for AccommodationViewPage.xaml
    /// </summary>
    public partial class AccommodationViewPage : Page
    {
        
        public AccommodationViewPage(Accommodation selectedAccommodation)
        {
            InitializeComponent();
           this.DataContext = new AccommodationViewModel(selectedAccommodation);
         
        }

        /*
        private void ViewStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            YearlyStatisticsPage yearlyStatisticsPage = new YearlyStatisticsPage(accommodation);
            this.NavigationService.Navigate(yearlyStatisticsPage);

        } */
    }
}
