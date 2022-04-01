using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Society Society { get; set; }
        public Location Location { get; set; }
        public Room Room { get; set; }
        public string SocietyId { get; set; }
        public string LocationId { get; set; }
        public int RoomId { get; set; }
        public Timespan Timespan { get; set; }
        public string TimespanId { get; set; }
    }
}
