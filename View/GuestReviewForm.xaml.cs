using BookingApp.Controller;
using BookingApp.Model;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestReviewForm.xaml
    /// </summary>
    public partial class GuestReviewForm : Window
    {
        private GuestReviewController _guestReviewController;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        public GuestReviewForm(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            _guestReviewController = new GuestReviewController();
            SelectedAccommodationReservation = accommodationReservation;
        }
        private int _cleanliness;
        public int Clean
        {
            get => _cleanliness;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Cleanliness and Rule Adherence must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rules;
        public int Rule
        {
            get => _rules;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Cleanliness and Rule Adherence must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _rules)
                {
                    _rules = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            GuestReview guestReview = new GuestReview()
            {
                AccommodationReservation = SelectedAccommodationReservation,
                Cleanliness = Clean,
                RuleAdherence = Rule,
                Comment = Comment,
            };

            _guestReviewController.RateGuest(guestReview);
            MessageBox.Show("Guest successfully rated!");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

