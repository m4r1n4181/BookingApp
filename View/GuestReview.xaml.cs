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

        /*   private int _cleanliness;
           public int Clean
           {
               get => _cleanliness;
               set
               {
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
                   if (value != _rules)
                   {
                       _rules = value;
                       OnPropertyChanged();
                   }
               }
           } 

     public GuestReview()
           {
               InitializeComponent();
               this.DataContext = this;
               _guestReviewController = new GuestReviewController();
           }
         */
        private void Submit(object sender, RoutedEventArgs e)
        {
            //morala sam naglasiti da je iz modela jer i ovde imam GuestReview
            // dodati uslov za manje od 1 i vece od 5 da baci error 
            Model.GuestReview guestReview = new Model.GuestReview(
                int.Parse(Clean.Text),
                int.Parse(Rule.Text),
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
