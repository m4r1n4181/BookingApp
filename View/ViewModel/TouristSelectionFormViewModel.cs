using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.ViewModel
{
    public class TouristSelectionFormViewModel
    {
        private readonly TouristController _touristController;
        private TouristEntryController _touristEntryController;
        public ObservableCollection<Tourist> Tourists { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }

        public TouristSelectionFormViewModel(KeyPoint selectedKeyPoint)
        {
           
            SelectedKeyPoint = selectedKeyPoint;

            _touristController = new TouristController();
            _touristEntryController = new TouristEntryController();
            Tourists = new ObservableCollection<Tourist>(_touristController.GetAllNotOnTour(selectedKeyPoint.Tour.Id));

        }


        private void AddTouristEntry_Click(object sender, RoutedEventArgs e)
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
            
        }
    }
}
