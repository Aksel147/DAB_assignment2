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
        public List<LocationTimespan> Locations { get; set; }
        public List<RoomTimespan> Rooms { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
