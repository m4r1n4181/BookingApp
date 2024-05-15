using BookingApp.Controller;
using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Enums;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
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

namespace BookingApp.WPF.ViewModels.GuestViewModels
{
    public class RenovatingSuggestionsViewModel
    {
        public ObservableCollection<RenovatingUrgencyLevel> RenovatingUrgencyLevels { get; set; }
        public RenovatingUrgencyLevel SelectedRenovatingUrgencyLevel { get; set; }
        public AccommodationReservation SelectedAccommodationReservation;
        public RenovatingRequestController _renovatingRequestController { get; set; }
        public RelayCommand AddSuggestionCommand { get; set; }

 
        public RenovatingSuggestionsViewModel(AccommodationReservation accommodationReservation) 
        {
            _renovatingRequestController = new RenovatingRequestController(new Service.RenovatingRequestService(Injector.CreateInstance<IRenovatingRequestRepository>()));
            SelectedAccommodationReservation = accommodationReservation;
            RenovatingUrgencyLevels = new ObservableCollection<RenovatingUrgencyLevel>();
            RenovatingUrgencyLevels.Add(RenovatingUrgencyLevel.Level1);
            RenovatingUrgencyLevels.Add(RenovatingUrgencyLevel.Level2);
            RenovatingUrgencyLevels.Add(RenovatingUrgencyLevel.Level3);
            RenovatingUrgencyLevels.Add(RenovatingUrgencyLevel.Level4);
            RenovatingUrgencyLevels.Add(RenovatingUrgencyLevel.Level5);
            AddSuggestionCommand = new RelayCommand(AddSuggestion_Click);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _renovatingSuggestions;
        public string RenovatingSuggestions
        {
            get => _renovatingSuggestions;
            set
            {
                if (value != _renovatingSuggestions)
                {
                    _renovatingSuggestions = value;
                    OnPropertyChanged();
                }
            }
        }
        private void AddSuggestion_Click(object sender)
        {
            //pozoves servis da se sacuva u fajl preko kontrolera 
            RenovatingRequest renovatingRequest = new RenovatingRequest()
            {
                AccommodationReservation = SelectedAccommodationReservation,
                Level = SelectedRenovatingUrgencyLevel,
                RenovatingSuggestion = RenovatingSuggestions
            };
            _renovatingRequestController.SaveRenovatingRequest(renovatingRequest);
        }
    }
}
