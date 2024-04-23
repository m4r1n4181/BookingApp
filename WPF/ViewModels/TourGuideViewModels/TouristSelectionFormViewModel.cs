using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.ViewModels.TourGuideViewModels
{
    public class TouristSelectionFormViewModel
    {
        private readonly TouristController _touristController;
        private TouristEntryController _touristEntryController;
        public ObservableCollection<Tourist> Tourists { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }
        public RelayCommand AddTouristEntryCommand { get; set; }



        public TouristSelectionFormViewModel(KeyPoint selectedKeyPoint)
        {
            SelectedKeyPoint = selectedKeyPoint;

            _touristController = new TouristController();
            _touristEntryController = new TouristEntryController();
            Tourists = new ObservableCollection<Tourist>(_touristController.GetAllNotOnTour(selectedKeyPoint.Tour.Id));
            AddTouristEntryCommand = new RelayCommand(AddTouristEntry_Click, CanExecuteAddTouristEntryClick);


        }


        public void AddTouristEntry_Click(object param)
        {
            if (SelectedTourist == null)
            {
                MessageBox.Show("Please select a tourist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TouristEntry touristEntry = new TouristEntry
            {
                KeyPoint = SelectedKeyPoint,
                Tourist = SelectedTourist,

            };

            _touristEntryController.AddTouristEntry(touristEntry);

            MessageBox.Show("Tourist entry added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
           // Close();
        }

        public bool CanExecuteAddTouristEntryClick(object param)
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

