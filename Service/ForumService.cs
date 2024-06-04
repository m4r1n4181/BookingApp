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
    public class ForumService
    {
        private IForumRepository _forumRepository;

        private ICommentRepository _commentRepository;
        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
        }
        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }
        public List<Forum> GetForumsForOwner(int id)
        {
            return _forumRepository.GetForumsForOwner(id);
        }

        public Forum Get(int id)
        {
            return _forumRepository.Get(id);
        }
        public Forum Save(Forum forum)
        {
            return _forumRepository.Save(forum);
        }
        public Forum Update(Forum forum)
        {
            return _forumRepository.Update(forum);
        }
        public void Delete(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public Forum SaveForumComment(Forum forum)
        {
            return _forumRepository.SaveForumComment(forum);
        }

        public Boolean AvailableForum(Forum forum)
        {
            List<Forum> forums = GetAll();
            foreach (Forum forumFor in forums)
            {
                if (forum.Location.City == forumFor.Location.City)
                {
                    return false;
                }
            }
            return true;
        }


        public List<Forum> GetByAuthorId(int id)
        {
            return _forumRepository.GetByAuthorId(id);
        }
    }
}
