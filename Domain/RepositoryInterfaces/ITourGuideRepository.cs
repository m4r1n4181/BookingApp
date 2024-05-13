﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGuideRepository
    {
        List<TourGuide> GetAll();
        TourGuide Save(TourGuide tourGuide);
        int NextId();
        void Delete(TourGuide tourGuide);
        TourGuide Update(TourGuide tourGuide);
        TourGuide GetById(int id);
    }
}
