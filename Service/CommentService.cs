﻿using BookingApp.DependencyInjection;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class CommentService
    {
        private ICommentRepository _commentRepository;

        public CommentService()
        {
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment Get(int id)
        {
            return _commentRepository.Get(id);
        }
        public Comment Save(Comment comment)
        {
            return _commentRepository.Save(comment);
        }
        public Comment Update(Comment comment)
        {
            return _commentRepository.Update(comment);
        }
        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public List<Comment> GetByForumId(int id)
        {
            return _commentRepository.GetByForumId(id);
        }
    }
}