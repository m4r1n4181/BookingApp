using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Security.Principal;

namespace BookingApp.View
{
    public partial class TourReservationForm : Window, INotifyPropertyChanged
    {
        private TourReservationService _tourReservationService;
        private TourParticipantService _tourParticipantService;
        private TourRepository _tourRepository;
        private TouristRepository _touristRepository;
        private UserRepository _userRepository;
        private User User { get; set; }

        public Tour SelectedTour { get; set; }
        public ObservableCollection<TourParticipants> Participants { get; set; }

        private string _addedParticipant;
        public string AddedParticipant
        {
            get => _addedParticipant;
            set
            {
                if (value != _addedParticipant)
                {
                    _addedParticipant = value;
                    OnPropertyChanged("AddedParticipant");
                }
            }
        }

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

        public TourReservationForm()
        {
            InitializeComponent();
            DataContext = this;
            _tourReservationService = new TourReservationService();
            _tourParticipantService = new TourParticipantService();
            Participants = new ObservableCollection<TourParticipants>();
            _tourRepository = new TourRepository();
            _touristRepository = new TouristRepository();
            _userRepository = new UserRepository();
        }

        public TourReservationForm(Tour selectedTour, User user) : this()
        {
            SelectedTour = selectedTour;
            User = user;
        }


        private void UseMyInfo_Button_Click(object sender, RoutedEventArgs e)
        {
            Tourist tourist1 = _touristRepository.GetByUserId(User.Id);
            TourParticipants tourist = new TourParticipants
            {
                Id = tourist1.Id,
                FirstName = tourist1.FirstName,
                LastName = tourist1.LastName,
                Age = Convert.ToInt32(tourist1.Age)
            };

            Participants.Add(tourist);

            // Ažuriraj prikaz dodatih učesnika
            UpdateAddedParticipants();

            // Spremi učesnika u CSV datoteku
            _tourParticipantService.CreateParticipant(tourist);
        }


        private void AddParticipant_Button_Click(object sender, RoutedEventArgs e)
        {
            int numberOfParticipants = Convert.ToInt32(ParticipantsNumberTextBox.Text);

            if (Participants.Count >= numberOfParticipants)
            {
                MessageBox.Show($"You have already entered {numberOfParticipants} participants.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TourParticipants participant = new TourParticipants
            {
                FirstName = ParticipantNameTextBox.Text,
                LastName = ParticipantLastNameTextBox.Text,
                Age = Convert.ToInt32(ParticipantAgeTextBox.Text)
            };

            // Dodaj učesnika u listu
            Participants.Add(participant);

            // Ažuriraj prikaz dodatih učesnika
            UpdateAddedParticipants();

            // Spremi učesnika u CSV datoteku
            _tourParticipantService.CreateParticipant(participant);

            // Očisti tekstualna polja nakon dodavanja učesnika
            ParticipantNameTextBox.Text = "";
            ParticipantLastNameTextBox.Text = "";
            ParticipantAgeTextBox.Text = "";
        }



        private void UpdateAddedParticipants()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var participant in Participants)
            {
                sb.Append($"{participant.FirstName} {participant.LastName} ({participant.Age}), ");
            }
            // Ukloni poslednji zarez i prazan prostor
            string addedParticipants = sb.ToString().TrimEnd(' ', ',');
            AddedParticipant = addedParticipants;
        }



        private void CheckAvailability_Button_Click(object sender, RoutedEventArgs e)
        {
            int participantsNumber = Convert.ToInt32(ParticipantsNumberTextBox.Text);

            if (SelectedTour.AvailableSeats == 0)
            {
                MessageBoxResult result = MessageBox.Show("Tour has no available seats. Do you want to check alternative tours?", "Availability Check", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Pozivamo metod za prikaz alternativnih tura
                    ShowAlternativeTours();
                }
                else
                {
                    return;
                }
            }
            else if(participantsNumber > SelectedTour.AvailableSeats)
            {
                MessageBoxResult result = MessageBox.Show($"The selected tour has {SelectedTour.AvailableSeats} seats left. Do you want to edit the number of participants?", "Availability Check", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Pozivamo metod za prikaz alternativnih tura
                    return;
                }
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Tour has enough seats, you can continue!");
            }
        }

        private void ShowAlternativeTours()
        {
            // Dobijemo lokaciju odabrane ture
            Location selectedLocation = SelectedTour.Location;

            // var alternativeTours = _tourRepository.GetAlternativeTours(selectedLocation);

            AlternativeToursView alternativeToursView = new AlternativeToursView(SelectedTour);

            alternativeToursView.ShowDialog();
        }



        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            int participantsNumber = Participants.Count;

            TourReservation reservation = new TourReservation()
            {
                GuestsNumber = participantsNumber,
                UserId = User.Id, // Dodajemo UserId korisnika koji je napravio rezervaciju
                Tour = SelectedTour,
                Tourists = Participants.ToList<TourParticipants>(),
                TouristEntry = new TouristEntry(id),
                Tourist = _touristRepository.GetByUserId(User.Id)

            };
            _tourReservationService.Create(reservation);

            // Ažuriraj broj dostupnih mjesta za turu
            UpdateAvailableSeats(SelectedTour.Id, participantsNumber);

            MessageBox.Show("Tour reserved with participants: " + Participants.Count);
            // Clear participants list after reservation
            Participants.Clear();
        }


        private void UpdateAvailableSeats(int tourId, int reservedSeats)
        {
            // Učitaj sve ture iz CSV datoteke
            List<Tour> tours = _tourRepository.GetAll();

            // Pronađi odgovarajuću turu koja se ažurira
            Tour tourToUpdate = tours.Find(t => t.Id == tourId);

            if (tourToUpdate != null)
            {
                // Ažuriraj broj dostupnih mjesta
                tourToUpdate.AvailableSeats -= reservedSeats;

                // Ažuriraj turu u CSV datoteci
                _tourRepository.Update(tourToUpdate);
            }
        }

        private void Voucher_Click(object sender, RoutedEventArgs e)
        {
            UsableVouchers usableVouchers = new UsableVouchers();
            usableVouchers.Show();

        }


    }
}