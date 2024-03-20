
using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

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

        private Tour SelectedTour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
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
                        /*
                        CreateTourForm createTourForm = new CreateTourForm();
                        createTourForm.Show();
                        */
                    }
                    else if (user.Type == UserType.Owner)
                    {
                       /* RegisterAccommodationForm registerAccommodationForm = new RegisterAccommodationForm();
                        registerAccommodationForm.Show();
                        AccommodationReservationToRateForm accommodationReservationToRateForm = new AccommodationReservationToRateForm(user);
                        accommodationReservationToRateForm.Show();
                       */
                    }

                    else if (user.Type == UserType.Tourist)
                    {
                        TourOverviewForm tourOverviewForm = new TourOverviewForm();
                        tourOverviewForm.Show();

                        /*TourReservationForm tourReservationForm = new TourReservationForm(SelectedTour);
                        tourReservationForm.Show();*/
                    }
                    else
                    {

                    }
                    CommentsOverview commentsOverview = new CommentsOverview(user);
                    commentsOverview.Show();
                    Close();

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
