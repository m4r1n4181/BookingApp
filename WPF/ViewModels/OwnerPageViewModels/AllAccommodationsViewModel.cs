using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using BookingApp.WPF.View.OwnerPages;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class AllAccommodationsViewModel : ViewModelBase
    {
     

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
        public RelayCommand ViewAccommodationCommand { get; set; }
        public AllAccommodationsViewModel()
        {
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
        }


          

    }
}
