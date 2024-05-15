using BookingApp.Controller;
using BookingApp.Service;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using BookingApp.WPF.View.OwnerPages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class AllAccommodationsViewModel : ViewModelBase
    {

        public ICommand DeleteAccommodationCommand { get; }
        public ICommand ViewAccommodationCommand { get; }

        #region NotifyProperties
        private ObservableCollection<Accommodation> accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
                OnPropertyChanged();
            }
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public AccommodationController _accommodationController;
        

        public AllAccommodationsViewModel()
        {
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
            DeleteAccommodationCommand = new RelayCommand(DeleteAccommodation);
           // ViewAccommodationCommand = new RelayCommand(ViewAccommodation);
        }

        

        private void DeleteAccommodation(object param)
        {
            if (param is Accommodation accommodation)
            {
                _accommodationController.Delete(accommodation);
                Accommodations.Remove(accommodation);
                MessageBox.Show("Accommodation successfully deleted.");
            }
        }

      /*  private void ViewAccommodation(object param)
        {
            if (param is Accommodation accommodation)
            {
                
            }
        }
      */

    }
}
