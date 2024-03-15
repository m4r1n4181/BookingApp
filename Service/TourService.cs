using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Service
{
    public class TourService
    {
        private TourRepository tourRepository;

        public TourService(TourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }
        //metoda da se kreira pa doda tura u repository
        
    }

}
