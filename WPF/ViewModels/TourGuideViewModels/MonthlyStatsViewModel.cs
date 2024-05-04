using BookingApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TourGuideViewModels
{
    public class MonthlyStatsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TourRequestController _tourRequestController;
        public Dictionary<string, int> RequestsByYearAndMonth { get; set; }

        public MonthlyStatsViewModel(int selectedYear)
        {
            _tourRequestController = new TourRequestController();
            RequestsByYearAndMonth = _tourRequestController.CountRequestsByYearAndMonth(selectedYear);

        }
    }
}
