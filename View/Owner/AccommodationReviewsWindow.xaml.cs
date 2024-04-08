using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationReviewsWindow.xaml
    /// </summary>
    public partial class AccommodationReviewsWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationOwnerReview> AccommodationReviews { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AccommodationReviewsWindow(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();

            AccommodationReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllValidReviews(accommodation));

        }
    }
}
