using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TouristService
    {
        private TouristRepository _touristRepository;

        public TouristService()
        {
            _touristRepository = new TouristRepository();
        }

        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }

    }
}
