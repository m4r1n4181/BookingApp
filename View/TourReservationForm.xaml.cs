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

namespace BookingApp.View
{
    public partial class TourReservationForm : Window, INotifyPropertyChanged
    {
        private TourReservationService _tourReservationService;
        private TourParticipantService _tourParticipantService;
        private TourRepository _tourRepository;

        public Tour SelectedTour {  get; set; }
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
        }

        public TourReservationForm(Tour selectedTour) : this()
        {
            SelectedTour = selectedTour;
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

            if (participantsNumber > SelectedTour.AvailableSeats)
            {
                MessageBoxResult result = MessageBox.Show("The number of participants exceeds the available seats. Do you want to check alternative tours?", "Availability Check", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Otvaranje AlternativeToursWindow prozora
                   // AlternativeToursView alternativeToursWindow = new AlternativeToursView(SelectedTour.Location); // Prosleđujemo lokaciju ture
                   // alternativeToursWindow.Show();
                }
                else
                {
                    // Korisnik ne želi da proveri alternativne ture
                }
            }
            else
            {
                MessageBox.Show("The selected tour has enough available seats. You can proceed with the reservation.", "Availability Check", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }




        private void ShowAlternativeTours()
        {
            // Dobijemo lokaciju odabrane ture
            Location selectedLocation = SelectedTour.Location;

            // Dobijemo alternative sa istom lokacijom kao odabrana tura
            var alternativeTours = _tourRepository.GetAlternativesByLocation(selectedLocation);

            // Kreiramo prozor za prikaz alternativnih tura i prosleđujemo listu tura
           // AlternativeToursView alternativeToursWindow = new AlternativeToursView(alternativeTours);

            // Prikažemo prozor
            //alternativeToursWindow.ShowDialog();
        }


        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            int participantsNumber = Participants.Count;
            TourReservation reservation = new TourReservation()
            {
                GuestsNumber = participantsNumber,
                Tour = SelectedTour,
                Tourists = Participants.ToList<TourParticipants>()
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


    }
}
