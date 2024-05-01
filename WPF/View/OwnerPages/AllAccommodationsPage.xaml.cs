using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for AllAccommodationsPage.xaml
    /// </summary>
    public partial class AllAccommodationsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Accommodation> accommodations;
        public ObservableCollection<Accommodation> Accommodations 
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
                OnPropertyChanged();
            }

        }
        public AccommodationController _accommodationController;
        public Accommodation SelectedAccommodation;
        public ReservationRescheduleRequestController _reservationRescheduleRequestsController;

        public AllAccommodationsPage()
        {
            InitializeComponent();
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
            if (AllAccommodationsDataGrid.Items.Count == 0)
            {
                AllAccommodationsDataGrid.ItemsSource = Accommodations;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      private void ViewAccommodationButton_Click(object sender, RoutedEventArgs e)
      {
          // Logika za prikaz detalja smještaja
          // Dobijanje podataka o odabranom smještaju iz SelectedAccommodation
          // Implementacija prikaza detalja odabranog smještaja
      }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Logika za brisanje
            // Dobijanje podataka o odabranom smještaju iz SelectedAccommodation
            // Implementacija brisanja odabranog smještaja
        }

    }
}
