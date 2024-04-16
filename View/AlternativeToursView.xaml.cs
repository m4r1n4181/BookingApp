using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
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
    /// Interaction logic for AlternativeToursView.xaml
    /// </summary>
    public partial class AlternativeToursView : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public TourService _tourService;
        //public User User { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AlternativeToursView(Location location)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourService = new TourService();
            Tours = new ObservableCollection<Tour>(_tourService.GetAlternativeTours(location.Id));
        }
    }
}
