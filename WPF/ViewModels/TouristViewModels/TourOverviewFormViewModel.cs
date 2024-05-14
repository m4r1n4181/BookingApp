using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using BookingApp.WPF.View.Tourist;
using System.Windows.Navigation;
using BookingApp.View;
using System;

namespace BookingApp.WPF.ViewModels.Tourist
{
    public class TourOverviewFormViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public NavigationService NavigationService { get; set; }
        public User User { get; set; }
        private readonly TourController _tourController;

        public Tour SelectedTour { get; set; }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxTourists;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists)
                {
                    _maxTourists = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand ReserveCommand { get; private set; }
        public RelayCommand MyToursCommand { get; private set; }
        public RelayCommand VouchersCommand { get; private set; }
        public RelayCommand CreateRequestCommand { get; private set; }

        // Constructor
        public TourOverviewFormViewModel(NavigationService navigation)
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());
            User = SignInForm.LoggedUser;
            NavigationService = navigation;

            SearchCommand = new RelayCommand(SearchTours);
            ReserveCommand = new RelayCommand(Reserve);
            MyToursCommand = new RelayCommand(MyTours);
            VouchersCommand = new RelayCommand(Vouchers);
            CreateRequestCommand = new RelayCommand(Request);
        }

        private void Request()
        {
            CreateRequestView createRequestView = new CreateRequestView(NavigationService);
            NavigationService.Navigate(createRequestView);
        }

        // Method to handle searching for tours
        private void SearchTours()
        {
            TourSearchParams searchParams = new TourSearchParams
            {
                City = City,
                Country = Country,
                Language = Language,
                Duration = Duration,
               // MaxTourists = MaxTourists
            };

            Tours.Clear();
            foreach (Tour tour in _tourController.SearchTours(searchParams))
            {
                Tours.Add(tour);
            }
        }

        // Method to handle tour reservation
        private void Reserve()
        {
            if (SelectedTour != null)
            {
               // TourReservationForm viewReservationForm = new TourReservationForm(SelectedTour, User);
               // viewReservationForm.Show();
            }
        }

        // Method to open MyTours view
        private void MyTours()
        {
            MyToursView myToursView = new MyToursView(NavigationService);
            NavigationService.Navigate(myToursView);


        }

        // Method to open MyVouchers view
        private void Vouchers()
        {
            MyVouchersView myVouchersView = new MyVouchersView(NavigationService);
            NavigationService.Navigate(myVouchersView);
        }
        public bool CanExecuteCommand(object param)
        {
            return true;
        }
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}