using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TourGuide
{
    /// <summary>
    /// Interaction logic for SearchTourRequests.xaml
    /// </summary>
    public partial class SearchTourRequests : Window, INotifyPropertyChanged
    {
        private TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public TourRequest SelectedTourRequest { get; set; }

        public ObservableCollection<RequestStatusType> Types { get; set; }

        public RequestStatusType? SelectedStatus { get; set; }


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


        private RequestStatusType _requestStatus;
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SearchTourRequests()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRequestController = new TourRequestController();

            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetAllWithLocations());
            City = "";
            Country = "";
            Types = new ObservableCollection<RequestStatusType>
            {
                RequestStatusType.Approved,
                RequestStatusType.Standby,
                RequestStatusType.Declined
            };
            SelectedStatus = null;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

        }    

    

        public void Update()
        {
            TourRequests.Clear();
            foreach (TourRequest tourRequest in _tourRequestController.GetAll())
            {
                TourRequests.Add(tourRequest);
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            TourRequestSearch tourRequestSearch = new TourRequestSearch();
            tourRequestSearch.City = City;
            tourRequestSearch.Country = Country;
            tourRequestSearch.Status = SelectedStatus;
            tourRequestSearch.MaxTourists = MaxTourists;
            tourRequestSearch.StartDate = StartDate;
            tourRequestSearch.EndDate = EndDate;

            TourRequests.Clear();
            foreach (TourRequest tourRequest in _tourRequestController.SearchTourRequest(tourRequestSearch))
            {
                TourRequests.Add(tourRequest);
            }

        }

       

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourRequest == null)
            {
                return;
            }

            SelectedTourRequest.RequestStatus = RequestStatusType.Approved;
            _tourRequestController.Update(SelectedTourRequest);
            Close();   

            //uraditi ovde refresh 
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourRequest == null)
            {
                return;
            }
            SelectedTourRequest.RequestStatus = RequestStatusType.Declined;
            _tourRequestController.Update(SelectedTourRequest);
            Close();
            //uraditi ovde refresh 

        }

    }
}
