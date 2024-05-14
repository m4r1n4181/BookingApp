using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class AccommodationViewModel : ViewModelBase
    {
        private AccommodationController _accommodationController;

      
        public Accommodation accommodation {  get; set; }
       
        public RelayCommand ViewStatisticsCommand { get; set; }
        public AccommodationViewModel(Accommodation selectedAccommodation)
        {
            _accommodationController = new AccommodationController();
            accommodation = _accommodationController.GetById(selectedAccommodation.Id);
            ViewStatisticsCommand = new RelayCommand(ExecuteViewStatistics, CanExecuteViewStatistics);
        }

        private bool CanExecuteViewStatistics(object param)
        {
            //not implemented yet
            return true;
        }

        private void ExecuteViewStatistics(object param)
        {
            //not implemented yet bice za sims kt3
        }
    }
}
