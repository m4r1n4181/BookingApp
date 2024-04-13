using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;


namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AllTours.xaml
    /// </summary>
    public partial class AllTours : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }

        private TourController _tourController;

        public AllTours()
        {
            InitializeComponent();
            DataContext = this;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAllWithLocations());
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            TourView tourView = new TourView(SelectedTour);
            tourView.ShowDialog();


        }

        private void ActiveTours_Click(object sender, RoutedEventArgs e)
        {

            ActiveTours activeTours = new ActiveTours();
            activeTours.Show();


        }
    }
}
