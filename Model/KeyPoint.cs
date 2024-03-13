using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class KeyPoint
    {
        private string name;
        private bool isActive = false;
        private int associatedTour;
        public KeyPoint() { }

        public KeyPoint(string name, bool isActive, int associatedTour)
        {
            this.name = name;
            this.isActive = isActive;
            this.associatedTour = associatedTour;
        }
    }

}