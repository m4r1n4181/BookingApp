using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
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

namespace BookingApp.ViewModels.GuestViewModels
{
    public class AccommodationReservationToRateOwnerFormViewModel
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public RelayCommand RateCommand { get; set; }

        public AccommodationReservationToRateOwnerFormViewModel() 
        { 
             LoggedInUser = SignInForm.LoggedUser;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationReservationService = new AccommodationReservationService(
                Injector.CreateInstance<IAccommodationReservationRepository>(),
                Injector.CreateInstance<IAccommodationRepository>(),
                Injector.CreateInstance<INotificationRepository>(),
                Injector.CreateInstance<IOwnerReviewRepository>(),
                Injector.CreateInstance<ISuperGuestRepository>());
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllByGuestForRating(LoggedInUser.Id));
            RateCommand = new RelayCommand(Rate_Click);
        }

        private void Rate_Click(object sender)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Please select a reservation before rating.");
                return;
            }
            
            OwnerReviewForm ownerReviewForm = new OwnerReviewForm(SelectedReservation);
            ownerReviewForm.ShowDialog();
            Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Refresh()
        {
            AccommodationReservations.Clear();
            int loggedId = SignInForm.LoggedUser.Id;
            foreach (AccommodationReservation reservation in _accommodationReservationService.GetAllByGuestForRating(LoggedInUser.Id))
            {
                AccommodationReservations.Add(reservation);
            }
        }
    }
}
