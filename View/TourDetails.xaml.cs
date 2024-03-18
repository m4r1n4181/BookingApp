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
    /// Interaction logic for TourDetails.xaml
    /// </summary>
    public partial class TourDetails : Window
    {

        public string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    //OnPropertyChanged();
                }
            }
        }


        public Tour SelectedTour { get; set; }
        private KeyPointController _keyPointController;
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public TourDetails(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = tour;

            TourName = tour.Name;

            _keyPointController = new KeyPointController();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetAllForTour(tour.Id));

        }

        public void dugmeClick()
        {
            if(SelectedKeyPoint == null)
            {
                //messageboc
                return;
            }

            _keyPointController.ActivateKeyPoint(SelectedKeyPoint.Id);

            //refresh KeyPoints
            KeyPoints.Clear();
            foreach(KeyPoint keyPoint in _keyPointController.GetAllForTour(SelectedTour.Id))
            {
                KeyPoints.Add(keyPoint);
            }




        }

        //za dodavanje turiste
        // selectujem keypoint i click dugme nadji tutistu
        //novi prozor sa svim turistama gde selektujemo jednog
        // u taj novi prozor posalji SelectedKeyPoint
        //tamo sklopis objecat ToursitEntry
        // i samo ga creiras u controlleru
    }
}
