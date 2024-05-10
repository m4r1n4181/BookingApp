using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View.ViewModels.TourGuideViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourDetails.xaml
    /// </summary>
    public partial class TourDetails : Window
    {
        private double currentX;
        private int currentKeyPointIndex;
        private TourDetailsViewModel viewModel;
        public TourDetails(Tour tour)
        {
            InitializeComponent();
            viewModel = new TourDetailsViewModel(tour);
            this.DataContext = viewModel;
            currentX = 0;
            currentKeyPointIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int keyPointNum = viewModel.KeyPoints.Count;
            if (currentKeyPointIndex == keyPointNum - 1)
            {
                return;
            }
            currentKeyPointIndex++;
            int keyPointCount = 0;
            double pictureX = -1;

            // Ensure containers are created and visible
            tourFlowItemControl.UpdateLayout();

            foreach (var item in tourFlowItemControl.Items)
            {
                DependencyObject container = tourFlowItemControl.ItemContainerGenerator.ContainerFromItem(item);

                if (container != null && container is Visual visual)
                {
                    // Use relative coordinates within the control or window
                    Point position = visual.TransformToAncestor(tourFlowItemControl).Transform(new Point(0, 0));
                    if (keyPointCount == currentKeyPointIndex)
                    {
                        pictureX = position.X;
                        break;
                    }
                    keyPointCount++;
                }
            }

            // Ensure pictureX is valid before animating
            if (pictureX >= 0)
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = currentX;
                currentX = pictureX;
                if (keyPointNum == 3)
                {
                    currentX += 20;
                }
                animation.To = currentX;
                animation.Duration = TimeSpan.FromSeconds(1);

                TranslateTransform translateTransform = cikaImage.RenderTransform as TranslateTransform;
                if (translateTransform != null)
                {
                    translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
                }
            }
        }

    }

}

