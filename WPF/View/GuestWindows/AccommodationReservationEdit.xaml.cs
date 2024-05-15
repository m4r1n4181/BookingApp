using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels.GuestViewModels;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.Views.GuestWindows
{
    /// <summary>
    /// Interaction logic for AccommodationReservationEdit.xaml
    /// </summary>
    public partial class AccommodationReservationEdit : Window//, INotifyPropertyChanged
    {

        public AccommodationReservationEdit(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            DataContext = new AccommodationReservationEditViewModel(accommodationReservation);

        }

    }
}
