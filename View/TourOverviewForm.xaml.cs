using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View.ViewModel;
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
using BookingApp.View.ViewModel.TouristViewModels;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourOverviewForm.xaml
    /// </summary>
    public partial class TourOverviewForm : Window
    {
        public TourOverviewForm(User user)
        {
            InitializeComponent();
            this.DataContext = new TourOverviewFormViewModel(user);
        }

    }
}