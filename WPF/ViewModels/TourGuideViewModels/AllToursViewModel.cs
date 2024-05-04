using BookingApp.Controller;
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

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class AllToursViewModel : INotifyPropertyChanged
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

        public RelayCommand ActiveCommand { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand TourStatisticsCommand { get; set; }
        public RelayCommand TutorialCommand { get; set; }

        public AllToursViewModel()
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllWithLocations());
            ViewCommand = new RelayCommand(View_Click, CanExecuteViewClick);
            ActiveCommand = new RelayCommand(ActiveTours_Click, CanExecuteActiveToursClick);
            AddCommand = new RelayCommand(AddTours_Click, CanExecuteAddToursClick);
            TourStatisticsCommand = new RelayCommand(TourStatistics_Click, CanExecuteTourStatisticsClick);
            TutorialCommand = new RelayCommand(Tutorial_Click, CanExecuteTutorialClick);

        }

        public void View_Click(object param)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourView tourView = new TourView(SelectedTour);
            tourView.ShowDialog();


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

        public void ActiveTours_Click(object param)
        {

            ActiveTours activeTours = new ActiveTours();
            activeTours.Show();


        }
        public bool CanExecuteActiveToursClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void AddTours_Click(object param)
        {

            CreateTourForm createTourForm = new CreateTourForm();
            createTourForm.ShowDialog();


        }
        public bool CanExecuteAddToursClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void TourStatistics_Click(object param)
        {
            TourStatisticsOverview tourStatisticsOverview = new TourStatisticsOverview();
            tourStatisticsOverview.Show();
            //napravi tako da mozes da zatvoris prethodniii obavezno za sve kao da te baci na nes drugo 


        }
        public bool CanExecuteTourStatisticsClick(object param)
        {
            return true;
            // if (SelectedTour == null)
            //{
            // MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //  return false;
            //}
            //return true;
        }

        public void Tutorial_Click(object param)
        {
           
         //prozor za tutorial 

        }
        public bool CanExecuteTutorialClick(object param)
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
