using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DependencyInjection
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IReservationRescheduleRequestRepository), new ReservationRescheduleRequestRepository() },
            { typeof(IAccommodationOwnerReviewRepository), new AccommodationOwnerReviewRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IOwnerReviewRepository), new OwnerReviewRepository() },
            { typeof(IUserRepository), new UserRepository() },
            { typeof(ILocationRepository), new LocationRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
            { typeof(INotificationRepository), new NotificationRepository() },
            { typeof(IKeyPointRepository), new KeyPointRepository() },
            { typeof(ITourGuideRepository), new TourGuideRepository() },
            { typeof(ITouristEntryRepository), new TouristEntryRepository() },
            { typeof(ITouristRepository), new TouristRepository() },
            { typeof(ITourParticipantRepository), new TourParticipantRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(ITourRequestRepository), new TourRequestRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(ITourReviewRepository), new TourReviewRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(IRenovatingRequestRepository), new RenovatingRequestRepository() },
            { typeof(ISuperGuestRepository), new SuperGuestRepository() },
            { typeof(IAccommodationRenovationRepository), new AccommodationRenovationRepository() },
            { typeof(IGuestReviewRepository), new GuestReviewRepository() },


            
            //{}
        
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }

}
