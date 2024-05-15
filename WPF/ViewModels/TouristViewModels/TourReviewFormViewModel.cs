using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TourReviewFormViewModel : INotifyPropertyChanged
    {
        private TourReviewController _tourReviewController;
        private TourReservation _selectedTourReservation;
        private int _knowledge;
        private int _fluency;
        private int _tourAppeal;
        private string _comment;
        private ObservableCollection<string> _pictures;

        public TourReviewFormViewModel(TourReservation tourReservation)
        {
            _tourReviewController = new TourReviewController(new TourReviewService());
            _selectedTourReservation = tourReservation;
            Pictures = new ObservableCollection<string>();
        }

        public int Knowledge
        {
            get => _knowledge;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency, and Tour Appeal must be between 1 and 5.");
                    return;
                }

                if (value != _knowledge)
                {
                    _knowledge = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Fluency
        {
            get => _fluency;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency, and Tour Appeal must be between 1 and 5.");
                    return;
                }

                if (value != _fluency)
                {
                    _fluency = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TourAppeal
        {
            get => _tourAppeal;
            set
            {
                if (value < 1 || value > 5)
                {
                    MessageBox.Show("Knowledge, Fluency, and Tour Appeal must be between 1 and 5.");
                    return;
                }

                if (value != _tourAppeal)
                {
                    _tourAppeal = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public ObservableCollection<string> Pictures
        {
            get => _pictures;
            set
            {
                _pictures = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddReviewCommand => new RelayCommand(AddReview);

        public ICommand AddImagesCommand => new RelayCommand(AddImages);

        private void AddReview(object parameter)
        {
            TourReview tourReview = new TourReview()
            {
                TourReservation = _selectedTourReservation,
                Knowledge = Knowledge,
                Fluency = Fluency,
                TourAppeal = TourAppeal,
                Comment = Comment,
                Pictures = Pictures.ToList(),
            };

            _tourReviewController.RateTour(tourReview);
            MessageBox.Show("Tour and Guide successfully rated!");

        }

        private void AddImages(object parameter)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images";
            openFileDialog1.Filter = "All Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                string imagePath = openFileDialog1.FileName;
                string imageFileName = Path.GetFileName(imagePath);
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

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute(parameter);
    }
}