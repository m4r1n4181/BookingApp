using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.GuestViewModels
{
    public class OwnerReviewFormViewModel
    {

        private OwnerReviewController _ownerReviewController;
        public AccommodationReservation SelectedAccommodationReservation;
        public List<string> Pictures { get; set; }

        public RelayCommand AddCommand {  get; set; }
        public RelayCommand SaveCommand { get; set; }
        public OwnerReviewFormViewModel(AccommodationReservation accommodationReservation) 
        {
            _ownerReviewController = new OwnerReviewController(new OwnerReviewService());
            SelectedAccommodationReservation = accommodationReservation;
            Pictures = new List<string>();
            AddCommand = new RelayCommand(Button_Click_2);
            SaveCommand = new RelayCommand(Button_Click);
        }

        private int _cleanliness;
        public int Clean
        {
            get => _cleanliness;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Cleanliness must be number between 1 and 5.");
                    return;
                }
                if (value != _cleanliness)
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


        private void Button_Click(object sender)
        {

            OwnerReview ownerReview = new OwnerReview()
            {
                Reservation = SelectedAccommodationReservation,
                Cleanliness = Clean,
                RuleAdherence = Rule,
                Comment = Comment,
                Pictures = Pictures,
            };

            _ownerReviewController.RateOwner(ownerReview);
            MessageBox.Show("Owner and accommodation successfully rated!");
            //this.Close();

        }

      /*  private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }*/

        private void Button_Click_2(object sender)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images";
            openFileDialog1.Filter = "All Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                string imagePath = openFileDialog1.FileName;//stara putanja
                string imageFileName = System.IO.Path.GetFileName(imagePath);
                string imageDestinationPath = "../../../Resources/Images/" + imageFileName; //nova putanja
                File.Copy(imagePath, imageDestinationPath);

                Pictures.Add(imageDestinationPath);

            }

        }
    }
}
