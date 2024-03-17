using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestReview.xaml
    /// </summary>
    public partial class GuestReview : Window
    {
        private GuestReviewController _guestReviewController;
        public GuestReview()
        {
            InitializeComponent();
            _guestReviewController = new GuestReviewController();
        }


        private void Submit(object sender, RoutedEventArgs e)
        {
            //morala sam naglasiti da je iz modela jer i ovde imam GuestReview
            Model.GuestReview guestReview = new Model.GuestReview(
                int.Parse(Cleanliness.Text),
                int.Parse(RuleAdherence.Text),
                Comment.Text
            );

            _guestReviewController.RateGuest(guestReview);
            MessageBox.Show("Guest successfully rated!");
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
