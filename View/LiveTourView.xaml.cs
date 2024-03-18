using BookingApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View
{
    public partial class LiveTourView : Window
    {
        public ObservableCollection<Tour> Tours { get; set; } // Prikaz liste tura

        public ICommand ActivateTourCommand { get; private set; } // ICommand za aktiviranje ture

        public LiveTourView()
        {
            InitializeComponent();
            DataContext = this;

            Tours = new ObservableCollection<Tour>(); // Inicijalizacija liste tura

           
           // Tours.Add(new Tour { Name = "Tour 1", StartDate = DateTime.Now });
           // Tours.Add(new Tour { Name = "Tour 2", StartDate = DateTime.Now });
           // Tours.Add(new Tour { Name = "Tour 3", StartDate = DateTime.Now });

            //ActivateTourCommand = new RelayCommand(ActivateTour); // Inicijalizacija ICommand-a
        }

        private void ActivateTour(object tour)
        {
            // Otvori novi prozor za aktiviranu turu sa ključnim tačkama
            Tour activatedTour = tour as Tour;
            if (activatedTour != null)
            {
                //TourKeyPointsView keyPointsView = new TourKeyPointsView(activatedTour);
                //keyPointsView.ShowDialog();
            }
        }
    }
}

