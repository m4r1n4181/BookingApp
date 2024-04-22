using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.ViewModels.TourGuideViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View
{
    public partial class CreateTourForm : Window
    {

        public CreateTourForm()
        {
            InitializeComponent();
            this.DataContext = new CreateTourFormViewModel();
        }

    }
}



