using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerPageViewModels
{
    public class AccommodationViewViewModel
    {
        public AccommodationController _accommodationController { get; set; }
        public Accommodation accommodation { get; set; }
        public AccommodationViewViewModel(Accommodation selectedAccommodation)
        {
            _accommodationController = new AccommodationController();
            accommodation = _accommodationController.GetById(selectedAccommodation.Id);
        }
    }
}
