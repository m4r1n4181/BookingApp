﻿using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.OwnerViewModels
{
    public class AccommodationOwnerReviewsViewModel : ViewModelBase
    {
        public ObservableCollection<AccommodationOwnerReview> AccommodationOwnerReviews { get; set; }
        public AccommodationOwnerReviewController _accommodationOwnerReviewController;

        public AccommodationOwnerReviewsViewModel() { 
        _accommodationOwnerReviewController = new AccommodationOwnerReviewController();
            AccommodationOwnerReviews = new ObservableCollection<AccommodationOwnerReview>(_accommodationOwnerReviewController.GetAllValidReviewsForOwner(SignInForm.LoggedUser.Id));
        }
    }
}
