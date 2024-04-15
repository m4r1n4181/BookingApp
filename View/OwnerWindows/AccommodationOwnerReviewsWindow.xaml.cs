using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReviewsWindow.xaml
    /// </summary>
    public partial class AccommodationOwnerReviewsWindow : Window
    {
        public User User {  get; set; }
        public ObservableCollection<AccommodationOwnerReview> AccommodationOwnerReviews { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public AccommodationOwnerReviewsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            //AccommodationOwnerReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllReviewsTest(User.Id));
        }
        public AccommodationOwnerReviewsWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            User = user; 
            AccommodationOwnerReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllReviewsTest(User.Id));
        }
    }
}
