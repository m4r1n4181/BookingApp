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
    public partial class TourReviewForm : Window
    {
        private TourReviewController _tourReviewController;
        public TourReservation SelectedTourReservation { get; set; }

        public TourReviewForm(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourReviewController = new TourReviewController();
            SelectedTourReservation = tourReservation;
        }
        private int _knowledge;
        public int Knowledge
        {
            get => _knowledge;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency  and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _knowledge)
                {
                    _knowledge = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _fluency;
        public int Fluency
        {
            get => _fluency;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency  and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _fluency)
                {
                    _fluency = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _tourAppeal;
        public int TourAppeal
        {
            get => _tourAppeal;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency  and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _fluency)
                {
                    _fluency = value;
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
            TourReview tourReview = new TourReview()
            {
                TourReservation = SelectedTourReservation,
                Knowledge = Knowledge,
                Fluency = Fluency,
                TourAppeal = TourAppeal,
                Comment = Comment,
            };

            _tourReviewController.RateTour(tourReview);
            MessageBox.Show("Tour successfully rated!");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}