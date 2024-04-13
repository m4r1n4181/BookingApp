using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for ActiveTours.xaml
    /// </summary>
    public partial class ActiveTours : Window //trebaju mi samo active tours 
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public ActiveTours()
        {
            InitializeComponent();
            DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllActiveTours());
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourDetails tourDetails = new TourDetails(SelectedTour);
            tourDetails.ShowDialog();


        }
    }
}
