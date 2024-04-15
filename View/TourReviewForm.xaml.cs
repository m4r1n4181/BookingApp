using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.ViewModel.TouristViewModels;
using BookingApp.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for TourReviewForm.xaml
    /// </summary>
    public partial class TourReviewForm : Window
    {
        public TourReviewForm(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = new TourReviewFormViewModel(tourReservation);
        }

    }
    }
