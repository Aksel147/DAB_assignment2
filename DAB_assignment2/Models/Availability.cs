using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public List<Timespan> Timespans { get; set; }
        public List<Location>? Locations { get; set; }
        public List<Room>? Rooms { get; set; }
        public string LocationId { get; set; }
        public int RoomId { get; set; }
    }
}
