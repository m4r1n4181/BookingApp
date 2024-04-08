using BookingApp.Model;
using BookingApp.Model.Enums;
using BookingApp.Repository;
using BookingApp.View.Owner;
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
                        TourGuideHomePage tourGuideHomePage= new TourGuideHomePage();
                        tourGuideHomePage.Show();
                        /* CreateTourForm createTourForm = new CreateTourForm();
                         createTourForm.Show();
                         LiveTourView liveTourView = new LiveTourView();
                         liveTourView.Show();*/

                    }
                    else if (user.Type == UserType.Owner)
                    {
                        RegisterAccommodationForm registerAccommodationForm = new RegisterAccommodationForm();
                        registerAccommodationForm.Show();
                        AccommodationReservationToRateForm accommodationReservationToRateForm = new AccommodationReservationToRateForm(user);
                        accommodationReservationToRateForm.Show();
                       
                    }

                    else if (user.Type == UserType.Tourist)
                    {

                    }
                    else
                    {
                        AccommodationSearch accommodationSearch = new AccommodationSearch();
                        accommodationSearch.Show();

                        //OwnerReviewForm ownerReviewForm = new OwnerReviewForm(user);
                        //ownerReviewForm.Show();
                        //ovaj deo se odkomentarise kad se doradi bas za tu rezervaciju ocenjivanje trenutno to nije povezano
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
