using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ForumViewModel: ViewModelBase
    {
        private readonly CommentController _commentController;
        public ObservableCollection<Comment> Comments { get; set; }

       
        ICommand CommentCommand { get; set; }
        ICommand ReportCommand { get; set; }
        public Forum forum { get; set; }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        public Comment SelectedComment { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ForumViewModel( Forum selectedForum)
        {
           
            forum = selectedForum;


            _commentController = new CommentController();


            Comments = new ObservableCollection<Comment>(_commentController.GetByForumId(forum.Id));
            Comments.Clear();
            Comments = new ObservableCollection<Comment>(_commentController.GetByForumId(forum.Id));
            CommentCommand = new RelayCommand(Execute_Comment, CanExecute);
           ReportCommand = new RelayCommand(Execute_Report, CanExecuteReport);
        }
        
        private void Execute_Report()
        {
            SelectedComment.ReportsNumber++;
            _commentController.Update(SelectedComment);
        }

        private bool CanExecuteReport()
        {
            return true;
        }

        private bool CanExecute()
        {
            if (string.IsNullOrEmpty(Comment))
            {
                MessageBox.Show("Niste uneli komentar!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (forum.IsOpen == false)
            {
                MessageBox.Show("Ne mozete dodati komentar na zatvorene forume!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
           
            }

        private void Execute_Comment()
        {
          
                Comment comment = new Comment() { Text = Comment, Author = SignInForm.LoggedUser, Role = SignInForm.LoggedUser.Type, ForumId = forum.Id, ReportsNumber = 0 };
                _commentController.Save(comment);
                forum.Comments.Add(comment);
                MessageBox.Show("Uspešno ste ostavili komentar na forumu!", "Komentar ostavljen!", MessageBoxButton.OK);
            
        }

    }
}
