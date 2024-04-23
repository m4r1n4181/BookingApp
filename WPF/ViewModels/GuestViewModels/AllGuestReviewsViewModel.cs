using BookingApp.Controller;
using BookingApp.Model;
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

namespace BookingApp.WPF.ViewModels.GuestViewModels
{
    public class AllGuestReviewsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GuestReview> GuestReviews { get; set; }
        public GuestReviewController _guestReviewController {  get; set; }

        public User LoggedInUser { get; set; }
        public AllGuestReviewsViewModel()
        {
            LoggedInUser = SignInForm.LoggedUser;
            _guestReviewController = new GuestReviewController();
            GuestReviews = new ObservableCollection<GuestReview>(_guestReviewController.GetGuestReviews(LoggedInUser.Id));
        }   

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
