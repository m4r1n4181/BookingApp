using BookingApp.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TourGuideViewModels
{
    public class MonthlyStatsViewModel
    {
        private TourRequestController _tourRequestController;
        public Dictionary<string, int> RequestsByYearAndMonth { get; set; }

        public MonthlyStatsViewModel(int selectedYear)
        {
            _tourRequestController = new TourRequestController();
            RequestsByYearAndMonth = _tourRequestController.CountRequestsByYearAndMonth(selectedYear);

        }
    }
}
