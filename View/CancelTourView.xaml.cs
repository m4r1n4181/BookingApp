using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for CancelTourView.xaml
    /// </summary>
    public partial class CancelTourView : Window,INotifyPropertyChanged
    {
        public ObservableCollection<TourReservation> TourReservation { get; set; }
        public TourReservation SelectedTourReservation { get; set; }
        public Tourist SelectedTourist { get; set; }

        private TourReservationController _tourReservationController;
        private VoucherController _voucherController;

        private TouristController _touristController; //posle se menja na turiste koji su bas specificno rez turu (participents)
        public ObservableCollection<Tourist> Tourist { get; set; }

        public ObservableCollection<VoucherType> Types { get; set; }
        public VoucherType? SelectedType { get; set; }

        private VoucherType _voucherType;
        public VoucherType Type
        {
            get => _voucherType;
            set
            {
                if (value != _voucherType)
                {
                    _voucherType = value;
                    OnPropertyChanged();
                }
            }
        }
        public string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _language;
        public string Languages
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxTourists;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists)
                {
                    _maxTourists = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourDate;
        public string TourDate
        {
            get => _tourDate;
            set
            {
                if (value != _tourDate)
                {
                    _tourDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


     

        public CancelTourView()
        {
            InitializeComponent();
            this.DataContext = this;
           // SelectedTourReservation = tourReservation;

           // TourName = tourReservation.Tour.Name;

            _tourReservationController = new TourReservationController();

            
            _touristController = new TouristController();
            Tourist = new ObservableCollection<Tourist>(_touristController.GetAll());
            Types = new ObservableCollection<VoucherType>();
            Types.Add(VoucherType.resignation);
            Types.Add(VoucherType.cancellation);
            Types.Add(VoucherType.winner);
            SelectedType = null;



        }

        public void CancelTour_Click(object sender, RoutedEventArgs e) //treba svima da grantujem vaucher i oda da mogu da napravim da dam otkaz 
        {
            //Voucher voucher = new Voucher(); 
            // _voucherController.Save(voucher);

            Close();
           

        }


    }
}
