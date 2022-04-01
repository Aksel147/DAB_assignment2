using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int PeopleLimit { get; set; }
        public Location Location { get; set; }
        public string LocationId { get; set; }
        public List<Booking>? Bookings { get; set; }
        public Availability Availability { get; set; }
        public int AvailabilityId { get; set; }
    }
}
