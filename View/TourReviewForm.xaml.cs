using BookingApp.Controller;
using BookingApp.Model;
using Microsoft.Win32;
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
    public partial class TourReviewForm : Window
    {
        private TourReviewController _tourReviewController;
        public TourReservation SelectedTourReservation { get; set; }
        public List<string> Pictures { get; set; }

        private int _knowledge;
        public int Knowledge
        {
            get => _knowledge;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("Knowledge, Fluency and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("Knowledge, Fluency and Tour Appeal must be between 1 and 5.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (value != _tourAppeal)
                {
                    _tourAppeal = value;
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

        public TourReviewForm(TourReservation tourReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourReviewController = new TourReviewController();
            SelectedTourReservation = tourReservation;
            Pictures = new List<string>();
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            TourReview tourReview = new TourReview()
            {
                TourReservation = SelectedTourReservation,
                Knowledge = Knowledge,
                Fluency = Fluency,
                TourAppeal = TourAppeal,
                Comment = Comment,
                Pictures = Pictures,
            };

            _tourReviewController.RateTour(tourReview);
            MessageBox.Show("Tour successfully rated!");
            this.Close();
        }

        private void AddImages_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images";
            openFileDialog1.Filter = "All Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                string imagePath = openFileDialog1.FileName;
                string imageFileName = System.IO.Path.GetFileName(imagePath);
                string imageDestinationPath = "../Resources/Images/" + imageFileName;

                Pictures.Add(imageDestinationPath);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
