using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Location
    {
        public string Address { get; set; }
        public string Properties { get; set; }
        public int PeopleLimit { get; set; }
        public List<Room>? Rooms { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<Timespan> Availability { get; set; }
    }
}
