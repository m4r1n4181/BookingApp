using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for SortedAccommodations.xaml
    /// </summary>
    public partial class SortedAccommodations : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationController _accommodationController;
        public SortedAccommodations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            //LoggedInUser = user;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllSorted());
           
        }

    }
}
