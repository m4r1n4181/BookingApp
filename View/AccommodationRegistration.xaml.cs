using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO.Packaging;
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
    /// Interaction logic for AccommodationRegistration.xaml
    /// </summary>
    public partial class AccommodationRegistration : Window
    {
        private AccommodationController _accommodationController;
        
        public AccommodationRegistration()
        {
            InitializeComponent();
            _accommodationController = new AccommodationController();

        }

        // cancel button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // save accommodation
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
                // Kreiranje nove instance Accommodation na osnovu unetih podataka iz TextBox-ova
                Accommodation accommodation = new Accommodation(
                    Name.Text,
                    (AccommodationType)Enum.Parse(typeof(AccommodationType), Type.Text),
                    new Location(City.Text, Country.Text),
                    int.Parse(MaxGuests.Text),
                    int.Parse(MinDays.Text),
                    int.Parse(CancellationDays.Text),
                    Pictures.Text.Split(',').ToList()
                );

            // Poziv funkcije iz kontrolera
            _accommodationController.RegisterAccommondation(accommodation);

                // Nakon što je smeštaj registrovan, možete obavestiti korisnika ili izvršiti neku drugu logiku
                MessageBox.Show("Accommodation successfully saved!");
                // Ili zatvorite prozor ako je to potrebno
                this.Close();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
