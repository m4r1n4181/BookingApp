using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View.ViewModels.TourGuideViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
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
    /// Interaction logic for AllTourGuideReservations.xaml
    /// </summary>
    /// mozda dodati polje za datetime cisto da imam uvid u to kad treba tura da se desi al svakako radi 48sati pre...
    public partial class FutureTours : Window
    {

        public FutureTours()
        {
            InitializeComponent();
            this.DataContext = new FutureToursViewModel();

        }

    }
}
