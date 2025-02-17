﻿using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View;
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
    public class ActiveToursViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public RelayCommand ViewCommand { get; set; }


        public ActiveToursViewModel()
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllActiveTours());
            ViewCommand = new RelayCommand(View_Click, CanExecuteViewClick);

        }

        public void View_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourDetails tourDetails = new TourDetails(SelectedTour);
            tourDetails.ShowDialog();


        }

        public bool CanExecuteViewClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }
    }
}
