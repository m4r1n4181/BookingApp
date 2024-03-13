using System;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.Controller
{
	public class GuestReviewController
	{
		private GuestReviewService _guestReviewService;

		public GuestReviewController()
		{
			_guestReviewService = new GuestReviewService();
		}
		public void RateGuest(GuestReview guestReview) 
		{
			_guestReviewService.RateGuest(guestReview);
		}
		public void NotifyToRate(Owner owner)
		{

		}

	}
}
