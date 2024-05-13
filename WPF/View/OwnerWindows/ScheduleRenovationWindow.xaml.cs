<<<<<<< HEAD:WPF/View/OwnerWindows/ScheduleRenovationWindow.xaml.cs
﻿using BookingApp.WPF.ViewModels.OwnerPageViewModels;
using BookingApp.WPF.ViewModels.OwnerViewModels;
=======
﻿using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
>>>>>>> feature/create-tour-from-request:WPF/View/TourGuide/ActiveTours.xaml.cs
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
    /// Interaction logic for ScheduleRenovationWindow.xaml
    /// </summary>
    public partial class ScheduleRenovationWindow : Window
    {
        public ScheduleRenovationWindow()
        {
            InitializeComponent();
            this.DataContext = new ScheduleRenovationWindowViewModel();

        }
    }
}
