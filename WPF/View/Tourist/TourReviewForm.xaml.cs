﻿using BookingApp.Model;
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
    /// Interaction logic for TourReviewForm.xaml
    /// </summary>
    public partial class TourReviewForm : Page
    {
        public TourReviewForm(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = new TourReviewFormViewModel(tourReservation);
        }

    }
}
