﻿using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View.ViewModels.TourGuideViewModels;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for ReviewDetails.xaml
    /// </summary>
    public partial class ReviewDetails : Window
    {
        public ReviewDetails(Tour tour)
        {
            InitializeComponent();
            this.DataContext = new ReviewDetailsViewModel(tour);
        
        }
     
    }
}
