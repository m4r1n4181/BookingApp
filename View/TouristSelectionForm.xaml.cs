using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TouristSelectionForm.xaml
    /// </summary>
    public partial class TouristSelectionForm : Window, INotifyPropertyChanged
    {
        private readonly TouristController _touristController;
        public ObservableCollection<Tourist> Tourists { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }    

        public TouristSelectionForm(KeyPoint selectedKeyPoint)
        {
            InitializeComponent();
            SelectedKeyPoint = selectedKeyPoint;

            TouristController touristController = new TouristController();
            Tourists = new ObservableCollection<Tourist>(touristController.GetAll()); 

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddTouristEntryClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTourist == null)
            {
                MessageBox.Show("Please select a tourist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            TouristEntry touristEntry = new TouristEntry
            {
                   KeyPoint = SelectedKeyPoint,
                   Tourist = SelectedTourist,
                   
            };

            TouristEntryController touristEntryController = new TouristEntryController();
            touristEntryController.AddTouristEntry(touristEntry); 

            MessageBox.Show("Tourist entry added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
         }
           
     }
    
 }
