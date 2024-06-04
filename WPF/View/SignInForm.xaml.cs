using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.WPF.View.Tourist;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.OwnerPages;
using BookingApp.WPF.View.OwnerWindows;
using BookingApp.View.OwnerWindows;
using BookingApp.WPF.Views.GuestWindows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly IUserRepository _repository;
        private NotificationController _notificationController;

        public static User LoggedUser { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = Injector.CreateInstance<IUserRepository>();
            _notificationController = new NotificationController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
               
                if (user.Password == txtPassword.Password)
                {
                    LoggedUser = user;
                    
                    if (user.Type == UserType.TourGuide)
                    {
                        TourGuideHomePage tourGuideHomePage= new TourGuideHomePage();
                        tourGuideHomePage.Show();

                    }
                    else if (user.Type == UserType.Owner)
                    {
                       // OwnerUIWindow ownerUIWindow = new OwnerUIWindow();
                        //ownerUIWindow.Show();
                     // AllAccommodationsWindow allAccommodationsWindow = new AllAccommodationsWindow();
                       // allAccommodationsWindow.Show(); 
                      // OwnerMainWindow ownerMainWindow = new OwnerMainWindow();
                       // ownerMainWindow.Show();
                       // SortedAccommodations sortedAccommodations = new SortedAccommodations();
                        //sortedAccommodations.Show();
                       // LocationRecommendation locationRecommendation = new LocationRecommendation();
                        //locationRecommendation.Show();
                        AllForums allForums= new AllForums();
                        allForums.Show();
                        LocationRecommendation locationRecommendation = new LocationRecommendation();
                        locationRecommendation.Show();

                    }

                    else if (user.Type == UserType.Tourist)
                    {
                        TouristMainWindow touristMainWindow = new TouristMainWindow(LoggedUser);
                        touristMainWindow.Show();

                        List<Notification> notifications = _notificationController.GetByUserId(LoggedUser.Id);
                        foreach(Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Message, "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        _notificationController.ReadAllUserNotifications(LoggedUser.Id);

                      
                    }
                    else if(user.Type == UserType.Guest)
                    {
                        GuestHomePage guestHomePage = new GuestHomePage();
                        guestHomePage.Show();

                        List<Notification> notifications = _notificationController.GetByUserId(LoggedUser.Id);
                        foreach (Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        _notificationController.ReadAllUserNotifications(LoggedUser.Id);
                        Close();


                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }
    }
}