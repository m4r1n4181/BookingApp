using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Service;
using BookingApp.WPF.ViewModels.TourGuideViewModels;
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

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for RequestDetails.xaml
    /// </summary>
    public partial class RequestDetails : Window

    {
        public RequestDetails(TourRequest tourRequest)
        {
            InitializeComponent();
            this.DataContext = new RequestDetailsViewModel(tourRequest);

        }
    }
}
