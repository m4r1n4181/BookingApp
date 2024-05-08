using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class RenovatingRequestRepository : IRenovatingRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/renovatingRequests.csv";
        private readonly Serializer<RenovatingRequest> _serializer;
        public List<RenovatingRequest> RenovatingRequests;

        public RenovatingRequestRepository()
        {
            _serializer = new Serializer<RenovatingRequest>();
            RenovatingRequests = _serializer.FromCSV(FilePath);
        }

        public RenovatingRequest GetById(int id)
        {
            RenovatingRequests = _serializer.FromCSV(FilePath);
            return RenovatingRequests.FirstOrDefault(request => request.Id == id);
        }

        public List<RenovatingRequest> GetAll()
        { 

            return _serializer.FromCSV(FilePath);
        }

        public RenovatingRequest Save(RenovatingRequest renovatingRequest)
        {
            renovatingRequest.Id = NextId();
            var renovatingRequests = _serializer.FromCSV(FilePath);
            renovatingRequests.Add(renovatingRequest);
            _serializer.ToCSV(FilePath, renovatingRequests);
            return renovatingRequest;
        }

        private int NextId()
        {
            var renovatingRequests = _serializer.FromCSV(FilePath);
            if (renovatingRequests.Count < 1)
            {
                return 1;
            }
            return renovatingRequests.Max(r => r.Id) + 1;
        }

    }


}

