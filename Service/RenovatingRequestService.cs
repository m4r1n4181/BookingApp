using BookingApp.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class RenovatingRequestService
    {
        private readonly IRenovatingRequestRepository _renovatingRequestRepository;

        public RenovatingRequestService(IRenovatingRequestRepository renovatingRequestRepository)
        {
            _renovatingRequestRepository = renovatingRequestRepository;
        }

        public RenovatingRequest GetRenovatingRequestById(int id)
        {
            return _renovatingRequestRepository.GetById(id);
        }

        public List<RenovatingRequest> GetAllRenovatingRequests()
        {
            return _renovatingRequestRepository.GetAll();
        }
        public RenovatingRequest Save(RenovatingRequest renovatingRequest)
        {
            return _renovatingRequestRepository.Save(renovatingRequest);
        }
    }
}
