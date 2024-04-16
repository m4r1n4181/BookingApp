using System;
using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View.ViewModels.TourGuideViewModels;


namespace BookingApp.View
{
    public partial class LiveTourView : Window
    { 
        public LiveTourView()
        {
            InitializeComponent();
            this.DataContext = new LiveTourViewModel();
            
        }

    }
}
