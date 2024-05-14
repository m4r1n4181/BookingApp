using BookingApp.Controller;
using BookingApp.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for YearlyStatisticsPage.xaml
    /// </summary>
    public partial class YearlyStatisticsPage : Page, INotifyPropertyChanged
    {
       
        public ObservableCollection<int> Years { get; set; }
        public AccommodationByYearStatisticDto AccommodationStatistic {  get; set; }
       
        public AccommodationReservationController _accommodationReservationController;

        public Accommodation Accommodation { get; set; }

        public YearlyStatisticsPage(Accommodation accommodation)
        {
            InitializeComponent();
            Years = new ObservableCollection<int> { 2023, 2024, 2022, 2021 };
            this.DataContext = this;
            SelectedYear = 2023;
            Accommodation = accommodation;
            _accommodationReservationController = new AccommodationReservationController();
            AccommodationStatistic = _accommodationReservationController.GetStatisticForYear(accommodation.Id, SelectedYear);
          



        }
        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                    // Here you can call methods to update other parts of your UI based on the selected year
                    // UpdateStatistics();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}