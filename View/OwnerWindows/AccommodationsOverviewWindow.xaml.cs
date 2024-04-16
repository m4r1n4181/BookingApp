using BookingApp.Controller;
using BookingApp.Model;
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
using System.Windows.Shapes;

namespace BookingApp.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationsOverviewWindow.xaml
    /// </summary>
    public partial class AccommodationsOverviewWindow : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationController _accommodationController;
        public Accommodation SelectedAccommodation;
        public AccommodationsOverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            //LoggedInUser = user;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));

        }
    }
}
