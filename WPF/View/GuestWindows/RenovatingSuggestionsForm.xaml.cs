using BookingApp.Model;
using BookingApp.ViewModels.GuestViewModels;
using BookingApp.WPF.ViewModels.GuestViewModels;
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

namespace BookingApp.WPF.View.GuestWindows
{
    public partial class RenovatingSuggestionsForm : Window
    {

        public RenovatingSuggestionsForm(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            DataContext = new RenovatingSuggestionsViewModel(accommodationReservation);
        }

       
    }
}
