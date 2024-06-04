using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.WPF.View.OwnerWindows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class AllForumsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly ForumController _forumController;
        private readonly LocationController _locationController;

        
        public ObservableCollection<Forum> Forums { get; set; }
        private Forum _forum;
        public Forum SelectedForum
        {
            get { return _forum; }

            set
            {
                _forum = value;
                OnPropertyChanged("SelectedForum");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        // public Forum SelectedForum { get; set; }
        public ICommand ForumCommand { get; set; }
        public AllForumsViewModel()
        {
            _forumController = new ForumController();
            _locationController = new LocationController();

            Forums = new ObservableCollection<Forum>(_forumController.GetForumsForOwner(SignInForm.LoggedUser.Id));
            /*
            for (int i = 0; i < Forums.Count; ++i)
            {
                Forums[i].Location = _locationController.GetAll().Find(a => a.Id == Forums[i].Location.Id);

            }*/
            ForumCommand = new RelayCommand(Execute_ViewForumCommand, CanExecute);
        }

        private bool CanExecute()
        {
            //  return SelectedForum != null;
            return true;
        }

        private void Execute_ViewForumCommand()
        {
            ForumWindow forumWindow = new ForumWindow(SelectedForum);
            forumWindow.Show();
        }

    }
}
