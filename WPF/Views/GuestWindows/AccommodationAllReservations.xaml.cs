using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.WPF.Views.GuestWindows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View.GuestWindows
{
    public partial class AccommodationAllReservations : Window//, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationAllReservations()
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = SignInForm.LoggedUser;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllByGuest(LoggedInUser.Id));
        }

      /*  private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before activating.");
                return;
            }

            OwnerReviewForm ownerReviewForm = new OwnerReviewForm(SelectedReservation);
            ownerReviewForm.ShowDialog();
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before editing.");
                return;
            }
            AccommodationReservationEdit accommodationReservationEdit = new AccommodationReservationEdit(SelectedReservation);
            accommodationReservationEdit.Show();

            //this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before canceling.");
                return;
            }
            _accommodationReservationService.CancelReservation(SelectedReservation.Id);
           // MessageBox.Show("Reservation canceled successfully.");
            Refresh();
            
            
        }

        private void Refresh()
        {
            AccommodationReservations.Clear();
            int loggedId = SignInForm.LoggedUser.Id;
            foreach(AccommodationReservation reservation in _accommodationReservationService.GetAllByGuest(loggedId))
            {
                AccommodationReservations.Add(reservation);
            }
        }
    }
}
