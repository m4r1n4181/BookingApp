using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    public partial class AccommodationReservationToRateForm : Window, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationReservationToRateForm()
        {
            InitializeComponent();
            DataContext = this;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllByOwnerForRating(SignInForm.LoggedUser.Id));
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before activating.");
                return;
            }
            GuestReviewForm guestReviewForm = new GuestReviewForm(SelectedReservation);
            guestReviewForm.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
