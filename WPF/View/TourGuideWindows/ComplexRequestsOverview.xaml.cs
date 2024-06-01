using BookingApp.Controller;
using BookingApp.Domain.Models;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TourGuideWindows
{
    /// <summary>
    /// Interaction logic for ComplexRequestsOverview.xaml
    /// </summary>
    public partial class ComplexRequestsOverview : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ComplexTourRequest _selectedComplexRequest;
        public ComplexTourRequest SelectedComplexRequest
        {
            get { return _selectedComplexRequest; }
            set
            {
                if (_selectedComplexRequest != value)
                {
                    _selectedComplexRequest = value;
                    OnPropertyChanged();
                    UpdateAvailableDates();
                }
            }
        }

        private ObservableCollection<DateTime> _availableDates;
        public ObservableCollection<DateTime> AvailableDates
        {
            get { return _availableDates; }
            set
            {
                if (_availableDates != value)
                {
                    _availableDates = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }


        private TourGuideController _tourGuideController;

        private ComplexTourRequestController _complexTourRequestController;
        public ObservableCollection<ComplexTourRequest> ComplexRequests { get; set; }

        public ComplexRequestsOverview()
        {
            InitializeComponent();

            _tourGuideController = new TourGuideController();
            _complexTourRequestController = new ComplexTourRequestController();
            ComplexRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestController.GetAll());

            DataContext = this;
        }

        private void UpdateAvailableDates()
        {
            if (SelectedComplexRequest != null)
            {
                var guideId = SignInForm.LoggedUser.Id; 
                var complexRequestId = SelectedComplexRequest.Id;
                AvailableDates = new ObservableCollection<DateTime>(_complexTourRequestController.GetAvailableDatesForTourPart(guideId, complexRequestId));
            }
            else
            {
                AvailableDates = new ObservableCollection<DateTime>();
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComplexRequest != null && SelectedDate != null)
            {
               // _complexTourRequestController.AcceptRequest(SelectedComplexRequest.Id, SelectedDate);
               // ComplexRequests.Remove(SelectedComplexRequest);
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComplexRequest != null)
            {
               // _complexTourRequestController.DeclineRequest(SelectedComplexRequest.Id);
                //ComplexRequests.Remove(SelectedComplexRequest);
            }
        }

    }
}
