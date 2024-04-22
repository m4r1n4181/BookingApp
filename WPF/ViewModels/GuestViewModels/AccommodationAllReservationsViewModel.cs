using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.WPF.Views.GuestWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.GuestViewModels
{
    public class AccommodationAllReservationsViewModel
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public RelayCommand EditCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

      

        public AccommodationAllReservationsViewModel()
        {
            LoggedInUser = SignInForm.LoggedUser;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllByGuest(LoggedInUser.Id));

            EditCommand = new RelayCommand(Edit_Click);
            CancelCommand = new RelayCommand(Cancel_Click);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Edit_Click(object sender)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation for editing.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AccommodationReservationEdit accommodationReservationEdit = new AccommodationReservationEdit(SelectedReservation);
            accommodationReservationEdit.Show();

            //this.Close();
        }
        
        private void Cancel_Click(object sender)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation for canceling.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool result = _accommodationReservationService.CancelReservation(SelectedReservation.Id);
            if (result == false)
            {
                MessageBox.Show("Cannot cancel reservation.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (result == true)
            {
                MessageBox.Show("You have successfully canceled your reservation!", "Canceled!", MessageBoxButton.OK);
            }


            Refresh();


        }

        private void Refresh()
        {
            AccommodationReservations.Clear();
            int loggedId = SignInForm.LoggedUser.Id;
            foreach (AccommodationReservation reservation in _accommodationReservationService.GetAllByGuest(loggedId))
            {
                AccommodationReservations.Add(reservation);
            }
        }
    }
}
