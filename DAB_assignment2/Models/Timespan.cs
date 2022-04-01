using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Timespan
    {
        public string Span { get; set; }
        public List<Location> Locations { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
