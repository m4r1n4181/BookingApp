using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TourDetails : Window, INotifyPropertyChanged
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
                    OnPropertyChanged();
                }
            }
        }

        public string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _language;
        public string Languages
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _keyPoint;
        public string KeyPoint
        {
            get => _keyPoint;
            set
            {
                if (value != _keyPoint)
                {
                    _keyPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxTourists;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists)
                {
                    _maxTourists = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourDate;
        public string TourDate
        {
            get => _tourDate;
            set
            {
                if (value != _tourDate)
                {
                    _tourDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tour SelectedTour { get; set; }
        private KeyPointController _keyPointController;
   
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }

        private TourController _tourController;
        public TourDetails(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = tour;

            TourName = tour.Name;
            Location = $"{tour.Location.City}, {tour.Location.Country}";
            Description = tour.Description;
            Languages = tour.Language;
            MaxTourists = tour.MaxTourists;
            Duration = tour.Duration;
            TourDate = string.Join(", ", tour.StartDates);


            _keyPointController = new KeyPointController();
            _tourController = new TourController();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetAllForTour(tour.Id));




        }

        public void ActivateKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint == null)
            {
                MessageBox.Show("Please select a keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            _keyPointController.ActivateKeyPoint(SelectedKeyPoint.Id);
            KeyPoints.Clear();
            foreach (KeyPoint keyPoint in _keyPointController.GetAllForTour(SelectedTour.Id))
            {
                KeyPoints.Add(keyPoint);
            }
        }

        public void MarkTourist_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedKeyPoint == null)
            {
                MessageBox.Show("Please select a keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedKeyPoint.IsActive == false)
            {
                MessageBox.Show("Please select active keyPoint.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TouristSelectionForm touristSelectionForm = new TouristSelectionForm(SelectedKeyPoint);
            touristSelectionForm.ShowDialog();

        }

        public void EndTour_Click(object sender, RoutedEventArgs e)
        {
            //tourId
            _tourController.EndTour(SelectedTour.Id);
            Close();

        }
        

    }

   }

        //za dodavanje turiste
        // selectujem keypoint i click dugme nadji tutistu
        //novi prozor sa svim turistama gde selektujemo jednog
        // u taj novi prozor posalji SelectedKeyPoint
        //tamo sklopis objecat ToursitEntry
        // i samo ga creiras u controlleru

