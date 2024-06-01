using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetAllWithDateTime();
        void BindLocations();
        List<Tour> GetAllWithLocations();
        void BindTourGuide();
        List<Tour> SearchTours(TourSearchParams searchParams);
        Tour GetById(int id);
        List<Tour> GetAll();
        List<Tour> GetAllFinished();
        Tour Save(Tour tour);
        int NextId();
        void Delete(Tour tour);
        Tour Update(Tour tour);
        List<Tour> GetByTourGuide(TourGuide tourGuide);
        List<Tour> GetByTourGuideNotStarted(int tourGuideId);

        List<Tour> GetTodayTours();
        List<Tour> GetAllActiveTours();
        List<Tour> GetAlternativeTours(int locationId);
        List<Tour> GetFutureTours();
        List<Tour> SearchTourForTourGuide(TourGuideSearch tourGuideSearch);
        List<Tour> GetCancelledToursByGuideId(int guideId);
        List<Tour> GetAllByTourGuideId(int tourGuideId);
        

    }
}
