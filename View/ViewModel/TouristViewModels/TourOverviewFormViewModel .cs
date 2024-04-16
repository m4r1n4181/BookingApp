using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using BookingApp.View;
using BookingApp.View.ViewModel.TouristViewModels;

namespace BookingApp.View.ViewModel.TouristViewModels
{
    public class TourOverviewFormViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> Tours { get; set; }
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

        public ICommand SearchCommand { get; private set; }
        public ICommand ReserveCommand { get; private set; }
        public ICommand MyToursCommand { get; private set; }
        public ICommand VouchersCommand { get; private set; }

        // Constructor
        public TourOverviewFormViewModel(User user)
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());
            User = user;

            SearchCommand = new RelayCommand(SearchTours);
            ReserveCommand = new RelayCommand(Reserve);
            MyToursCommand = new RelayCommand(MyTours);
            VouchersCommand = new RelayCommand(Vouchers);
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
                MaxTourists = MaxTourists
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
                TourReservationForm viewReservationForm = new TourReservationForm(SelectedTour, User);
                viewReservationForm.Show();
            }
        }

        // Method to open MyTours view
        private void MyTours()
        {
            MyToursView myToursView = new MyToursView(User);
            myToursView.Show();


        }

        // Method to open MyVouchers view
        private void Vouchers()
        {
            MyVouchersView myVouchersView = new MyVouchersView();
            myVouchersView.Show();
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
