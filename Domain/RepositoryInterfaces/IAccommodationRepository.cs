using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        public Accommodation GetById(int id);
        public Accommodation Save(Accommodation accommodation);
        public void Delete(Accommodation accommodation);
        public Accommodation Update(Accommodation accommodation);
        public List<Accommodation> GetByOwner(Owner owner);

        public List<Accommodation> GetAll();

        public List<Accommodation> GetAllWithLocations();
        public List<Accommodation> GetByOwner(int ownerId);

        public List<Accommodation> SearchAccommodation(AccommodationSearchParams searchParams);

    }


}

