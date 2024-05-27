using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
using System;
using System.Collections;
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

namespace BookingApp.WPF.View.TourGuideWindows
{
    /// <summary>
    /// Interaction logic for SearchTourRequests.xaml
    /// </summary>
    public partial class SearchTourRequests : Window
    {
       
        public SearchTourRequests()
        {
            InitializeComponent();
            this.DataContext = new SearchTourRequestsViewModel();
        
        }    

    }
}
