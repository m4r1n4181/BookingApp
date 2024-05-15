using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
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
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class MyRequestsViewModel
    {
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public TourRequestController _tourRequestController;
        public TourRequest _tourRequest;
        public NavigationService Navigation { get; set; }
        // public User User { get; set; }
        private static string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private static RequestStatusType _requestStatus;
        public RequestStatusType RequestStatus
        {
            get => _requestStatus;
            set
            {
                if (value != _requestStatus)
                {
                    _requestStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyRequestsViewModel(NavigationService navigation)
        {
            _tourRequestController = new TourRequestController();
            // User = user;
            Navigation = navigation;
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetAllTourRequestsForUser(SignInForm.LoggedUser.Id));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
