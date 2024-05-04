using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class AccommodationRenovationsOverviewViewModelPage : ViewModelBase
    {
        private AccommodationRenovationController _controller;
        public string Type { get; set; }
        //public AccommodationRenovation SelectedAccommodationRenovation { get; set; }
        private ObservableCollection<AccommodationRenovation> _accommodationRenovations;
        public ObservableCollection<AccommodationRenovation> AccommodationRenovations
        {
            get { return _accommodationRenovations; }
            set
            {
                _accommodationRenovations = value;
                OnPropertyChanged();
            }
        }
        private AccommodationRenovation _accommodationRenovation;
        public AccommodationRenovation SelectedAccommodationRenovation
        {
            get { return _accommodationRenovation; }

            set
            {
                _accommodationRenovation = value;
                OnPropertyChanged();
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (_selectedType != value)
                {                 
                    _selectedType = value;
                    OnPropertyChanged();
                    RefreshData();
                }
            }
        }




        public ICommand LoadScheduledRenovationsCommand { get; private set; }
        public ICommand LoadPreviousRenovationsCommand { get; private set; }
        // public RelayCommand CancelRenovationCommand { get; set; }
        public ICommand CancelRenovationCommand { get; private set; }


        public AccommodationRenovationsOverviewViewModelPage()
        {
            _controller = new AccommodationRenovationController();
            LoadScheduledRenovationsCommand = new RelayCommand(LoadScheduledRenovations);
            LoadPreviousRenovationsCommand = new RelayCommand(LoadPreviousRenovations);
            CancelRenovationCommand = new RelayCommand(CancelRenovation, CanCancelRenovation);
            Type = SelectedType;
            LoadAll();
            //SelectedType = "Scheduled Renovations"; // Set a default type
            RefreshData();
        }




        public void RefreshData()
        {
            if (SelectedType == "Scheduled Renovations")
                LoadScheduledRenovations(null);
            else if (SelectedType == "Previous Renovations")
                LoadPreviousRenovations(null);
        }

        private void LoadAll()
        {
            AccommodationRenovations = new ObservableCollection<AccommodationRenovation>(_controller.GetAllForOwner(SignInForm.LoggedUser.Id));
            RefreshData();
        }


        private void LoadScheduledRenovations(object param)
        {
            AccommodationRenovations = new ObservableCollection<AccommodationRenovation>(_controller.GetAllScheduledRenovations(SignInForm.LoggedUser.Id));
        }

        private void LoadPreviousRenovations(object param)
        {
            AccommodationRenovations = new ObservableCollection<AccommodationRenovation>(_controller.GetAllPreviousRenovations(SignInForm.LoggedUser.Id));
        }

        private void CancelRenovation(object param)
        {
             
             
                 _controller.CancelRenovation(SelectedAccommodationRenovation);
                 RefreshData();
             
                
             
           // var controller = new AccommodationRenovationController();
            //controller.CancelRenovation(SelectedAccommodationRenovation);
        }


        private bool CanCancelRenovation(object param)
        {            
            return SelectedAccommodationRenovation != null;
        }
    }
}
