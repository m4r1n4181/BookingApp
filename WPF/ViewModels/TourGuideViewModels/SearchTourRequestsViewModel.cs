using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model.Enums;
using BookingApp.ViewModels;
using BookingApp.WPF.View.TourGuide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TourGuideViewModels
{
    public class SearchTourRequestsViewModel : INotifyPropertyChanged
    {
        private TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public TourRequest SelectedTourRequest { get; set; }

        public ObservableCollection<RequestStatusType> Types { get; set; }

        public RequestStatusType? SelectedStatus { get; set; }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ViewCommand { get; set; }
        public RelayCommand StatisticsCommand { get; set; }


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

        public SearchTourRequestsViewModel()
        {
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

            SearchCommand = new RelayCommand(Search_Click, CanExecuteSearchClick);
            ViewCommand = new RelayCommand(View_Click, CanExecuteViewClick);
            StatisticsCommand = new RelayCommand(Statistics_Click, CanExecuteStatisticsClick);


        }

        private void Refresh()
        {
            TourRequests.Clear();
            foreach (TourRequest tourRequest in _tourRequestController.GetAll())
            {
                TourRequests.Add(tourRequest);
            }
        }

        public void Search_Click(object param)
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
        public bool CanExecuteSearchClick(object param)
        {
            return true;
        }

        public void View_Click(object param)
        {
            if (SelectedTourRequest != null)
            {
                RequestDetails requestDetails = new RequestDetails(SelectedTourRequest);
                requestDetails.Show();
            }
        }
        public bool CanExecuteViewClick(object param)
        {
            return true;
        }

        public void Statistics_Click(object param)
        {

            RequestStatisticsOverview requestStatisticsOverview = new RequestStatisticsOverview();
            requestStatisticsOverview.Show();

        }

        public bool CanExecuteStatisticsClick(object param)
        {
            return true;
        }


    }
}
