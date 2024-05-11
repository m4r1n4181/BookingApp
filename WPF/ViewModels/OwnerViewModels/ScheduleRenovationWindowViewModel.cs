using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ScheduleRenovationWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationController _accommodationController { get; set; }
        public AccommodationRenovationController _accommodationRenovationController { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public ObservableCollection<DateRange> AvailableTerms { get; set; }
        public RelayCommand ScheduleRenovationCommand { get; set; }


        public DateRange SelectedTerm { get; set; }
        public Accommodation Accommodation { get; set; }


        #region NotifyProperties
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
        }
        private DateTime _selectedStartDate = DateTime.Now.Date;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                if (value != _selectedStartDate)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _selectedEndDate = DateTime.Now.Date;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                if (value != _selectedEndDate)
                {
                    _selectedEndDate = value;
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

        public string _description;
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
        #endregion
        public ScheduleRenovationWindowViewModel()
        {
            _accommodationController = new AccommodationController();
            _accommodationRenovationController = new AccommodationRenovationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetByOwner(SignInForm.LoggedUser.Id));
            SearchCommand = new RelayCommand(Execute_SearchCommand, Can_SearchCommand);
            ScheduleRenovationCommand = new RelayCommand(Execute_ScheduleRenovationCommand);

            AvailableTerms = new ObservableCollection<DateRange>();
            SelectedTerm = new DateRange();
            Accommodation = SelectedAccommodation;
            //SelectedAccommodation = accommodation;
           

        }
      
        public bool CanExecute_ScheduleRenovationCommand(object param)
        {
            return SelectedTerm != null;
        }
        public void Execute_ScheduleRenovationCommand(object param)
        {
            AccommodationRenovation accommodationRenovation = new AccommodationRenovation() { Accommodation = SelectedAccommodation, Start = SelectedTerm.Start, End = SelectedTerm.End, Description = Description, IsCancelled = false };
            _accommodationRenovationController.Save(accommodationRenovation);

        }
        public bool Can_SearchCommand(object param)
        {

            return true;
        }
        private void Refresh(List<DateRange> availableTerms)
        {
            AvailableTerms.Clear();
            foreach (DateRange availableTerm in availableTerms)
            {
                AvailableTerms.Add(availableTerm);
            }
            // OnPropertyChanged(nameof(AvailableTerms)); // Dodajte ovu liniju kako bi se osiguralo da se promjene u AvailableTerms reflektiraju u DataGridu
        }

        public void Execute_SearchCommand(object param)
        {
            if (Duration == 0 || SelectedEndDate < SelectedStartDate) //SelectedEndDate == default(DateTime) ||
            {
                MessageBox.Show("Niste uneli ispravane podatke, pokušajte ponovo.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int totalDays = (SelectedEndDate - SelectedStartDate).Days;
            if (Duration > totalDays)
            {
                MessageBox.Show("Niste uneli ispravnu vrednost trajanja, pokušajte ponovo.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            List<DateRange> availableTerms = _accommodationRenovationController.FindAllAvailableTerms(SelectedAccommodation, SelectedStartDate, SelectedEndDate, Duration);
            Refresh(availableTerms);
            if (availableTerms.Count == 0)
            {
                MessageBox.Show("Uneti termini su popunjeni, pokusajte sa nekim drugim terminima.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }


}
