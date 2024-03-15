using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestForm.xaml
    /// </summary>
    public partial class GuestForm : Window
    {
        private AccommodationController _accommodationController;
        public static ObservableCollection<Accommodation> Accommodations { get; set; }


        public Accommodation SelectedAccommodation { get; set; }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public GuestForm()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in _accommodationController.GetAll())
            {
                Accommodations.Add(accommodation);
            }

        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchParams searchParams = new AccommodationSearchParams();
            searchParams.Name = Name;
            searchParams.City = "";
            searchParams.Country = "";
            searchParams.Type = null;
            searchParams.MaxGests = -1; 
            searchParams.MinReservationDays = -1;
            Accommodations.Clear();
            foreach (Accommodation accommodation in _accommodationController.SearchAccommodations(searchParams))
            {
                Accommodations.Add(accommodation);
            }
        }
    }
}
