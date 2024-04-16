using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.ViewModels.GuestViewModels;
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
    /// Interaction logic for OwnerReviewForm.xaml
    /// </summary>
    public partial class OwnerReviewForm : Window
    {

        public OwnerReviewForm(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            DataContext = new OwnerReviewFormViewModel(accommodationReservation);

        }

    }
}
