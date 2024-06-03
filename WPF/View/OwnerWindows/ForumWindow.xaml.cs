using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Model;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.OwnerWindows
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window //, INotifyPropertyChanged
    {
        private readonly CommentController _commentController;
        public ForumWindow(Forum selectedForum)
        {
            this.DataContext = new ForumViewModel(selectedForum);
            //_commentController = new CommentController();
            InitializeComponent();
        }
        /*
        private Comment _commentt;
        public Comment SelectedComment
        {
            get { return _commentt; }

            set
            {
                _commentt = value;
                OnPropertyChanged("SelectedComment");
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
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedComment.ReportsNumber++;
            _commentController.Update(SelectedComment);

        }*/
    }
}
