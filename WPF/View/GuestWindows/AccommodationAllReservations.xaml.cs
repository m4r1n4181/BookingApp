using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.ViewModels.GuestViewModels;
using BookingApp.WPF.Views.GuestWindows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View.GuestWindows
{
    public partial class AccommodationAllReservations : Window//, INotifyPropertyChanged
    {


        public AccommodationAllReservations()
        {
            InitializeComponent();
            DataContext = new AccommodationAllReservationsViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*  private void Activate_Click(object sender, RoutedEventArgs e)
          {
              if (SelectedReservation == null)
              {
                  MessageBox.Show("Please select a reservation before activating.");
                  return;
              }

              OwnerReviewForm ownerReviewForm = new OwnerReviewForm(SelectedReservation);
              ownerReviewForm.ShowDialog();
          }*/


    }
}
