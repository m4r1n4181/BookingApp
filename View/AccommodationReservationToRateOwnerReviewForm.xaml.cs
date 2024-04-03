using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
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
    /// Interaction logic for AccommodationReservationToRateOwnerReview.xaml
    /// </summary>
    public partial class AccommodationReservationToRateOwnerReview : Window
    {

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationReservationToRateOwnerReview(User user)
        {
            InitializeComponent();

            DataContext = this;
            LoggedInUser = user;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllByOwnerForRating(user.Id));
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before activating.");
                return;
            }

            OwnerReviewForm ownerReviewForm = new OwnerReviewForm(SelectedReservation);
            ownerReviewForm.ShowDialog();
        }


        /*public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }






    }
