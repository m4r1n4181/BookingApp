using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using BookingApp.WPF.View.OwnerPages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ScheduledRenovationsWindowViewModel : ViewModelBase
    {

        private ObservableCollection<AccommodationRenovation> accommodationRenovations;
        public ObservableCollection<AccommodationRenovation> AccommodationRenovations
        {
            get { return accommodationRenovations; }
            set
            {
                accommodationRenovations = value;
                OnPropertyChanged();
            }
        }

        public AccommodationRenovationController _accommodationRenovationController;

        private AccommodationRenovation _accommodationRenovation;
        public AccommodationRenovation SelectedAccommodationRenovation
        {
            get { return _accommodationRenovation; }

            set
            {
                _accommodationRenovation = value;
                OnPropertyChanged();
            }
        }

        public Accommodation SelectedAccommodation { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand CancelRenovationCommand { get; set; }
        public ScheduledRenovationsWindowViewModel()
        {
            _accommodationRenovationController = new AccommodationRenovationController();

           // SelectedAccommodation = accommodation;

            AccommodationRenovations = new ObservableCollection<AccommodationRenovation>(_accommodationRenovationController.GetAllForOwner(SignInForm.LoggedUser.Id));

            CancelRenovationCommand = new RelayCommand(Execute_CancelRenovationCommand, CanExecute_CancelRenovationCommand);
        }
        public void Refresh()
        {
            AccommodationRenovations.Clear();
            foreach (AccommodationRenovation renovation in _accommodationRenovationController.GetAllForOwner(SignInForm.LoggedUser.Id))
            {
                AccommodationRenovations.Add(renovation);
            }
        }
        public bool CanExecute_CancelRenovationCommand(object param)
        {
            return SelectedAccommodationRenovation != null;
        }

        public void Execute_CancelRenovationCommand(object param)
        {
            _accommodationRenovationController.CancelRenovation(SelectedAccommodationRenovation);
            Refresh();
        }
    }
}
