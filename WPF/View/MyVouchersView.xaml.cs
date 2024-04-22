using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View.ViewModel.TouristViewModels;

namespace BookingApp.View
{
    public partial class MyVouchersView : Window
    { 
        public MyVouchersView()
        {
            InitializeComponent();
            this.DataContext = new MyVouchersViewModel();
        }
    }
}
