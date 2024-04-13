using BookingApp.Controller;
using BookingApp.Domain.Models;
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

namespace BookingApp.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for DeclineRescheduleRequestComment.xaml
    /// </summary>
    public partial class DeclineRescheduleRequestComment : Window, INotifyPropertyChanged
    {
        public ReservationRescheduleRequestController _reservationRescheduleRequestController;

        #region NotifyProperties
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
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        public ReservationRescheduleRequest ReservationRescheduleRequest { get; set; }

        public DeclineRescheduleRequestComment(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationRescheduleRequestController = new ReservationRescheduleRequestController();

            ReservationRescheduleRequest = reservationRescheduleRequest;

        }
        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {

            ReservationRescheduleRequest.Status = Model.Enums.RequestStatusType.Declined;
            ReservationRescheduleRequest.Comment = Comment;
            _reservationRescheduleRequestController.Update(ReservationRescheduleRequest);
            Close();
        }
    }
}
