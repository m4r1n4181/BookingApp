using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
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
    public partial class TouristSelectionForm : Window
    {
        private readonly TouristController _touristController;
        private TouristEntryController _touristEntryController;
        public ObservableCollection<Tourist> Tourists { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        public Tourist SelectedTourist { get; set; }    

        public TouristSelectionForm(KeyPoint selectedKeyPoint)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedKeyPoint = selectedKeyPoint;

            _touristController = new TouristController();
            _touristEntryController = new TouristEntryController();
            Tourists = new ObservableCollection<Tourist>(_touristController.GetAllNotOnTour(selectedKeyPoint.Tour.Id)); 

        }

        
        private void AddTouristEntry_Click(object sender, RoutedEventArgs e)
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

            _touristEntryController.AddTouristEntry(touristEntry); 

            MessageBox.Show("Tourist entry added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
         }
           
     }
      // u taj novi prozor posalji SelectedKeyPoint
        //tamo sklopis objecat ToursitEntry
        // i samo ga creiras u controlleru
    
 }
