using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class LanguageStatisticsDto
    {
        public string Language { get; set; }
        public int FinishedTourCount { get; set; }
        public double AverageRating { get; set; }

        public LanguageStatisticsDto(string language)
        {
            Language = language;
        }
    }
}
