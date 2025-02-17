﻿using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model.Enums;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.TourGuideViewModels
{
    public class RequestDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _language;
        public string Languages
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

        private string _description;
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
        private NotificationController _notificationController;
        public TourRequest SelectedTourRequest { get; set; }

        private TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public ObservableCollection<DateTime> Dates { get; set; }
        public DateTime SelectedDate { get; set; }

        public RelayCommand ApproveCommand { get; set; }
        public RelayCommand DeclineCommand { get; set; }

        public RequestDetailsViewModel(TourRequest tourRequest)
        {
            SelectedTourRequest = tourRequest;

            Location = $"{tourRequest.Location.City}, {tourRequest.Location.Country}";
            Languages = tourRequest.Language;
            MaxTourists = tourRequest.MaxTourists;
            Description = tourRequest.Description;

            _notificationController = new NotificationController();
            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetAllWithLocations());

            DateTime startDate = SelectedTourRequest.StartDate;
            DateTime endDate = SelectedTourRequest.EndDate;

            Dates = new ObservableCollection<DateTime>();
            for (DateTime date = tourRequest.StartDate; date <= tourRequest.EndDate; date = date.AddDays(1))
            {
                Dates.Add(date);
            }

            ApproveCommand = new RelayCommand(Approve_Click, CanExecuteApproveClick);
            DeclineCommand = new RelayCommand(Decline_Click, CanExecuteDeclineClick);
        }

        public void Approve_Click(object param)
        {
            if (SelectedDate == null || SelectedTourRequest == null)
            {
                return;
            }
            TourRequest tourRequest = _tourRequestController.ApproveRequest(SelectedTourRequest.Id, SelectedDate);
            if(tourRequest == null)
            {
                MessageBox.Show("Vec ste zauzeti na taj dan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        public bool CanExecuteApproveClick(object param)
        {
            return true;

        }

        public void Decline_Click(object param)
        {
            if (SelectedDate == null || SelectedTourRequest == null)
            {
                return;
            }
            _tourRequestController.DeclineRequest(SelectedTourRequest.Id);

        }

        public bool CanExecuteDeclineClick(object param)
        {
            return true;

        }


    }
}
