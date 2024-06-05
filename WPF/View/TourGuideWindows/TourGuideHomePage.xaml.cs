using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
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

namespace BookingApp.WPF.View.TourGuideWindows
{
    /// <summary>
    /// Interaction logic for TourGuideHomePage.xaml
    /// </summary>
    public partial class TourGuideHomePage : Window, INotifyPropertyChanged
    {

        private Visibility _superGuideVisibility;

        public Visibility SuperGuideVisibility
        {
            get => _superGuideVisibility;
            set
            {
                _superGuideVisibility = value;
                OnPropertyChanged(nameof(SuperGuideVisibility));
            }
        }

        public bool IsSuperGuide
        {
            get => _isSuperGuide;
            set
            {
                _isSuperGuide = value;
                OnPropertyChanged(nameof(IsSuperGuide));
                SuperGuideVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private bool _isSuperGuide;

        public string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Dictionary<string, bool> SuperGuideStatusPerLanguage { get; private set; }

        public TourController _tourController;
        public ObservableCollection<Tour> TodayTours { get; set; }
        public ObservableCollection<Tour> TodayToursThreeByThree { get; set; }
        public ObservableCollection<Tour> ThisWeeksMonday { get; set; }
        public ObservableCollection<Tour> ThisWeeksTuesday { get; set; }
        public ObservableCollection<Tour> ThisWeeksWednesday { get; set; }
        public ObservableCollection<Tour> ThisWeeksThursday { get; set; }
        public ObservableCollection<Tour> ThisWeeksFriday { get; set; }

        private int atIndex;
        public TourGuideHomePage()
        {
            InitializeComponent();
            this.DataContext = this;

            atIndex = 0;
            _tourController = new TourController();

            TodayTours = new ObservableCollection<Tour>(_tourController.GetTodayTours());

            List<Tour> first3 = TodayTours.Skip(atIndex).Take(3).ToList();

            TodayToursThreeByThree = new ObservableCollection<Tour>(first3);

            ThisWeeksMonday = new ObservableCollection<Tour>(_tourController.GetThisWeeksMondayTours());
            ThisWeeksTuesday = new ObservableCollection<Tour>(_tourController.GetThisWeeksTuesdayTours());
            ThisWeeksWednesday = new ObservableCollection<Tour>(_tourController.GetThisWeeksWednesdayTours());
            ThisWeeksThursday = new ObservableCollection<Tour>(_tourController.GetThisWeeksThursdayTours());
            ThisWeeksFriday = new ObservableCollection<Tour>(_tourController.GetThisWeeksFridayTours());

            TourGuideService tourGuideService = new TourGuideService();
            BookingApp.Model.TourGuide currentGuide = tourGuideService.GetById(SignInForm.LoggedUser.Id);
            if (currentGuide != null)
            {
                FirstName = currentGuide.FirstName;
                LastName = currentGuide.LastName;
            }

            SuperGuideStatusPerLanguage = new Dictionary<string, bool>();


            var languageToCheck = "Engleski";
            int guideId = SignInForm.LoggedUser.Id;

            bool isSuperGuide = _tourController.CheckAndAssignSuperGuide(guideId, languageToCheck);
            SuperGuideStatusPerLanguage[languageToCheck] = isSuperGuide;

            IsSuperGuide = isSuperGuide;
        }

        private void CreateNewTour_Click(object sender, RoutedEventArgs e)
        {
            CreateTourForm createTourForm = new CreateTourForm();
            createTourForm.Show();
        }

        private void LiveTour_Click(object sender, RoutedEventArgs e)
        {
            LiveTourView liveTourView = new LiveTourView();
            liveTourView.Show();
        }

        private void AllFutureTours_Click(object sender, RoutedEventArgs e)
        {
            TourReservation tourReservation = new TourReservation();

            FutureTours futureTours = new FutureTours();
            futureTours.Show();
        }

        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {

            TourStatisticsOverview tourStatisticsOverview = new TourStatisticsOverview();
            tourStatisticsOverview.Show();
        }

        private void TourGuideReviews_Click(object sender, RoutedEventArgs e)
        {

            TourGuideReviews tourGuideReviews = new TourGuideReviews();
            tourGuideReviews.Show();
        }
        private void AllTours_Click(object sender, RoutedEventArgs e)
        {

            AllTours allTours = new AllTours();
            allTours.Show();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            TourGuideHomePage tourGuideHomePage = new TourGuideHomePage();
            tourGuideHomePage.Show();
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SearchTourRequests searchTourRequests = new SearchTourRequests();
            searchTourRequests.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
           GuideProfile guideProfile = new GuideProfile();
           guideProfile.Show();
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            //otvara prozor za tutorial 
        }

        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            if (atIndex - 3 >= TodayTours.Count)
            {
                return;
            }
            atIndex -= 3;

            List<Tour> other3 = TodayTours.Skip(atIndex).Take(3).ToList();

            RefreshTours(other3);

        }

        private void rigthButton_Click(object sender, RoutedEventArgs e)
        {
            if(atIndex + 3 >= TodayTours.Count)
            {
                return;
            }
            atIndex += 3;

            List<Tour> other3 = TodayTours.Skip(atIndex).Take(3).ToList();

            RefreshTours(other3);
        }

        private void RefreshTours(List<Tour> tours)
        {
            TodayToursThreeByThree.Clear();
            tours.ForEach(t => TodayToursThreeByThree.Add(t));
        }
    }
}
