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
        public Availability Availability { get; set; }
        public int AvailabilityId { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
