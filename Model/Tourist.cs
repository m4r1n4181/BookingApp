using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Tourist
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }

        public int Age { get; set; }

        // Metoda za prikaz svih tura
        public List<Tour> GetAllTours(string filePath)
        {
            // Pozivamo metodu GetAllTours iz klase Tour kako bismo dobili sve ture
            return Tour.GetAllTours(filePath);
        }

        public List<Tour> FindTour(Location location, int duration, string language, int numberOfPeople)
        {
            // Učitaj sve ture iz CSV fajla
            List<Tour> allTours = Tour.LoadToursFromCsv("");

            // Filtriranje tura na osnovu zadatih parametara
            var filteredTours = allTours.Where(tour =>
                tour.Location.City.Equals(location.City, StringComparison.OrdinalIgnoreCase) &&
                tour.Location.Country.Equals(location.Country, StringComparison.OrdinalIgnoreCase) &&
                tour.Duration <= duration &&
                tour.Language.Equals(language, StringComparison.OrdinalIgnoreCase) &&
                tour.MaxTourists >= numberOfPeople
            ).ToList();

            return filteredTours;
        }

    }
}


