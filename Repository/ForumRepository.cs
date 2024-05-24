using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ForumRepository : IForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";

        private readonly Serializer<Forum> _serializer;

        private CommentRepository _commentRepository;

        private List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _commentRepository = new CommentRepository();
            _forums = _serializer.FromCSV(FilePath);
        }

        public List<Forum> GetAll()
        {
            return _forums;
        }
        public Forum Get(int id)
        {
            return _forums.Find(f => f.Id == id);
        }
        public Forum Save(Forum forum)
        {
            forum.Id = NextId();
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }
        public int NextId()
        {
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(f => f.Id) + 1;
        }
        public void Delete(Forum forum)
        {
            Forum founded = _forums.Find(f => f.Id == forum.Id);
            _forums.Remove(founded);
            _serializer.ToCSV(FilePath, _forums);
        }

        public Forum Update(Forum forum)
        {
            Forum current = _forums.Find(f => f.Id == forum.Id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }
        

        public List<Forum> GetByAuthorId(int id)
        {
            return _forums.FindAll(f => f.Author.Id == id);
        }
    }
}
