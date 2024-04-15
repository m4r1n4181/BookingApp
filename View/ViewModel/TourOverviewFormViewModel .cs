using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.DTO;

namespace BookingApp.View.ViewModel
{
    public class TourOverviewFormViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public User User { get; set; }
        private readonly TourController _tourController;

        public Tour SelectedTour { get; set; }

        // Constructor
        public TourOverviewFormViewModel(User user)
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());
            User = user;
        }

        // Method to handle searching for tours
        public void SearchTours(string city, string country, string language, int duration, int maxTourists)
        {
            TourSearchParams searchParams = new TourSearchParams
            {
                City = city,
                Country = country,
                Language = language,
                Duration = duration,
                MaxTourists = maxTourists
            };

            Tours.Clear();
            foreach (Tour tour in _tourController.SearchTours(searchParams))
            {
                Tours.Add(tour);
            }
        }

      /*  public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
