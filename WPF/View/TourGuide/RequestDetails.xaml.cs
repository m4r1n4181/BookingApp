using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Service;
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
    /// Interaction logic for RequestDetails.xaml
    /// </summary>
    public partial class RequestDetails : Window, INotifyPropertyChanged

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




        public RequestDetails(TourRequest tourRequest)
        {
            InitializeComponent();
            SelectedTourRequest = tourRequest;
            DataContext = this;

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
        }
        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null)
            {
                 SelectedTourRequest.RequestStatus = RequestStatusType.Approved;
                 _tourRequestController.Update(SelectedTourRequest);
                 _tourRequestController.SaveTourFromRequest(SelectedTourRequest, SelectedDate);
                 Close();
                //uraditi ovde refresh 

                _notificationController.SendNotification(SelectedTourRequest.Id, SelectedTourRequest.Tourist, "Vaš zahtev je odobren za datum " + SelectedDate.ToShortDateString(), NotificationStatus.unread);
            }
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

            string message = "Vaš zahtev je odbijen jer nema dostupnih termina";
            _notificationController.SendNotification(SelectedTourRequest.Id, SelectedTourRequest.Tourist, message, NotificationStatus.unread);

        }
    }
}
