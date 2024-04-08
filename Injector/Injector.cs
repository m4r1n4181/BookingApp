using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
        };

        public static void BindComponents()
        {

           /* VoucherRepository voucherRepository = new VoucherRepository();
            voucherRepository.BindVoucherUser();

            AccommodationOwnerReviewRepository accommodationOwnerReviewRepository = new AccommodationOwnerReviewRepository();
            accommodationOwnerReviewRepository.BindAccommodationOwnerReviewWithAccommodationReservation();
           */
            ReservationRescheduleRequestRepository reservationRescheduleRequestRepository = new ReservationRescheduleRequestRepository();
            reservationRescheduleRequestRepository.BindReservationRescheduleRequestWithAccommodationReservation();
            reservationRescheduleRequestRepository.BindReservationRescheduleRequestWithUser();

            /*
            NotificationRepository notificationRepository = new NotificationRepository();
            notificationRepository.BindNotificationTourReservation();
            */
           // _implementations.Add(typeof(IVoucherRepository), voucherRepository);
            //_implementations.Add(typeof(IAccommodationOwnerReviewRepository), accommodationOwnerReviewRepository);
            _implementations.Add(typeof(IReservationRescheduleRequestRepository), reservationRescheduleRequestRepository);
            //_implementations.Add(typeof(INotificationRepository), notificationRepository);
        }
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
