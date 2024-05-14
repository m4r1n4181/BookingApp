using BookingApp.Controller;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.ViewModels;
using BookingApp.WPF.View.OwnerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class YearlyStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<AccommodationByYearStatisticDto> AccommodationStatistics { get; set; }
        public AccommodationReservationController _accommodationReservationController;

        public Accommodation Accommodation { get; set; }

        private AccommodationByYearStatisticDto _selectedStatistic;
        public AccommodationByYearStatisticDto SelectedStatistic
        {
            get { return _selectedStatistic; }

            set
            {
                _selectedStatistic = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand StatisticsByMonthCommand { get; set; }

        public int BestYear { get; set; }
        public YearlyStatisticsViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
            _accommodationReservationController = new AccommodationReservationController();
            AccommodationStatistics = new ObservableCollection<AccommodationByYearStatisticDto>(_accommodationReservationController.GetYearStatisticForAccommodation(Accommodation.Id));


            StatisticsByMonthCommand = new RelayCommand(Execute_StatisticsByMonthCommand, CanExecute_StatisticsByMonthCommand);

            BestYear = _accommodationReservationController.GetBestYearForAccommodation(Accommodation.Id);
        }
        public bool CanExecute_StatisticsByMonthCommand(object param)
        {
            return SelectedStatistic != null;
        }

        public void Execute_StatisticsByMonthCommand(object param)
        {
           MonthlyStatistics monthlyStatistics = new MonthlyStatistics(SelectedStatistic,Accommodation);
            monthlyStatistics.Show();
        }
    }
}
