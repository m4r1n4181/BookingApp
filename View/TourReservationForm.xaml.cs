using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourReservationForm.xaml
    /// </summary>
    public partial class TourReservationForm : Window
    {
        public User LoggedInUser { get; set; }

        public Tour SelectedTour { get; set; }

        public TourParticipants TourParticipants { get; set; }

        private readonly TourReservationRepository _repository;
        private readonly TourParticipantService _service;

        private string _firstName;
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

        private string _lastName;
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

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value != _age)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourReservationForm(User user)
        {
            InitializeComponent();
            Title = "Create reservation";
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourReservationRepository();
        }

        public TourReservationForm(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            Title = "Reserve tour";
            /*txtCommentText.IsEnabled = false;
            btnSave.Visibility = Visibility.Collapsed;*/
            SelectedTour = selectedTour;
            //Text = selectedComment.Text;
            _repository = new TourReservationRepository();
        }


        private void AddParticipant_Button_Click(object sender, RoutedEventArgs e)
        {
            TourParticipants tourParticipants = new TourParticipants
            {
                FirstName = Name,
                LastName = LastName,
                Age = Age,
            };

        }


        /* private void ReserveTour_Click(object sender, RoutedEventArgs e)
         {
             if (SelectedTour != null)
             {
                 // Kreiranje rezervacije ture na osnovu unesenih podataka
                 int participantsNumber = int.Parse(ParticipantsNumberTextBox.Text);

                 // Dodatna logika za kreiranje rezervacije ture
                 TourReservation reservation = new TourReservation()
                 {
                     GuestsNumber = participantsNumber,
                     Tour = SelectedTour,
                     Tourists = new List<TourParticipants>()
                 };

                 // Dodavanje rezervacije ture u bazu podataka
                 _repository.Save(reservation);

                 // Dodatna logika, na primjer, ažuriranje korisničkog interfejsa ili provjera
             }
             else
             {
                 // Dodatna logika ako tura nije odabrana
             }
         }*/


        /*  private void Cancel(object sender, RoutedEventArgs e)
          {
              Close();
          }*/
    }
}

