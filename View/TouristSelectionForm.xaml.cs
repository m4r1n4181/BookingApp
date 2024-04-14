using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.ViewModel;
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
        
        public TouristSelectionForm(KeyPoint selectedKeyPoint)
        {
            InitializeComponent();
            this.DataContext = new TouristSelectionFormViewModel(selectedKeyPoint);
          

        }

        
       
      // u taj novi prozor posalji SelectedKeyPoint
        //tamo sklopis objecat ToursitEntry
        // i samo ga creiras u controlleru
    
 }
