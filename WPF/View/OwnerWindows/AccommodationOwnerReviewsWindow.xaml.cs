using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
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

namespace BookingApp.WPF.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReviewsWindow.xaml
    /// </summary>
    public partial class AccommodationOwnerReviewsWindow : Window
    {
        public ObservableCollection<AccommodationOwnerReview> AccommodationOwnerReviews { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;
        public AccommodationOwnerReviewsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            AccommodationOwnerReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllValidReviewsForOwner(SignInForm.LoggedUser.Id));
        }
    
    }
}
