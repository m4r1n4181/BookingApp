using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public interface IClose
    {
        Action Close { get; set; }
    }
}
