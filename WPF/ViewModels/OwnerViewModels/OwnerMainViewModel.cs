using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BookingApp.Controller;
using BookingApp.View;
using BookingApp.WPF.View.OwnerWindows;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{

    public class OwnerMainViewModel : ViewModelBase
    {
        private AccommodationOwnerReviewController _accommodationOwnerReviewController;

        private Visibility _superOwnerVisibility;

        public Visibility SuperOwnerVisibility
        {
            get => _superOwnerVisibility;
            set
            {
                _superOwnerVisibility = value;
                OnPropertyChanged(nameof(SuperOwnerVisibility));
            }
        }

        public ICommand MyAccommodationsCommand { get; private set; }
       
        public ICommand LogOutCommand { get; private set; }
        public ICommand ScheduleRenovationCommand { get; private set; }
        public ICommand ScheduledRenovationsCommand { get; private set; }
        public OwnerMainViewModel()
        {
             _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            _superOwnerVisibility = Visibility.Visible;
            if (!_accommodationOwnerReviewController.IsSuperOwner(SignInForm.LoggedUser.Id))
            {
                _superOwnerVisibility = Visibility.Hidden;
            }
            MyAccommodationsCommand = new RelayCommand(OpenMyAccommodations);
            LogOutCommand = new RelayCommand(PerformLogOut);
            ScheduleRenovationCommand = new RelayCommand(OpenScheduleRenovation);
            ScheduledRenovationsCommand = new RelayCommand(OpenScheduledRenovations);
        }
        private void OpenMyAccommodations(object obj)
        {
            AllAccommodationsWindow allAccommodationsWindow = new AllAccommodationsWindow();
            allAccommodationsWindow.Show();
        }


        private void PerformLogOut(object obj)
        {

            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            //this.Close();
        }

        private void OpenScheduleRenovation(object obj)
        {
            ScheduleRenovationWindow schedule = new ScheduleRenovationWindow();
            schedule.Show();
        }

        private void OpenScheduledRenovations(object obj)
        {
            ScheduledRenovationsWindow scheduled = new ScheduledRenovationsWindow();
            scheduled.Show();
        }
    }
}
