using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for GuideProfile.xaml
    /// </summary>
    public partial class GuideProfile : Window
    {
        private TourGuideController _tourGuideController;

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


        public GuideProfile()
        {
            InitializeComponent();
            DataContext = this;

            _tourGuideController = new TourGuideController();
            _superGuideVisibility = Visibility.Visible;
            bool isSuperGuide = _tourGuideController.IsSuperGuide(SignInForm.LoggedUser.Id);

            if (isSuperGuide)
            {
                _superGuideVisibility = Visibility.Visible;
            }
            else
            {
                _superGuideVisibility = Visibility.Hidden;
            }

            TourGuideService tourGuideService = new TourGuideService();
            BookingApp.Model.TourGuide currentGuide = tourGuideService.GetById(SignInForm.LoggedUser.Id); 
            if (currentGuide != null)
            {
                FirstName = currentGuide.FirstName;
                LastName = currentGuide.LastName;
                SuperGuideVisibility = currentGuide.IsSuperGuide ? Visibility.Visible : Visibility.Collapsed;
            }

        }

       
        private void Quit_Job_Click(object sender, RoutedEventArgs e)
        {
            int guideId = SignInForm.LoggedUser.Id;
            _tourGuideController.Resignation(guideId);
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

        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SearchTourRequests searchTourRequests = new SearchTourRequests();
            searchTourRequests.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            //za sad me samo baca na signin, posle neka logika 
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
    }
}
