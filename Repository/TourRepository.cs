using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private List<Tour> tours;

        public TourRepository()
        {
            tours = new List<Tour>();
        }

        public void Add(Tour tour) //metoda da se doda tura (u kojoj se prvo kreira, pa onda dodaje tura)
        {
            tours.Add(tour);
        }

        public List<Tour> GetAllTours()
        {
            return tours.ToList();
        }

    }


}
