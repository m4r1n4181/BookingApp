using BookingApp.Model;
using BookingApp.WPF.ViewModels.OwnerPageViewModels;
using System;
using System.Windows.Controls;

namespace BookingApp.WPF.View.OwnerPages
{
    /// <summary>
    /// Interaction logic for AccommodationRenovationsOverviewPage.xaml
    /// </summary>
    public partial class AccommodationRenovationsOverviewPage : Page
    {
        

        public AccommodationRenovationsOverviewPage(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationRenovationsOverviewViewModelPage(); 
        }       

    }

}
