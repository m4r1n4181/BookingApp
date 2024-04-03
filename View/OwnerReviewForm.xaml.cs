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
    /// Interaction logic for OwnerReviewForm.xaml
    /// </summary>
    public partial class OwnerReviewForm : Window
    {
        private OwnerReviewController _ownerReviewController;
        public AccommodationReservation SelectedAccommodationReservation;
        public OwnerReviewForm(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            _ownerReviewController = new OwnerReviewController();
            SelectedAccommodationReservation = accommodationReservation;
        }

        private int _cleanliness;
        public int Clean
        {
            get => _cleanliness;
            set
            {
                if(value < 1 || value > 5)
                {
                    MessageBox.Show("Cleanliness must be number between 1 and 5.");
                    return;
                }
                if(value!= _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rule;
        public int Rule
        {
            get => _rule;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Rule adherence must be number between 1 and 5.");
                    return;
                }
                if (value != _rule)
                {
                    _rule = value;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OwnerReview ownerReview = new OwnerReview()
            {
                Reservation = SelectedAccommodationReservation,
                Cleanliness = Clean,
                RuleAdherence = Rule,
                Comment = Comment,
            };

            _ownerReviewController.RateOwner(ownerReview);
            MessageBox.Show("Owner and accommodation successfully rated!");
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
