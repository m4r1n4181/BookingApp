using BookingApp.Domain.Models;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class RenovatingRequestController
    {
        private readonly RenovatingRequestService _renovatingRequestService;

        public RenovatingRequestController(RenovatingRequestService renovatingRequestService)
        {
            _renovatingRequestService = renovatingRequestService;
        }

        public List<RenovatingRequest> GetAllRenovatingRequests()
        {
            return _renovatingRequestService.GetAllRenovatingRequests();
        }

        public RenovatingRequest GetRenovatingRequestById(int id)
        {
            return _renovatingRequestService.GetRenovatingRequestById(id);
        }
        public RenovatingRequest SaveRenovatingRequest(RenovatingRequest renovatingRequest)
        {
            return _renovatingRequestService.Save(renovatingRequest);
        }

    }
}
